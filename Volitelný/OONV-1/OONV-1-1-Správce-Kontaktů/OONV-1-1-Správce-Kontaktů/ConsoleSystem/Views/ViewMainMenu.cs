using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.Commands.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    internal class ViewMainMenu : IView {

        CommandDictionary _commandDictionary;

        public ViewMainMenu() {
            Trace.WriteLine("Main menu view konstruktor called");
            _commandDictionary = new CommandDictionary();
            _commandDictionary.AddInputPair("1", new CommandEnterContactListView());
        }

        public void Render() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|||||||||||||-Main Menu-|||||||||||||");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("> 1 - Enter Contact List");
            Console.WriteLine("> undo - Undo last action");
            Console.WriteLine("> back - Go to previous menu");
            Console.WriteLine("> exit - Exit application");

        }
        public void HandleInput(string input) {
            if(_commandDictionary.GetDictionary().ContainsKey(input)) {
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
