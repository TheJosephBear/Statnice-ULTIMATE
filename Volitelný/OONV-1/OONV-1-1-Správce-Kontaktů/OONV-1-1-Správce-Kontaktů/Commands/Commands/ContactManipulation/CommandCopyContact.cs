using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    /// <summary>
    /// Command to create a copy of a given contact.
    /// </summary>
    public class CommandCopyContact : ICommand {
        private Contact _contactReff;

        /// <summary>
        /// Initializes the copy command with a reference to the contact.
        /// </summary>
        /// <param name="contact">The contact to copy.</param>
        public CommandCopyContact(Contact contact) {
            _contactReff = contact;
        }

        /// <summary>
        /// Executes the copy action.
        /// </summary>
        public void Execute() {
            ContactManager.Instance.CreateContactCopy(_contactReff);
        }

        /// <summary>
        /// Undoes the copy by removing the original reference.
        /// </summary>
        public void Undo() {
            ContactManager.Instance.DeleteContact(_contactReff);
        }
    }
}
