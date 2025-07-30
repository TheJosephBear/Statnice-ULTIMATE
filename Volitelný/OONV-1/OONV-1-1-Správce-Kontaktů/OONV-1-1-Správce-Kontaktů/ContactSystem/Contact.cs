using System.Text.Json;
using OONV_1_1_Správce_Kontaktů.Interface;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    /// <summary>
    /// Represents a contact with basic information like name, email, and phone number.
    /// Implements prototype pattern for cloning.
    /// </summary>
    public class Contact : IPrototype<Contact> {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Contact() {
            Name = "";
            Email = "";
            PhoneNumber = "";
        }

        public Contact(string name, string email, string phoneNumber) {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Contact(string name, string email) {
            Name = name;
            Email = email;
        }

        public Contact(string name) {
            Name = name;
        }

        /// <summary>
        /// Creates a copy of this contact with "(Copy)" appended to the name.
        /// </summary>
        /// <returns>A new copied Contact instance.</returns>
        public Contact Copy() {
            return new Contact(Name + "(Copy)", Email, PhoneNumber);
        }

        /// <summary>
        /// Creates a copy of this contact.
        /// Optionally keeps the same name without "(Copy)" suffix.
        /// </summary>
        /// <param name="sameName">If true, copy has the same name.</param>
        /// <returns>A new copied Contact instance.</returns>
        public Contact Copy(bool sameName = false) {
            if (sameName) return new Contact(Name, Email, PhoneNumber);
            return new Contact(Name + "(Copy)", Email, PhoneNumber);
        }

        /// <summary>
        /// Serializes the contact to a JSON string.
        /// </summary>
        /// <returns>JSON string representation of the contact.</returns>
        public string ToJson() {
            return $"{{\"Name\":\"{Escape(Name)}\",\"Email\":\"{Escape(Email)}\",\"PhoneNumber\":\"{Escape(PhoneNumber)}\"}}";
        }

        /// <summary>
        /// Deserializes a JSON string into this contact's properties.
        /// </summary>
        /// <param name="json">JSON string representing a contact.</param>
        public void FromJson(string json) {
            var contactData = JsonDocument.Parse(json).RootElement;
            Name = contactData.GetProperty("Name").GetString() ?? "";
            Email = contactData.GetProperty("Email").GetString() ?? "";
            PhoneNumber = contactData.GetProperty("PhoneNumber").GetString() ?? "";
        }

        private string Escape(string input) {
            return input.Replace("\"", "\\\""); // escape double quotes
        }
    }
}