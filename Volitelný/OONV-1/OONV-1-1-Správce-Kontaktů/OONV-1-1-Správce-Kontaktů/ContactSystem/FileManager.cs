using System;
using System.Diagnostics;
using System.IO;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    /// <summary>
    /// Provides file operations for saving and loading JSON data.
    /// </summary>
    public static class FileManager {
        /// <summary>
        /// Saves a JSON string into a file, creating directories if needed.
        /// </summary>
        /// <param name="fileName">The file path to save to.</param>
        /// <param name="jsonString">The JSON content to write.</param>
        public static void SaveIntoFile(string fileName, string jsonString) {
            try {
                string directory = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory)) {
                    Trace.WriteLine("Created directory");
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(fileName, jsonString);
                Trace.WriteLine("Text written in file");
            } catch (Exception ex) {
                Trace.WriteLine("Error saving file: " + ex.Message);
            }
        }

        /// <summary>
        /// Reads the entire content of a file as a string.
        /// </summary>
        /// <param name="fileName">The file path to read from.</param>
        /// <returns>File content as string, or empty if file does not exist.</returns>
        public static string GetFileAsString(string fileName) {
            if (File.Exists(fileName)) {
                return File.ReadAllText(fileName);
            }
            Trace.WriteLine("Error getting file: " + fileName);
            return string.Empty;
        }
    }
}
