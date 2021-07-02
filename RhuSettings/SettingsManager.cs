using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace RhuSettings
{
    public static class SettingsManager
    {
        public static DataList getDataFromJson(string json)
        {
            JObject obj = JObject.Parse(json);
            return loadObject(obj);
        }

        public static DataList loadObject(JObject obj)
        {
            DataList value = new DataList();
            foreach (var item in obj)
            {
                DataObject val;
                switch (item.Value.Type)
                {
                    case JTokenType.Object:
                        val = loadObject(item.Value.ToObject<JObject>());
                        break;
                    default:
                        DataNode node = new DataNode();
                        node.setval(item.Value.ToObject<object>());
                        val = node;
                        break;
                }
                value.addDataObject(item.Key,val);
            }
            return value;
        }

        public static string getJsonFromData(DataList val)
        {
            JObject jobj = getJsonFromDataList(val);
            return jobj.ToString();
        }

        public static JObject getJsonFromDataList(DataList val)
        {
            JObject obj = new JObject();
            foreach (var item in val)
            {
                if (item.Value.GetType() == typeof(DataList))
                {
                    obj[item.Key] = getJsonFromDataList((DataList)item.Value);
                }
                else
                {
                    object value = ((DataNode)item.Value)?.getval();
                   obj[item.Key] = new JValue(value);
                }
            }
            return obj;
        }

        public static T loadSettingsObject<T>(params DataList[] args) where T : SettingsObject, new()
        {
            T val = new T();
            foreach (var item in args)
            {
                val = (T)loadSettingsObjectInternal(val, item);
            }
            return val;
        }

        public static SettingsObject loadSettingsObjectInternal(SettingsObject obj,DataList startList)
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                SettingsField argfield = field.GetCustomAttribute<SettingsField>();
                if (argfield != null)
                {
                    object value = field.GetValue(obj);
                    string help = argfield.help;
                    string Path = argfield.Path.ToString();
                    string[] pathparts = Path.Split('/');
                    string fieldname = field.Name;
                    Type dataType = field.FieldType;
                    DataList location;
                    if (Path == "/")
                    {
                        location = startList;
                    }
                    else
                    {
                        DataList pos = location = startList;
                        foreach (string item in pathparts)
                        {
                            if(item != "")
                            {
                                location = pos = pos.addList(item);
                            }
                        }
                    }
                    if (typeof(SettingsObject).IsAssignableFrom(dataType))
                    {
                        DataList setobj = location.addList(fieldname);
                        SettingsObject val;
                        if (value != null)
                        {
                            val = (SettingsObject)value;
                        }
                        else
                        {
                            val = (SettingsObject)Activator.CreateInstance(dataType);
                        }
                        loadSettingsObjectInternal(val, setobj);
                        field.SetValue(obj, val);
                    }
                    else
                    {
                        DataNode val = (DataNode)location.getDataObject(fieldname);

                        if(val != null)
                        {
                            if (dataType.IsEnum)
                            {
                                field.SetValue(obj, Enum.ToObject(dataType, (Int32)(Int64)val.getval()));
                            }
                            else
                            {
                                field.SetValue(obj, ChangeType(val.getval(), dataType));
                            }
                        }
                    }
                }
            }
            return obj;
        }
        public static dynamic ChangeType(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }
        public static DataList getDataListFromSettingsObject(SettingsObject obj, DataList startlist = default)
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                SettingsField argfield = field.GetCustomAttribute<SettingsField>();
                if (argfield != null)
                {
                    object value = field.GetValue(obj);
                    string help = argfield.help;
                    string Path = argfield.Path.ToString();
                    string[] pathparts = Path.Split('/');
                    string fieldname = field.Name;
                    Type dataType = field.FieldType;
                    DataList location;
                    if (Path == "/")
                    {
                        location = startlist;
                    }
                    else
                    {
                        DataList pos = location = startlist;
                        foreach (string item in pathparts)
                        {
                            if (item != "")
                            {
                                location = pos = pos.addList(item);
                            }
                        }
                    }
                    if (typeof(SettingsObject).IsAssignableFrom(dataType))
                    {
                        DataList setobj = location.addList(fieldname);
                        if (value == null) {
                            value = (SettingsObject)Activator.CreateInstance(dataType);
                            field.SetValue(obj, value);
                        }
                        getDataListFromSettingsObject((SettingsObject)value, setobj);

                    }
                    else
                    {
                        DataNode val = new DataNode();
                        val.setval(field.GetValue(obj));
                        location.addDataObject(fieldname, val);
                    }
                }
            }
            return startlist;
        }

    }
}
