using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands {
    internal class CommandRenderAlphabeticalContacts : ICommand {

        ViewContactList _viewInstanceReff;

        public CommandRenderAlphabeticalContacts(ViewContactList viewContactListReff) {
            _viewInstanceReff = viewContactListReff;
        }

        public void Execute() {
            _viewInstanceReff.RenderAlphabeticalList();
        }

        public void Undo() {
            _viewInstanceReff.Render();
        }
    }
}
