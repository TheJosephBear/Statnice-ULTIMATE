using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.Commands.Commands;
using OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation;
using OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    internal class ViewContactList : IView {

        CommandDictionary _commandDictionary;
        bool _renderClassicList = true;
        Dictionary<int, Contact> ContactIdPairs = new Dictionary<int, Contact>(); // For selecting contacts in commands

        public void Initialize() {
            Trace.WriteLine("_commandDictionary initialize in list: ");
            SetupCommands();
            _renderClassicList = true;
            // Načíst uložené kontakty
        }

        void SetupCommands() {
            _commandDictionary = new CommandDictionary();
            _commandDictionary.AddInputPair("1", new CommandEnterContactCreationView());
            _commandDictionary.AddInputPair("2", new CommandRenderAlphabeticalContacts(this));
            _commandDictionary.AddInputPair("3", new CommandSaveContacts());
            // Vyhledávání

        }

        public void Render() {
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("|||||||||||||-Contact List-|||||||||||||");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("> open *contact id* - open contact for editing");
            Console.WriteLine("> 1 - Add new contact");
            Console.WriteLine("> 2 - Filter alphabetically");
            Console.WriteLine("> 3 - Save contacts");
            Console.WriteLine("> undo - Undo last action");
            Console.WriteLine("> back - Go to previous menu");
            Console.WriteLine("> exit - Exit application");
            if (_renderClassicList) RenderClassicList();
        }

        public void HandleInput(string input) {
            // Checking dictionary with contacts type of command
            if (input.StartsWith("open ")) {
                string numberPart = input.Substring(5).Trim();
                if (int.TryParse(numberPart, out int id) && ContactIdPairs.ContainsKey(id)) {
                    Contact foundContact = ContactIdPairs[id];
                    CommandEnterContactViewing viewCommand = new CommandEnterContactViewing(foundContact);
                    viewCommand.Execute();
                    return;
                } else {
                    Console.Clear();
                    Render();
                    Console.WriteLine("Invalid contact ID.");
                    return;
                }
            }
            // Classic commands
            if (_commandDictionary.GetDictionary().ContainsKey(input)) {
                _commandDictionary.GetDictionary().TryGetValue(input, out var result);
                if (result != null) {
                    Console.Clear();
                    Render();
                    CommandManager.Instance.ExecuteCommand(result);
                }
            } else {
                Trace.WriteLine($"Rerendering");
                Console.Clear();
                Render();
                Console.WriteLine("(System): UNKNOWN COMMAND !");
            }

        }

        public void RenderClassicList() {
            ContactIdPairs = new Dictionary<int, Contact>();
            int i = 0;
            foreach (Contact contact in ContactManager.Instance.GetContactList()) {
                Console.WriteLine($"({i}): {contact.Name}");
                if(!ContactIdPairs.ContainsKey(i)) ContactIdPairs.Add(i, contact);
                i++;
            }
        }

        public void RenderAlphabeticalList() {
            Console.Clear();
            ContactIdPairs = new Dictionary<int, Contact>();
            _renderClassicList = false;
            Render();
            IIterator<Contact> iterator = ContactManager.Instance.GetContactIterator(Iterator.Interface.ListIteratorType.Alphabetical);
            while (iterator.HasNext()) { // while protože mám vlastní iterátor a nespoléhám se na to že prvky jsou ienumerable
                var contact = iterator.GetNext();
                Console.WriteLine($"({iterator.GetCurrentIndex()}): {contact.Name}");
                if (!ContactIdPairs.ContainsKey(iterator.GetCurrentIndex())) ContactIdPairs.Add(iterator.GetCurrentIndex(), contact);
            }
            _renderClassicList = true;
        }
    }
}
