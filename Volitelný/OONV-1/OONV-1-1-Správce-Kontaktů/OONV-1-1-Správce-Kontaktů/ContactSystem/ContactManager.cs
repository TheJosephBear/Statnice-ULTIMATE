using System.Text;
using OONV_1_1_Správce_Kontaktů.Interface;
using OONV_1_1_Správce_Kontaktů.Iterator;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;
using System.Text.Json;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    /// <summary>
    /// Manages contacts, providing CRUD, persistence, and iteration features.
    /// Implements singleton pattern.
    /// </summary>
    internal class ContactManager : Singleton<ContactManager> {
        private List<Contact> _contacts;
        private readonly string contactsFileName = "Contacts";
        private Contact _activeContact; // Opened contact for editing

        public ContactManager() {
            _contacts = new List<Contact>();
            LoadContacts();
        }

        /// <summary>
        /// Adds a contact to the contact list.
        /// </summary>
        public void AddContact(Contact contact) {
            _contacts.Add(contact);
        }

        /// <summary>
        /// Creates a copy of a contact and adds it to the list.
        /// </summary>
        /// <param name="contact">Contact to copy.</param>
        /// <param name="copySameName">Whether to keep the same name.</param>
        /// <returns>The copied contact.</returns>
        public Contact CreateContactCopy(Contact contact, bool copySameName = false) {
            Contact contactCopy = contact.Copy(copySameName);
            _contacts.Add(contactCopy);
            return contactCopy;
        }

        /// <summary>
        /// Creates a temporary copy of a contact without adding it to the list.
        /// </summary>
        public Contact CreateTemporaryCopy(Contact contactToCopy) {
            return contactToCopy.Copy(true);
        }

        /// <summary>
        /// Updates an existing contact with data from another contact.
        /// </summary>
        public void UpdateContact(Contact contact, Contact updatedContact) {
            contact.Name = updatedContact.Name;
            contact.PhoneNumber = updatedContact.PhoneNumber;
            contact.Email = updatedContact.Email;
        }

        /// <summary>
        /// Removes a contact from the list.
        /// </summary>
        public void DeleteContact(Contact contact) {
            _contacts.Remove(contact);
        }

        /// <summary>
        /// Saves all contacts to a JSON file.
        /// </summary>
        public void SaveContacts() {
            FileManager.SaveIntoFile(contactsFileName + ".json", GetContactListAsJson());
        }

        /// <summary>
        /// Loads contacts from the JSON file.
        /// </summary>
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

        /// <summary>
        /// Returns all contacts serialized as a JSON array string.
        /// </summary>
        private string GetContactListAsJson() {
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

        /// <summary>
        /// Gets the full contact list.
        /// </summary>
        public List<Contact> GetContactList() {
            return _contacts;
        }

        /// <summary>
        /// Returns an iterator for the contact list based on specified type.
        /// </summary>
        public IIterator<Contact> GetContactIterator(ListIteratorType type) {
            switch (type) {
                case ListIteratorType.Alphabetical:
                    return new ContactAlphabeticalIterator(_contacts);
                default:
                    return new ContactAlphabeticalIterator(_contacts);
            }
        }

        /// <summary>
        /// Sets the active contact being edited.
        /// </summary>
        public void SetActiveContact(Contact contact) {
            _activeContact = contact;
        }

        /// <summary>
        /// Gets the currently active contact.
        /// </summary>
        public Contact GetActiveContact() {
            return _activeContact;
        }
    }
}