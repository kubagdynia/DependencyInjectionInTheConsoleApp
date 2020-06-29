using System;
using Autofac;
using CommandLine;
using LoadingMultipleConfig.Configuration;
using LoadingMultipleConfig.Configuration.Models;
using LoadingMultipleConfig.Import;

namespace LoadingMultipleConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterModule(new ProcessModule())
                .RegisterModule(new ConfigurationsModule(args));

            var container = builder.Build();
            
            var importResult = container.Resolve<IImportProcess>().DoImport();

            // Show import result
            foreach (var item in importResult)
            {
                Console.WriteLine(item);
            }
        }
    }
    
    internal class ProcessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoadData>().As<ILoadData>().InstancePerDependency();
            
            builder.RegisterType<ImportInformBookstore>().Keyed<IImport>(ImportType.InformBookstore);
            builder.RegisterType<ImportSaveInDb>().Keyed<IImport>(ImportType.SaveInDb);
            builder.RegisterType<ImportSendToBackOfficeSystem>().Keyed<IImport>(ImportType.SendToBackOfficeSystem);

            builder.RegisterType<ImportProcess>().As<IImportProcess>().InstancePerDependency();
        }
    }

    internal class ConfigurationsModule : Module
    {
        private readonly string[] _args;

        public ConfigurationsModule(string[] args)
            => _args = args;

        protected override void Load(ContainerBuilder builder)
        {
            Parser.Default.ParseArguments<Options>(_args)
                .WithParsed(options =>
                {
                    builder.RegisterType<AppConfiguration>()
                        .WithParameter(new TypedParameter(typeof(string), options.Config))
                        .SingleInstance();

                })
                .WithNotParsed(errors =>
                {
                    // in case of parameter parsing errors or using help option close the application
                    Environment.Exit(0);
                });
        }
    }
    
    internal class Options
    {
        [Option('c', "config", Required = false, Default = "bookConfig.json" , HelpText = "Configuration file name")]
        public string Config { get; set; } = String.Empty;
    }
}