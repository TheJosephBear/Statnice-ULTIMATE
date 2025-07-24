using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using OONV_1_1_Správce_Kontaktů.ContactSystem;
using OONV_1_1_Správce_Kontaktů.Iterator.Interface;

namespace OONV_1_1_Správce_Kontaktů.Iterator {
    internal class ContactAlphabeticalIterator : IIterator<Contact> {

        List<Contact> _list;
        public int _index = 0;

        public ContactAlphabeticalIterator(List<Contact> givenList) { 
            _list = givenList;
            ArrangeList();
        }

        void ArrangeList() {
            _list = _list.OrderBy(c => c.Name).ToList();
        }

        // interface functions
        public bool HasNext() => _index < _list.Count;
        public Contact GetNext() => _list[_index++];
        public int GetCurrentIndex() => _index;
    }
}
