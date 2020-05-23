using System;
using System.IO;

namespace LoadingMultipleConfig
{
    public class LoadData : ILoadData
    {
        public string ReadData(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Error! Config file '{filename}' not found");
                Environment.Exit(0);
            }
            
            var jsonString = File.ReadAllText(filename);
            
            return jsonString;
        }
    }
}