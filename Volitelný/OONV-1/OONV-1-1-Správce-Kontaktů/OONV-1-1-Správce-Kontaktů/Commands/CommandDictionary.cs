using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.Commands.Interface;

namespace OONV_1_1_Správce_Kontaktů.Commands {
    internal class CommandDictionary {
        Dictionary<string, ICommand> _dictionary;

        public CommandDictionary() {
            _dictionary = new Dictionary<string, ICommand>();
        }

        public void AddInputPair(string key, ICommand value) {
            _dictionary.Add(key, value);
        }

        public Dictionary<string, ICommand> GetDictionary() {
            return _dictionary;
        }
    }
}
