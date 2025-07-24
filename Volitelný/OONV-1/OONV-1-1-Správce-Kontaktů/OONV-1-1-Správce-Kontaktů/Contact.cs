using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OONV_1_1_Správce_Kontaktů {
    internal class Contact : IPrototype<Contact> {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }

        public Contact(string name, string email, string phoneNumber) {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Contact Copy() {
            return new Contact(Name, Email, PhoneNumber);
        }
    }
}
