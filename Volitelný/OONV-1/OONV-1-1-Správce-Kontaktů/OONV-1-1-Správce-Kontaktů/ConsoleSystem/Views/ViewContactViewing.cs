using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Commands;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    internal class ViewContactViewing : IView {

        CommandDictionary _commandDictionary;

        public void Initialize() {
            SetupCommands();
        }

        void SetupCommands() {
            _commandDictionary = new CommandDictionary();
            // Edit name
            // Edit phone number
            // Edit email
            // Copy contact
        }

        public void Render() {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("|||||||||||||-Contact Viewing-|||||||||||||");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("> 1 - Edit Name");
            Console.WriteLine("> 2 - Edit Phone number");
            Console.WriteLine("> 3 - Edit Email");
            Console.WriteLine("> undo - Undo last action");
            Console.WriteLine("> back - Go to previous menu");
            Console.WriteLine("> exit - Exit application");
        }

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
