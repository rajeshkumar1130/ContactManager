using System;
using System.Collections.Generic;
using Xunit.Sdk;
using System.Reflection;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ContactManager.Common.Attributes
{
    public class JsonFileDataAttribute : DataAttribute
    {
        private readonly string _filePath;
        private readonly string _propertyName;

        /// <summary>
        /// Load data from a json file as data source.
        /// </summary>
        public JsonFileDataAttribute(string filePath):this(filePath,null)
        {

        }

        /// <summary>
        /// Load data from a json file as data source.
        /// </summary>
        public JsonFileDataAttribute(string filePath, string propertyName)
        {
            _filePath = filePath;
            _propertyName = propertyName;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            string fileData;
            if (testMethod == null) { throw new ArgumentNullException(nameof(testMethod)); }

            // Get the absolute path to the JSON file
            var path = Path.IsPathRooted(_filePath)
                ? _filePath
                : Path.GetRelativePath(Directory.GetCurrentDirectory(), _filePath);

            if (!File.Exists(path))
            {
                throw new ArgumentException($"Could not find file at path: {path}");
            }
            try
            {
                // Load the file
                fileData = File.ReadAllText(_filePath);
            }
            catch
            {
                throw;
            }

            if (string.IsNullOrEmpty(_propertyName))
            {
                JArray jArray = JArray.Parse(fileData);
                //whole file is the data 
                return jArray.ToObject<List<object[]>>();
            }

            // Only use the specified property as the data
            var allData = JObject.Parse(fileData);
            var data = allData[_propertyName];
            return data.ToObject<List<object[]>>();

        }
    }
}
