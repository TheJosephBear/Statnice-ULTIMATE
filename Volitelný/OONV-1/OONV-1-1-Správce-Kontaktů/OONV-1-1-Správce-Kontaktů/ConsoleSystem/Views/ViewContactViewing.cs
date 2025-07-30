using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation;
using System.Diagnostics;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    /// <summary>
    /// View for displaying and editing a single active contact.
    /// </summary>
    public class ViewContactViewing : IView {
        private CommandDictionary _commandDictionary;
        private Contact activeContact;

        /// <inheritdoc/>
        public void Initialize() {
            Trace.WriteLine("init view contact viewing");
            activeContact = ContactManager.Instance.GetActiveContact();
            SetupCommands();
        }

        /// <summary>
        /// Sets up available commands for copying and deleting the contact.
        /// </summary>
        private void SetupCommands() {
            _commandDictionary = new CommandDictionary();
            _commandDictionary.AddInputPair("copy", new CommandCopyContact(activeContact));
            _commandDictionary.AddInputPair("delete", new CommandRemoveContact(activeContact));
        }

        /// <inheritdoc/>
        public void Render() {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("|||||||||||||-Contact Viewing-|||||||||||||");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Name: - {activeContact.Name}");
            Console.WriteLine($"Phone number: - {activeContact.PhoneNumber}");
            Console.WriteLine($"Email: - {activeContact.Email}");
            Console.WriteLine("> editname *new name* - Edit Name");
            Console.WriteLine("> editnumber *new number* - Edit Phone number");
            Console.WriteLine("> editemail *new mail* - Edit Email");
            Console.WriteLine("> delete - Delete contact");
            Console.WriteLine("> copy - Copy contact");
            Console.WriteLine("> undo - Undo last action");
            Console.WriteLine("> back - Go to previous menu");
            Console.WriteLine("> exit - Exit application");
        }

        /// <inheritdoc/>
        public void HandleInput(string input) {
            if (input.StartsWith("edit")) {
                Contact futureContact = ContactManager.Instance.CreateTemporaryCopy(activeContact);

                if (input.StartsWith("editname")) {
                    string name = input.Substring(9).Trim();
                    futureContact.Name = name;
                } else if (input.StartsWith("editnumber")) {
                    string number = input.Substring(10).Trim();
                    futureContact.PhoneNumber = number;
                } else if (input.StartsWith("editemail")) {
                    string mail = input.Substring(10).Trim();
                    futureContact.Email = mail;
                }

                CommandManager.Instance.ExecuteCommand(new CommandEditContact(activeContact, futureContact));
                Console.Clear();
                Render();
                return;
            }

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
