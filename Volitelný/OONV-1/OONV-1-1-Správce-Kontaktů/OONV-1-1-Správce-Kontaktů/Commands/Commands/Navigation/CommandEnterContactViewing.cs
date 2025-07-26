using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;
using OONV_1_1_Správce_Kontaktů.ContactSystem;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation {
    internal class CommandEnterContactViewing : ICommand {

        Contact _contactReff;

        public CommandEnterContactViewing(Contact contact) {
            _contactReff = contact;
        }

        public void Execute() {
            ContactManager.Instance.SetActiveContact(_contactReff);
            NavigationManager.Instance.PushView(new ViewContactViewing());
        }

        public void Undo() {
            NavigationManager.Instance.PopView();
        }
    }
}
