using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation {
    /// <summary>
    /// Command to navigate to the view for displaying a specific contact.
    /// </summary>
    internal class CommandEnterContactViewing : ICommand {
        private Contact _contactReff;

        /// <summary>
        /// Initializes the command with a contact to display.
        /// </summary>
        /// <param name="contact">The contact to set as active and view.</param>
        public CommandEnterContactViewing(Contact contact) {
            _contactReff = contact;
        }

        /// <summary>
        /// Sets the active contact and opens the viewing screen.
        /// </summary>
        public void Execute() {
            ContactManager.Instance.SetActiveContact(_contactReff);
            NavigationManager.Instance.PushView(new ViewContactViewing());
        }

        /// <summary>
        /// Returns to the previous view.
        /// </summary>
        public void Undo() {
            NavigationManager.Instance.PopView();
        }
    }
}
