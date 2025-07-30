using Microsoft.VisualStudio.TestTools.UnitTesting;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using System.IO;

namespace TestProject1 {
    [TestClass]
    public class FileManagerTests {
        private readonly string testFile = "TestContacts.json";

        [TestCleanup]
        public void Cleanup() {
            if (File.Exists(testFile)) File.Delete(testFile);
        }

        [TestMethod]
        public void SaveAndReadFileTest() {
            string json = "{\"Name\":\"Test\",\"Email\":\"test@mail.com\",\"PhoneNumber\":\"123\"}";
            FileManager.SaveIntoFile(testFile, json);

            string readJson = FileManager.GetFileAsString(testFile);
            Assert.AreEqual(json, readJson);
        }

        [TestMethod]
        public void ReadMissingFileReturnsEmptyTest() {
            string result = FileManager.GetFileAsString("nonexistentfile.json");
            Assert.AreEqual(string.Empty, result);
        }
    }
}
