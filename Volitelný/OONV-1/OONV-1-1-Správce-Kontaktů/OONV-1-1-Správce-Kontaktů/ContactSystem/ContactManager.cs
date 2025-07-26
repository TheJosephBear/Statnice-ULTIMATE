using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Interface;
using OONV_1_1_Správce_Kontaktů.Iterator;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;
using System.Text.Json;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    internal class ContactManager : Singleton<ContactManager> {

        List<Contact> _contacts;
        string contactsFileName = "Contacts";
        Contact _activeContact; // Opened contact for editing

        public ContactManager() {
            _contacts = new List<Contact>();
            LoadContacts();
        }

        public void AddContact(Contact contact) {
            _contacts.Add(contact);
        }

        public Contact CreateContactCopy(Contact contact, bool copySameName = false) {
            Contact contactCopy = contact.Copy(copySameName);
            _contacts.Add(contactCopy);
            return contactCopy;
        }

        public Contact CreateTemporaryCopy(Contact contactToCopy) {
            return contactToCopy.Copy(true);
        }

        public void UpdateContact(Contact contact, Contact updatedContact) {
            contact.Name = updatedContact.Name;
            contact.PhoneNumber = updatedContact.PhoneNumber;
            contact.Email = updatedContact.Email;
        }

        public void DeleteContact(Contact contact) {
            _contacts.Remove(contact);
        }

        public void SaveContacts() {
            FileManager.SaveIntoFile(contactsFileName+".json", GetContactListAsJson());
        }

        public void LoadContacts() {
            string json = FileManager.GetFileAsString(contactsFileName + ".json");

            if (!string.IsNullOrWhiteSpace(json)) {
                try {
                    var jsonArray = JsonDocument.Parse(json).RootElement;

                    if (jsonArray.ValueKind == JsonValueKind.Array) {
                        _contacts.Clear();
                        foreach (var element in jsonArray.EnumerateArray()) {
                            Contact contact = new Contact();
                            contact.FromJson(element.GetRawText());
                            _contacts.Add(contact);
                        }
                    }
                } catch (Exception ex) {
                    Console.WriteLine("Error loading contacts: " + ex.Message);
                }
            }
        }

        string GetContactListAsJson() {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            for (int i = 0; i < _contacts.Count; i++) {
                sb.Append(_contacts[i].ToJson());
                if (i < _contacts.Count - 1)
                    sb.Append(",");
            }

            sb.Append("]");
            return sb.ToString();
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

        public void SetActiveContact(Contact contact) {
            _activeContact = contact;
        }

        public Contact GetActiveContact() {
            return _activeContact;
        }

    }
}
