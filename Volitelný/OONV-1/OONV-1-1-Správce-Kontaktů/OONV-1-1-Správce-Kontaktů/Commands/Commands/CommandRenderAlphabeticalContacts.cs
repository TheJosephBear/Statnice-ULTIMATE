using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands {
    /// <summary>
    /// Command to render contacts in alphabetical order in the contact list view.
    /// </summary>
    internal class CommandRenderAlphabeticalContacts : ICommand {
        private ViewContactList _viewInstanceReff;

        /// <summary>
        /// Initializes the command with a reference to the contact list view.
        /// </summary>
        /// <param name="viewContactListReff">The contact list view to operate on.</param>
        public CommandRenderAlphabeticalContacts(ViewContactList viewContactListReff) {
            _viewInstanceReff = viewContactListReff;
        }

        /// <summary>
        /// Renders the contact list sorted alphabetically.
        /// </summary>
        public void Execute() {
            _viewInstanceReff.RenderAlphabeticalList();
        }

        /// <summary>
        /// Reverts the view back to its default rendering.
        /// </summary>
        public void Undo() {
            _viewInstanceReff.Render();
        }
    }
}
