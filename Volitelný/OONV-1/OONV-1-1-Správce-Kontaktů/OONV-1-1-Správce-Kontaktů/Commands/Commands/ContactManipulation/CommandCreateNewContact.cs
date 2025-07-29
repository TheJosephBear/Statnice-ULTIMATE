using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    /// <summary>
    /// Command to add a newly created contact.
    /// </summary>
    internal class CommandCreateNewContact : ICommand {
        private Contact _newContact;

        /// <summary>
        /// Initializes the command with the new contact to add.
        /// </summary>
        /// <param name="contact">The contact to add.</param>
        public CommandCreateNewContact(Contact contact) {
            _newContact = contact;
        }

        /// <summary>
        /// Executes the addition of the contact.
        /// </summary>
        public void Execute() {
            ContactManager.Instance.AddContact(_newContact);
        }

        /// <summary>
        /// Undoes the addition by removing the contact.
        /// </summary>
        public void Undo() {
            ContactManager.Instance.DeleteContact(_newContact);
        }
    }
}
