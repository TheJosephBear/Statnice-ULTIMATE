using Microsoft.VisualStudio.TestTools.UnitTesting;
using OONV_1_1_Správce_Kontaktù.ContactSystem;

namespace TestProject1 {
    [TestClass]
    public class ContactTests {
        [TestMethod]
        public void ContactSerializationDeserializationTest() {
            var original = new Contact("Alice", "alice@mail.com", "123456789");
            string json = original.ToJson();

            var deserialized = new Contact();
            deserialized.FromJson(json);

            Assert.AreEqual(original.Name, deserialized.Name);
            Assert.AreEqual(original.Email, deserialized.Email);
            Assert.AreEqual(original.PhoneNumber, deserialized.PhoneNumber);
        }

        [TestMethod]
        public void ContactCopyWithSuffixTest() {
            var contact = new Contact("Bob", "bob@mail.com", "999");
            var copy = contact.Copy();

            Assert.AreEqual("Bob(Copy)", copy.Name);
            Assert.AreEqual(contact.Email, copy.Email);
        }

        [TestMethod]
        public void ContactCopySameNameTest() {
            var contact = new Contact("Carol", "carol@mail.com", "888");
            var copy = contact.Copy(true);

            Assert.AreEqual("Carol", copy.Name);
        }
    }
}