using System;

namespace RhuSettings
{
    public static class SettingsManager<T> where T: SettingsObject, new()
    {
        public static string SaveSettingsToSting(SettingsObject data, DataType type = DataType.JSON)
        {
            string returnString = "";
            switch (type)
            {
                case DataType.JSON:
                    break;
                case DataType.XML:
                    break;
                case DataType.YAML:
                    break;
                default:
                    break;
            }
            return returnString;
        }

        public static T loadSettingsFromString(string data, DataType type = DataType.JSON)
        {
            T obj = new T();

            switch (type)
            {
                case DataType.JSON:
                    break;
                case DataType.XML:
                    break;
                case DataType.YAML:
                    break;
                default:
                    break;
            }
            return obj;
        }
    }
}
