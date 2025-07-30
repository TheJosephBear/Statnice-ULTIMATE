namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface {
    /// <summary>
    /// Interface for views in the console application.
    /// Defines lifecycle and input-handling methods for views.
    /// </summary>
    public interface IView {
        /// <summary>
        /// Initializes the view. Called when the view becomes active.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Renders the view output to the console.
        /// </summary>
        void Render();

        /// <summary>
        /// Handles user input specific to the view.
        /// </summary>
        /// <param name="input">Input string from the user.</param>
        void HandleInput(string input);
    }
}
