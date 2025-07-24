using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Views;

namespace OONV_1_1_Správce_Kontaktů {
    internal class MainClass {
        public static void Main(String[] args) {

            NavigationManager navigationManager = NavigationManager.Instance ;

            void Start() {
                navigationManager.PushView(new ViewMainMenu());
                while (true) {
                    // Ask for input
                    string input = Console.ReadLine();
                    // Nav manager handles input
                    if(input != null) {
                        navigationManager.HandleInput(input);
                    }
                }
            }

            Start();
            
        }
    }
}
