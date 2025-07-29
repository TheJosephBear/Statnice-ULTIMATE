using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using System.Diagnostics;
using OONV_1_1_Správce_Kontaktů.Commands.Commands.ContactManipulation;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views {
    /// <summary>
    /// View for creating a new contact step-by-step via user input.
    /// </summary>
    internal class ViewContactCreation : IView {
        private Contact _newContactInProgress;
        private ContactCreationStep _creationStep;

        /// <summary>
        /// Steps of the contact creation wizard.
        /// </summary>
        private enum ContactCreationStep {
            EnterName,
            EnterEmail,
            EnterPhone,
            Done
        }

        /// <inheritdoc/>
        public void Initialize() {
            Trace.WriteLine(" initialize in creation ");
            _newContactInProgress = new Contact();
            _creationStep = ContactCreationStep.EnterName;
        }

        /// <inheritdoc/>
        public void Render() {
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("|||||||||||||-Contact Creation-|||||||||||||");
            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("> undo - Cancel contact creation");
            Console.WriteLine("> back - Cancel contact creation");
            Console.WriteLine("> exit - Exit application");
            Console.WriteLine("Enter name:");
        }

        /// <inheritdoc/>
        public void HandleInput(string input) {
            switch (_creationStep) {
                case ContactCreationStep.EnterName:
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

        /// <summary>
        /// Finalizes the creation of a contact and registers the creation command.
        /// </summary>
        private void FinalizeContactCreation() {
            var createCmd = new CommandCreateNewContact(_newContactInProgress);
            CommandManager.Instance.ExecuteCommand(createCmd);

            Console.WriteLine("(System): Contact created successfully.");
        }
    }
}
