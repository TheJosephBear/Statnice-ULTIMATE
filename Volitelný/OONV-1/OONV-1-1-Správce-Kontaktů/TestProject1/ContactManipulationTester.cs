using OONV_1_1_Správce_Kontaktù.ContactSystem;

namespace TestProject1 {
    [TestClass]
    public class ContactManipulationTester {
        [TestMethod]
        public void CreateContactCopyTest() {
            // Arrange (Nachystat si vìci)
            ContactManager contactManager = ContactManager.Instance;

            // Act (Spustit funkce, které mají fungovat)
            Contact contactToCopy = new Contact("Test1");
            contactToCopy.Email = "test";
            Contact createdContact = contactManager.CreateContactCopy(contactToCopy);
            
            // Assert (Ovìøit že mám správný výsledek)
            bool result = contactToCopy.Email == createdContact.Email;
            Assert.IsTrue(result);
        }
    }
}