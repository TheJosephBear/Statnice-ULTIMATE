using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    /// <summary>
    /// Command to update a contact's information.
    /// </summary>
    internal class CommandEditContact : ICommand {
        private Contact _contactReff;
        private Contact _updatedContactReff;
        private Contact _originalContactInfo;

        /// <summary>
        /// Initializes the edit command with old and new contact data.
        /// </summary>
        /// <param name="contact">The original contact reference.</param>
        /// <param name="updatedContact">The new contact data.</param>
        public CommandEditContact(Contact contact, Contact updatedContact) {
            _contactReff = contact;
            _updatedContactReff = updatedContact;
            _originalContactInfo = contact; // Note: This assumes reference-copy; consider using deep clone for immutability.
        }

        /// <summary>
        /// Applies the update to the contact.
        /// </summary>
        public void Execute() {
            ContactManager.Instance.UpdateContact(_contactReff, _updatedContactReff);
        }

        /// <summary>
        /// Reverts the contact back to original data.
        /// </summary>
        public void Undo() {
            ContactManager.Instance.UpdateContact(_contactReff, _originalContactInfo);
        }
    }
}
