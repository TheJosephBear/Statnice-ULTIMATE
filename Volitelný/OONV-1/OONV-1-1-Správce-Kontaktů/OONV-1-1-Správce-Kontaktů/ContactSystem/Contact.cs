using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Interface;

namespace OONV_1_1_Správce_Kontaktů.ContactSystem
{
    internal class Contact : IPrototype<Contact>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Contact() {
            Name = "";
            Email = "";
            PhoneNumber = "";
        }

        public Contact(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Contact Copy()
        {
            return new Contact(Name, Email, PhoneNumber);
        }
    }
}
