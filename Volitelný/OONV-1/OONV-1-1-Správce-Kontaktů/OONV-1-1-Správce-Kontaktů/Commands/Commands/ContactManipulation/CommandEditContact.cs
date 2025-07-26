using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    internal class CommandEditContact : ICommand {

        Contact _contactReff;
        Contact _updatedContactReff;
        Contact _originalContactInfo;

        public CommandEditContact(Contact contact, Contact updatedContact) {
            _contactReff = contact;
            _updatedContactReff = updatedContact;
            _originalContactInfo = contact;
        }

        public void Execute() {
            ContactManager.Instance.UpdateContact(_contactReff, _updatedContactReff);
        }

        public void Undo() {
            ContactManager.Instance.UpdateContact(_contactReff, _originalContactInfo);
        }
    }
}
