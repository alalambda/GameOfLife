using GameOfLife.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife.Logger
{
    public class JsonLogger<T> : ILogger<T>
    {
        private const string path = @"C:\Users\anna.timoskina\source\repos\GameOfLife\Log\";
        private const string DateTimeFormat = "yyyy-MM-dd-hh-mm-ss-fff-tt";
        private const string FilePrefix = "log_";
        private const string FileFormat = ".json";

        private string CreateFileName()
        {
            var dateTimeNow = DateTime.Now.ToString(DateTimeFormat);
            string fileName = $"{FilePrefix}{dateTimeNow}{FileFormat}";
            return fileName;
        }

        public T RestoreLastGameFromLogFile()
        {
            var latestFileName = GetLatestFileName();
            return string.IsNullOrEmpty(latestFileName) ? 
                default(T) : JsonConvert.DeserializeObject<T>(File.ReadAllText(path + latestFileName));
        }

        public void SaveGameToLogFile(T deserialized)
        {
            var fileName = CreateFileName();
            File.WriteAllText(path + fileName, JsonConvert.SerializeObject(deserialized));
        }

        private string GetLatestFileName()
        {
            var directory = new DirectoryInfo(path);
            var latestFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First().Name;
            return latestFile;
        }
    }
}
