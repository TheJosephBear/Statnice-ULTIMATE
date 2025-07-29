using OONV_1_1_Správce_Kontaktů.Commands.Interface;

namespace OONV_1_1_Správce_Kontaktů.Commands {
    /// <summary>
    /// Maps user input strings to corresponding command instances.
    /// </summary>
    internal class CommandDictionary {
        private Dictionary<string, ICommand> _dictionary;

        /// <summary>
        /// Initializes a new command dictionary.
        /// </summary>
        public CommandDictionary() {
            _dictionary = new Dictionary<string, ICommand>();
        }

        /// <summary>
        /// Adds a user input string mapped to a command.
        /// </summary>
        /// <param name="key">The user input string.</param>
        /// <param name="value">The command to execute.</param>
        public void AddInputPair(string key, ICommand value) {
            _dictionary.Add(key, value);
        }

        /// <summary>
        /// Gets the internal dictionary for command lookup.
        /// </summary>
        /// <returns>A dictionary of input-command pairs.</returns>
        public Dictionary<string, ICommand> GetDictionary() {
            return _dictionary;
        }
    }
}
