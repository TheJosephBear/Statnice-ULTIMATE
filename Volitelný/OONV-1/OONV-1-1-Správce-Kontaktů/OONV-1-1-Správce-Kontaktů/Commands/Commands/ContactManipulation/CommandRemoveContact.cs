using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    /// <summary>
    /// Command to remove a contact and return to the previous view.
    /// </summary>
    internal class CommandRemoveContact : ICommand {
        private Contact _contactReff;

        /// <summary>
        /// Initializes the remove command with a reference to the contact.
        /// </summary>
        /// <param name="contact">The contact to delete.</param>
        public CommandRemoveContact(Contact contact) {
            _contactReff = contact;
        }

        /// <summary>
        /// Removes the contact and navigates back.
        /// </summary>
        public void Execute() {
            ContactManager.Instance.DeleteContact(_contactReff);
            NavigationManager.Instance.PopView();
        }

        /// <summary>
        /// Undoes the removal by re-adding the contact.
        /// </summary>
        public void Undo() {
            ContactManager.Instance.AddContact(_contactReff);
        }
    }
}
