using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class JsonLogger<T> : ILogger<T>
    {
        private const string path = @"C:\Users\anna.timoskina\source\repos\GameOfLife\GameOfLife\Log\";
        private const string DateTimeFormat = "yyyy-MM-dd-hh-mm-ss";
        private const string FilePrefix = "log_";
        private const string FileFormat = ".json";

        private string CreateFileName()
        {
            var now = DateTime.Now.ToString(DateTimeFormat);
            string fileName = $"{FilePrefix}{now}{FileFormat}";
            return fileName;
        }

        public T RestoreLast()
        {
            var latestFileName = GetLatestFileName();
            if (latestFileName == null)
                return default(T);
            T t = JsonConvert.DeserializeObject<T>(File.ReadAllText(path + latestFileName));
            return t;
        }

        public void SaveLogFile(T t)
        {
            var fileName = CreateFileName();
            File.WriteAllText(path + fileName, JsonConvert.SerializeObject(t));
        }

        private string GetLatestFileName()
        {
            var directory = new DirectoryInfo(path);
            var latestFile = directory.GetFiles().OrderByDescending(f => f.LastWriteTime).First().Name;
            return latestFile;
        }
    }
}
