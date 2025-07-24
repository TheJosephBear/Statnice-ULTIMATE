using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OONV_1_1_Správce_Kontaktů.Interface;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;

namespace OONV_1_1_Správce_Kontaktů.Commands {
    internal class CommandManager : Singleton<CommandManager> {

        Stack<Interface.ICommand> commands;

        public CommandManager() {
            commands = new Stack<Interface.ICommand>();
        }

        public void ExecuteCommand(Interface.ICommand command) {
            command.Execute();
        }

        public void UndoLastCommand() {
            if(commands.Count > 0) {
                commands.Peek().Undo();
                commands.Pop();
            }
        }

    }
}
