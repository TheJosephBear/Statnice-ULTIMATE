using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Interface;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem {
    internal class Contact : IPrototype<Contact> {
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

        public Contact Copy() {
            return new Contact(Name + "(Copy)", Email, PhoneNumber);
        }

        public Contact Copy(bool sameName = false) {
            if (sameName) return new Contact(Name, Email, PhoneNumber);
            return new Contact(Name + "(Copy)", Email, PhoneNumber);
        }

        public string ToJson() {
            return $"{{\"Name\":\"{Escape(Name)}\",\"Email\":\"{Escape(Email)}\",\"PhoneNumber\":\"{Escape(PhoneNumber)}\"}}";
        }

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
