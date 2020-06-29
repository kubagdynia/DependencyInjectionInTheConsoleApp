using System.Linq;
using Autofac;
using FluentAssertions;
using LoadingMultipleConfig.Configuration;
using LoadingMultipleConfig.Configuration.Models;
using LoadingMultipleConfig.Import;
using Moq;
using NUnit.Framework;

namespace LoadingMultipleConfig.Tests
{
    public class LoadingMultipleConfigTests
    {
        [TestCase(ImportType.InformBookstore, "{\"importType\":\"informBookstore\",\"books\":[{\"title\":\"ASP.NET Core 3 and Angular 9\",\"pageCount\":732,\"authors\":[{\"name\":\"Valerio De Sanctis\"}]},{\"title\":\"Hands-On Domain-Driven Design with .NET Core\",\"pageCount\":446,\"authors\":[{\"name\":\"Alexey Zimarev\"}]}]}")]
        [TestCase(ImportType.SaveInDb, "{\"importType\":\"saveInDb\",\"books\":[{\"title\":\"ASP.NET Core 3 and Angular 9\",\"pageCount\":732,\"authors\":[{\"name\":\"Valerio De Sanctis\"}]},{\"title\":\"Hands-On Domain-Driven Design with .NET Core\",\"pageCount\":446,\"authors\":[{\"name\":\"Alexey Zimarev\"}]}]}")]
        [TestCase(ImportType.SendToBackOfficeSystem, "{\"importType\":\"sendToBackOfficeSystem\",\"books\":[{\"title\":\"ASP.NET Core 3 and Angular 9\",\"pageCount\":732,\"authors\":[{\"name\":\"Valerio De Sanctis\"}]},{\"title\":\"Hands-On Domain-Driven Design with .NET Core\",\"pageCount\":446,\"authors\":[{\"name\":\"Alexey Zimarev\"}]}]}")]
        public void Import_Type_Property_Should_Indicate_How_To_Import_Books(ImportType importType, string jsonData)
        {
            // Arrange
            var container = BuildContainer(jsonData);
            
            // Act
            var importResult = container.Resolve<IImportProcess>().DoImport();

            string taskToDo = importType switch
            {
                ImportType.InformBookstore => "The bookstore will be informed of the following books:",
                ImportType.SaveInDb => "The following books have been saved in the database:",
                ImportType.SendToBackOfficeSystem => "The following books have been sent to the back office system:",
                _ => ":("
            };
            
            // Assert
            importResult.First().Should().BeEquivalentTo(taskToDo);
        }
        
        [Test]
        public void No_Books_To_Import_Should_Return_Information_That_There_Is_No_Data_To_Import()
        {
            // Arrange
            string jsonData = "{\"importType\":\"saveInDb\"}";
            var container = BuildContainer(jsonData);
            
            // Act
            var importResult = container.Resolve<IImportProcess>().DoImport();

            
            // Assert
            importResult.Should().HaveCount(1);
            importResult.First().Should().BeEquivalentTo("No data to import!");
        }
        
        [TestCase(1, "{\"importType\":\"informBookstore\",\"books\":[{\"title\":\"Hands-On Domain-Driven Design with .NET Core\",\"pageCount\":446,\"authors\":[{\"name\":\"Alexey Zimarev\"}]}]}")]
        [TestCase(2, "{\"importType\":\"saveInDb\",\"books\":[{\"title\":\"ASP.NET Core 3 and Angular 9\",\"pageCount\":732,\"authors\":[{\"name\":\"Valerio De Sanctis\"}]},{\"title\":\"Hands-On Domain-Driven Design with .NET Core\",\"pageCount\":446,\"authors\":[{\"name\":\"Alexey Zimarev\"}]}]}")]
        public void Import_Should_Return_A_List_Of_Imported_Books(int numberOfImportedBooks, string jsonData)
        {
            // Arrange
            var container = BuildContainer(jsonData);
            
            // Act
            var importResult = container.Resolve<IImportProcess>().DoImport();
            
            // Assert
            
            // added 1 because the first line is the import type description
            importResult.Should().HaveCount(numberOfImportedBooks + 1);
        }
        
        private IContainer BuildContainer(string jsonData)
        {
            var loadDataMock = new Mock<ILoadData>();
            loadDataMock.Setup(c => c.ReadData(string.Empty)).Returns(jsonData);
            
            var builder = new ContainerBuilder();
            builder.RegisterInstance(loadDataMock.Object).As<ILoadData>();
            
            builder.RegisterType<ImportInformBookstore>().Keyed<IImport>(ImportType.InformBookstore);
            builder.RegisterType<ImportSaveInDb>().Keyed<IImport>(ImportType.SaveInDb);
            builder.RegisterType<ImportSendToBackOfficeSystem>().Keyed<IImport>(ImportType.SendToBackOfficeSystem);

            builder.RegisterType<ImportProcess>().As<IImportProcess>().InstancePerDependency();

            builder.RegisterType<AppConfiguration>()
                .WithParameter(new TypedParameter(typeof(string), string.Empty))
                .SingleInstance();
            
            return builder.Build();
        }
    }
}