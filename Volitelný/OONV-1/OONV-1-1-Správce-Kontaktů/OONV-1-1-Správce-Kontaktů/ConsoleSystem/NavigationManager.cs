using System.Diagnostics;
using OONV_1_1_Správce_Kontaktů.Commands;
using OONV_1_1_Správce_Kontaktů.ConsoleSystem.Interface;
using OONV_1_1_Správce_Kontaktů.Interface;

namespace OONV_1_1_Správce_Kontaktů.ConsoleSystem {
    /// <summary>
    /// Manages navigation through different views in the console application using a stack.
    /// Implements the Singleton pattern to ensure only one instance exists.
    /// </summary>
    internal class NavigationManager : Singleton<NavigationManager> {
        /// <summary>
        /// Stack that holds the navigation history of views.
        /// </summary>
        private Stack<IView> _viewStack;

        /// <summary>
        /// The current active view being displayed.
        /// </summary>
        private IView _currentView;

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationManager"/> class.
        /// Creates an empty stack of views.
        /// </summary>
        public NavigationManager() {
            _viewStack = new Stack<IView>();
        }

        /// <summary>
        /// Pushes a new view onto the navigation stack and displays it.
        /// </summary>
        /// <param name="view">The view to be pushed and shown.</param>
        public void PushView(IView view) {
            Trace.WriteLine("Pushing view: " + view.ToString());
            _viewStack.Push(view);
            ShowView(view);
        }

        /// <summary>
        /// Pops the current view from the navigation stack and displays the previous view.
        /// Does nothing if only one view is on the stack.
        /// </summary>
        public void PopView() {
            if (_viewStack.Count > 1)
                Trace.WriteLine("Popping view: " + _viewStack.Peek().ToString());
            _viewStack.Pop();
            ShowView(_viewStack.Peek());
        }

        /// <summary>
        /// Displays the specified view by clearing the console and rendering the view.
        /// </summary>
        /// <param name="view">The view to be displayed.</param>
        private void ShowView(IView view) {
            Trace.WriteLine("Showing view: " + view.ToString());
            _currentView = view;
            Console.Clear();
            view.Initialize();
            view.Render();
        }

        /// <summary>
        /// Handles user input globally or delegates it to the current view.
        /// Supports global commands like "back", "undo", and "exit".
        /// </summary>
        /// <param name="input">The user input string.</param>
        public void HandleInput(string input) {
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
                    Trace.WriteLine($"{_currentView.ToString()} is handling input ");
                    _currentView.HandleInput(input);
                    break;
            }
        }

        /// <summary>
        /// Gets the current active view.
        /// </summary>
        /// <returns>The current <see cref="IView"/>.</returns>
        public IView GetCurrentView() => _currentView;
    }
}
