using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Commands;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using System.Diagnostics;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    internal class ViewContactCreation : IView {

        private bool _contactCreationMode = false;
        private Contact _newContactInProgress;
        private ContactCreationStep _creationStep = ContactCreationStep.EnterName;

        private enum ContactCreationStep {
            EnterName,
            EnterEmail,
            EnterPhone,
            Done
        }

        public ViewContactCreation() {
            Trace.WriteLine("Contact creation view konstruktor called");
        }

        public void Render() {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("|||||||||||||-Contact Creation-|||||||||||||");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("> undo - Cancel contact creation");
            Console.WriteLine("> back - Cancel contact creation");
            Console.WriteLine("> exit - Exit application");
            Console.WriteLine("Enter name:");
        }

        public void HandleInput(string input) {
            switch (_creationStep) {
                case ContactCreationStep.EnterName:
                    _newContactInProgress = new Contact(); // New clean contact
                    _newContactInProgress.Name = input;
                    _creationStep = ContactCreationStep.EnterEmail;
                    Console.WriteLine("Enter email:");
                    break;

                case ContactCreationStep.EnterEmail:
                    _newContactInProgress.Email = input;
                    _creationStep = ContactCreationStep.EnterPhone;
                    Console.WriteLine("Enter phone number:");
                    break;

                case ContactCreationStep.EnterPhone:
                    _newContactInProgress.PhoneNumber = input;
                    _creationStep = ContactCreationStep.Done;
                    FinalizeContactCreation();
                    Console.WriteLine("Enter anything to continue...");
                    break;
                case ContactCreationStep.Done:
                    _creationStep = ContactCreationStep.EnterName;
                    NavigationManager.Instance.PopView();
                    break;
            }
        }

        private void FinalizeContactCreation() {
            var createCmd = new CommandCreateNewContact(_newContactInProgress);
            CommandManager.Instance.ExecuteCommand(createCmd);

            Console.WriteLine("(System): Contact created successfully.");

            _contactCreationMode = false;
            _newContactInProgress = null;
        }

    }
}
