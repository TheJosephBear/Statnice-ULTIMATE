using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    public static class FileManager {

        // Saves json string into a file, if file doesnt exist it creates one
        public static void SaveIntoFile(string fileName, string jsonString) {
            try {
                string directory = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
                    Trace.WriteLine($"Created directory");
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(fileName, jsonString);
                Trace.WriteLine($"Text written in file");
            } catch (Exception ex) {
                Trace.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        public static string GetFileAsString(string fileName) {
            if (File.Exists(fileName)) {
                return File.ReadAllText(fileName);
            }
            Trace.WriteLine($"Error getting file: {fileName}");
            return string.Empty;
        }

    }
}
