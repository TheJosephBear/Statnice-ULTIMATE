using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OONV_1_1_Správce_Kontaktů.Iterator.Interface {
    internal interface IIterator<T> {
        public bool HasNext();
        public T GetNext();
        public int GetCurrentIndex();
    }

    public enum ListIteratorType {
        Alphabetical
    }
}
