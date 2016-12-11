using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Extensions
{
    public static class FileSaver
    {
        public static string SavingFolderName { get; } = "\\query results";

        public static string SavingPath {
            get {
                return Directory.GetCurrentDirectory() + SavingFolderName + "\\";
            }
        }

        public static void SaveStringToFile(string fileName, string content)
        {
            Directory.CreateDirectory(SavingPath);
            var path = SavingPath + fileName;
            File.WriteAllText(path, content);

        }
    }
}
