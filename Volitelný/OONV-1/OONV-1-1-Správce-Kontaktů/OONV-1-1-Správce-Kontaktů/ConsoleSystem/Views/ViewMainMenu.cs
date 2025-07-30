using System.Diagnostics;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    /// <summary>
    /// Entry view that displays the main menu options.
    /// </summary>
    public class ViewMainMenu : IView {
        private CommandDictionary _commandDictionary;

        /// <inheritdoc/>
        public void Initialize() {
            Trace.WriteLine("_commandDictionary initialize in menu: ");
            _commandDictionary = new CommandDictionary();
            _commandDictionary.AddInputPair("1", new CommandEnterContactListView());
        }

        /// <inheritdoc/>
        public void Render() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|||||||||||||-Main Menu-|||||||||||||");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("> 1 - Enter Contact List");
            Console.WriteLine("> undo - Undo last action");
            Console.WriteLine("> back - Go to previous menu");
            Console.WriteLine("> exit - Exit application");
        }

        /// <inheritdoc/>
        public void HandleInput(string input) {
            if (_commandDictionary.GetDictionary().ContainsKey(input)) {
                _commandDictionary.GetDictionary().TryGetValue(input, out var result);
                if (result != null) {
                    CommandManager.Instance.ExecuteCommand(result);
                }
            } else {
                Console.Clear();
                Render();
                Console.WriteLine("(System): UNKNOWN COMMAND !");
            }
        }
    }
}
