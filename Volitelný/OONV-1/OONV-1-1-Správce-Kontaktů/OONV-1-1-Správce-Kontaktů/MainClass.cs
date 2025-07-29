using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů {
    /// <summary>
    /// Entry point of the application, responsible for starting the main navigation loop.
    /// </summary>
    internal class MainClass {
        /// <summary>
        /// Main method invoked at application start.
        /// Initializes navigation and starts input handling loop.
        /// </summary>
        /// <param name="args">Command line arguments.</param>
        public static void Main(string[] args) {
            NavigationManager navigationManager = NavigationManager.Instance;

            void Start() {
                navigationManager.PushView(new ViewMainMenu());

                while (true) {
                    // Ask for input
                    string input = Console.ReadLine();

                    // Nav manager handles input
                    if (input != null) {
                        navigationManager.HandleInput(input);
                    }
                }
            }

            Start();
        }
    }
}
