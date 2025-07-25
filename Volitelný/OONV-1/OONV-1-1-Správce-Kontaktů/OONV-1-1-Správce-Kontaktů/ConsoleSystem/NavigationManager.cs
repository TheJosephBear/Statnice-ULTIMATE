using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Trace.WriteLine("Pushing view: " + view.ToString());
            _viewStack.Push(view);
            ShowView(view);
        }
        
        public void PopView() {
            if (_viewStack.Count > 1)
                Trace.WriteLine("Popping view: " + _viewStack.Peek().ToString());
                _viewStack.Pop();
            ShowView(_viewStack.Peek());
        }

        void ShowView(IView view) {
            Trace.WriteLine("Showing view: " + view.ToString());
            _currentView = view;
            Console.Clear();
            view.Initialize();
            view.Render();
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
                    Trace.WriteLine($"{_currentView.ToString()} is handling input ");
                    _currentView.HandleInput(input);
                    break;
            }
        }

        public IView GetCurrentView() => _currentView;
    }
}
