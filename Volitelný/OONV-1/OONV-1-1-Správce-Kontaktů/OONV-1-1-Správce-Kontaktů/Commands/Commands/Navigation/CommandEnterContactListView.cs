using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů.Commands.Commands.Navigation
{
    internal class CommandEnterContactListView : ICommand
    {
        public void Execute()
        {
            NavigationManager.Instance.PushView(new ViewContactList());
        }

        public void Undo()
        {
            NavigationManager.Instance.PopView();
        }
    }
}
