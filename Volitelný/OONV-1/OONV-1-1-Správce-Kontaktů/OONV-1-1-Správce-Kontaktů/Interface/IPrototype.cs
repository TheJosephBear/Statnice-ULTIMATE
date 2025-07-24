using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OONV_1_1_Správce_Kontaktů.Interface
{
    internal interface IPrototype<T>
    {
        public T Copy();
    }
}
