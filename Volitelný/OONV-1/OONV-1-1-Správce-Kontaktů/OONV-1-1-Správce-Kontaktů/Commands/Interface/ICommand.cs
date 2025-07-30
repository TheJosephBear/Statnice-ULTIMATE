namespace OONV_1_1_Správce_Kontaktů.Commands.Interface {
    /// <summary>
    /// Represents a command with executable and undoable behavior.
    /// </summary>
    public interface ICommand {
        /// <summary>
        /// Executes the command's main logic.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undoes the effect of the command.
        /// </summary>
        void Undo();
    }
}