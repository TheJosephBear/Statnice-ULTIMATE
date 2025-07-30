using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation {
    /// <summary>
    /// Command to save all current contacts.
    /// </summary>
    public class CommandSaveContacts : ICommand {
        /// <summary>
        /// Executes saving of contacts.
        /// </summary>
        public void Execute() {
            Console.WriteLine("Contacts saved!"); // TEMPORARY: Consider injecting a logging system later
            ContactManager.Instance.SaveContacts();
        }

        /// <summary>
        /// Cannot undo a save operation.
        /// </summary>
        public void Undo() {
            // Not supported
        }
    }
}
