using System;
using System.Collections.Generic;
using System.Text;

namespace RhuSettings
{
    public class DataNode : DataObject
    {
        private object val = null;

        public object getval()
        {
            return val;
        }
        public void setval(object newval)
        {
             val = newval;
        }
    }
}
