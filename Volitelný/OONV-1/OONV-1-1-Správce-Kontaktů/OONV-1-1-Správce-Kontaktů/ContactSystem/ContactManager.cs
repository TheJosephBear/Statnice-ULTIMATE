using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Interface;
using OONV_1_1_Správce_Kontaktů.Iterator;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    internal class ContactManager : Singleton<ContactManager> {

        List<Contact> _contacts;

        public ContactManager() {
            _contacts = new List<Contact>();

        }

        public void AddContact(Contact contact) {
            _contacts.Add(contact);
        }

        public void CreateContactCopy(Contact contact) {

        }

        public void UpdateContact(Contact contact) {

        }

        public void DeleteContact(Contact contact) {

        }

        public List<Contact> GetContactList() {
            return _contacts;
        }

        public IIterator<Contact> GetContactIterator(ListIteratorType type) {
            switch (type) {
                case ListIteratorType.Alphabetical:
                    return new ContactAlphabeticalIterator(_contacts);
                default:
                    return new ContactAlphabeticalIterator(_contacts);
            }
        }



    }
}
