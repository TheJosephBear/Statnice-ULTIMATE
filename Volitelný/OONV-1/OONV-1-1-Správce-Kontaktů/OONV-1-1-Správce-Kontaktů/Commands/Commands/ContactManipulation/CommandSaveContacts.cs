using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    internal class CommandSaveContacts : ICommand {

        public void Execute() {
            Console.WriteLine("Contacts saved!"); // i know i shouldnt write into console through here but it is the fastest way to do it
            ContactManager.Instance.SaveContacts();
        }

        public void Undo() {
            // Can't undo that!
        }

    }
}
