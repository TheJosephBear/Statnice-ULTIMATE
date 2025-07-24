using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OONV_1_1_Správce_Kontaktů {
    internal class ContactManager {

        List<Contact> contacts;

        public ContactManager() {
            contacts = new List<Contact>();

        }

        public void AddContact(Contact contact) {

        }

        public void CreateContactCopy(Contact contact) {

        }

        public void UpdateContact(Contact contact) {

        }

        public void DeleteContact(Contact contact) {

        }

        public List<Contact> GetContactList(ListIteratorType iteratorType) {
            return contacts; // for now just like that
        }



    }

    public enum ListIteratorType {
        Classic,
        Alphabetical
    }
}
