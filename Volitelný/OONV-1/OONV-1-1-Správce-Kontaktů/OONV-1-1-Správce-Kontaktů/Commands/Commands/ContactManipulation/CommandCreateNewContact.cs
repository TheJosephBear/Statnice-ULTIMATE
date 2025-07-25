using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation
{
    internal class CommandCreateNewContact : ICommand
    {

        Contact _newContact;

        public CommandCreateNewContact(Contact contact)
        {
            _newContact = contact;
        }

        public void Execute()
        {
            ContactManager.Instance.AddContact(_newContact);
        }

        public void Undo()
        {
            ContactManager.Instance.DeleteContact(_newContact);
        }
    }
}
