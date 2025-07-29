using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation {
    /// <summary>
    /// Command to enter the contact creation view.
    /// </summary>
    internal class CommandEnterContactCreationView : ICommand {
        /// <summary>
        /// Pushes the contact creation view onto the navigation stack.
        /// </summary>
        public void Execute() {
            NavigationManager.Instance.PushView(new ViewContactCreation());
        }

        /// <summary>
        /// Pops the contact creation view off the navigation stack.
        /// </summary>
        public void Undo() {
            NavigationManager.Instance.PopView();
        }
    }
}
