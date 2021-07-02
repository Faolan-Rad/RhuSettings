using System;
using System.Collections.Generic;
using System.Text;

namespace RhuSettings
{
    public class DataList : DataObject
    {
        private Dictionary<string, DataObject> list = new Dictionary<string, DataObject>();

        public Dictionary<string, DataObject>.Enumerator GetEnumerator()
        {
            return list.GetEnumerator();
        }

        public void addDataObject(string loc, DataObject val)
        {
            list.Add(loc, val);
        }

        public DataObject getDataObject(string loc)
        {
            if (list.ContainsKey(loc))
            {
                return list[loc];
            }
            else
            {
                return null;
            }
        }

        public DataList addNewList(string loc)
        {
            DataList data = new DataList();
            list.Add(loc, data);
            return data;
        }

        public DataList addList(string loc)
        {
            DataList data;
            if (list.ContainsKey(loc))
            {
                data = (DataList)list[loc];
            }
            else
            {
                data = addNewList(loc);
            }
            return data;
        }
    }
}
