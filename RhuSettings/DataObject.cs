using System;
using System.Collections.Generic;
using System.Text;

namespace RhuSettings
{
    public class DataObject
    {
        string field;

        public virtual void loadData(string _field,object data)
        {
            field = _field;
        }
    }
}
