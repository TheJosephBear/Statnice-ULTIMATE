using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface {
    internal interface IView {
        public void Initialize();
        public void Render();
        public void HandleInput(string input);
    }
}
