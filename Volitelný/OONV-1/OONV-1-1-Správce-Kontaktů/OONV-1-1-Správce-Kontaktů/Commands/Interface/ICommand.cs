using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;

namespace OONV_1_1_Správce_Kontaktů.Commands.Interface
{
    internal interface ICommand {
        public void Execute();
        public void Undo();
    }
}
