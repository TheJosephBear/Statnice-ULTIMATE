using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.Commands.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    internal class ViewContactList : IView {

        CommandDictionary _commandDictionary;
        bool _renderClassicList = true;

        public void Initialize() {
            Trace.WriteLine("_commandDictionary initialize in list: ");
            SetupCommands();
            _renderClassicList = true;
            // Načíst uložené kontakty
        }

        void SetupCommands() {
            _commandDictionary = new CommandDictionary();
            _commandDictionary.AddInputPair("1", new CommandEnterContactCreationView());
            // Otevřit kontakt
            // Vyhledávání
            // Uložit kontakty
            // Filtrace
            _commandDictionary.AddInputPair("2", new CommandRenderAlphabeticalContacts(this));
        }

        public void Render() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|||||||||||||-Contact List-|||||||||||||");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("> 1 - Add new contact");
            Console.WriteLine("> 2 - Filter alphabetically");
            Console.WriteLine("> undo - Undo last action");
            Console.WriteLine("> back - Go to previous menu");
            Console.WriteLine("> exit - Exit application");
            if (_renderClassicList) RenderClassicList();
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

        public void RenderClassicList() {
            int i = 0;
            foreach (Contact contact in ContactManager.Instance.GetContactList()) {
                Console.WriteLine($"({i}): {contact.Name}");
                i++;
            }
        }

        public void RenderAlphabeticalList() {
            Console.Clear();
            _renderClassicList = false;
            Render();
            IIterator<Contact> iterator = ContactManager.Instance.GetContactIterator(Iterator.Interface.ListIteratorType.Alphabetical);
            while (iterator.HasNext()) { // while protože mám vlastní iterátor a nespoléhám se na to že prvky jsou ienumerable
                var contact = iterator.GetNext();
                Console.WriteLine($"({iterator.GetCurrentIndex()}): {contact.Name}");
            }
            _renderClassicList = true;
        }
    }
}
