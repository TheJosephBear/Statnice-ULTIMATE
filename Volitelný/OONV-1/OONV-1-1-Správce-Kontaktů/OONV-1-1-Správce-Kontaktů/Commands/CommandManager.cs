using OONV_1_1_Správce_Kontaktů.Interface;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;

namespace OONV_1_1_Správce_Kontaktů.Commands {
    /// <summary>
    /// Central class responsible for executing and managing undoable commands.
    /// </summary>
    internal class CommandManager : Singleton<CommandManager> {
        private Stack<ICommand> commands;

        /// <summary>
        /// Initializes the command manager and its internal command stack.
        /// </summary>
        public CommandManager() {
            commands = new Stack<ICommand>();
        }

        /// <summary>
        /// Executes a command and stores it for potential undo.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        public void ExecuteCommand(ICommand command) {
            command.Execute();
            commands.Push(command);
        }

        /// <summary>
        /// Undoes the last executed command if any exist.
        /// </summary>
        public void UndoLastCommand() {
            if (commands.Count > 0) {
                commands.Peek().Undo();
                commands.Pop();
            }
        }
    }
}
