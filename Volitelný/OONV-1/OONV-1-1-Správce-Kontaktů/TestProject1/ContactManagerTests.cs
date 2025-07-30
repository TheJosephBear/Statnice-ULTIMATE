using Microsoft.VisualStudio.TestTools.UnitTesting;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using System.Linq;

namespace TestProject1 {
    [TestClass]
    public class ContactManagerTests {
        private ContactManager manager;

        [TestInitialize]
        public void Setup() {
            manager = ContactManager.Instance;
            manager.GetContactList().Clear(); // Reset state before each test
        }

        [TestMethod]
        public void AddContactTest() {
            var contact = new Contact("David", "david@mail.com", "321");
            manager.AddContact(contact);

            Assert.IsTrue(manager.GetContactList().Contains(contact));
        }

        [TestMethod]
        public void DeleteContactTest() {
            var contact = new Contact("Eve", "eve@mail.com", "111");
            manager.AddContact(contact);
            manager.DeleteContact(contact);

            Assert.IsFalse(manager.GetContactList().Contains(contact));
        }

        [TestMethod]
        public void UpdateContactTest() {
            var contact = new Contact("Frank", "frank@mail.com", "555");
            var updated = new Contact("Frank Updated", "frank@newmail.com", "000");

            manager.AddContact(contact);
            manager.UpdateContact(contact, updated);

            Assert.AreEqual("Frank Updated", contact.Name);
            Assert.AreEqual("frank@newmail.com", contact.Email);
        }

        [TestMethod]
        public void SaveAndLoadContactsTest() {
            var contact = new Contact("Gina", "gina@mail.com", "777");
            manager.AddContact(contact);
            manager.SaveContacts();

            manager.GetContactList().Clear();
            manager.LoadContacts();

            Assert.IsTrue(manager.GetContactList().Any(c => c.Name == "Gina"));
        }

        [TestMethod]
        public void SetAndGetActiveContactTest() {
            var contact = new Contact("Henry");
            manager.SetActiveContact(contact);

            var active = manager.GetActiveContact();
            Assert.AreEqual("Henry", active.Name);
        }
    }
}
