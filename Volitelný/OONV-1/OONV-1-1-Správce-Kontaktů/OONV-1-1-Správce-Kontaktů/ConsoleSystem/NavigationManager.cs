using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.Interface;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem
{
    internal class NavigationManager : Singleton<NavigationManager> {
        Stack<IView> _viewStack;
        IView _currentView;

        public NavigationManager() {
            _viewStack = new Stack<IView>();
        }

        public void PushView(IView view) {
            _viewStack.Push(view);
            _currentView = view;
            Console.Clear();
            view.Render();
        }
        
        public void PopView() {
            if (_viewStack.Count > 1)
                _viewStack.Pop();
            Console.Clear();
            _viewStack.Peek().Render();
        }

        public void HandleInput(string input) {
            // Global commands, same for all views
            switch (input) {
                case "back":
                    PopView();
                    break;
                case "undo":
                    CommandManager.Instance.UndoLastCommand();
                    break;
                case "exit":
                    Environment.Exit(0);
                    break;
                default:
                    // View input handle
                    _currentView.HandleInput(input);
                    break;
            }
        }

        public IView GetCurrentView() => _currentView;
    }
}
