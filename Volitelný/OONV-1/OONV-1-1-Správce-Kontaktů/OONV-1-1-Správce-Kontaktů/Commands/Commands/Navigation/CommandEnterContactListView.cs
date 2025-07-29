using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation {
    /// <summary>
    /// Command to navigate to the contact list view.
    /// </summary>
    internal class CommandEnterContactListView : ICommand {
        /// <summary>
        /// Pushes the contact list view onto the navigation stack.
        /// </summary>
        public void Execute() {
            NavigationManager.Instance.PushView(new ViewContactList());
        }

        /// <summary>
        /// Pops the contact list view off the navigation stack.
        /// </summary>
        public void Undo() {
            NavigationManager.Instance.PopView();
        }
    }
}
