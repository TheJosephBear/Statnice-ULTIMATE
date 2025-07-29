using OONV_1_1_Správce_Kontaktů.ContactSystem;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;

namespace OONV_1_1_Správce_Kontaktů.Iterator {
    /// <summary>
    /// Iterator that iterates over a list of contacts in alphabetical order by name.
    /// </summary>
    internal class ContactAlphabeticalIterator : IIterator<Contact> {
        private List<Contact> _list;
        private int _index = 0;

        /// <summary>
        /// Initializes a new instance of <see cref="ContactAlphabeticalIterator"/> with the given list.
        /// Sorts the list alphabetically by contact name.
        /// </summary>
        /// <param name="givenList">List of contacts to iterate over.</param>
        public ContactAlphabeticalIterator(List<Contact> givenList) {
            _list = givenList;
            ArrangeList();
        }

        /// <summary>
        /// Sorts the internal list alphabetically by contact name.
        /// </summary>
        private void ArrangeList() {
            _list = _list.OrderBy(c => c.Name).ToList();
        }

        /// <inheritdoc/>
        public bool HasNext() => _index < _list.Count;

        /// <inheritdoc/>
        public Contact GetNext() => _list[_index++];

        /// <inheritdoc/>
        public int GetCurrentIndex() => _index;
    }
}
