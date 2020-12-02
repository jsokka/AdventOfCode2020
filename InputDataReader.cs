using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    public static class InputDataReader
    {
        private readonly static string inputDataFolderPath =
            Path.Combine(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).FullName, "InputData");

        public async static Task<IEnumerable<T>> GetInputDataAsync<T>(string fileName, string delimiter = "\n")
        {
            string filePath = Path.Combine(inputDataFolderPath, fileName);

            return (await File.ReadAllTextAsync(filePath))
                .Split(delimiter)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(Convert<T>);
        }

        static T Convert<T>(string value)
        {
            var convertedValue = TypeDescriptor.GetConverter(typeof(T));
            return (T)convertedValue.ConvertFromInvariantString(value);
        }
    }
}
