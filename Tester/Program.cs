using System;
using System.Reflection;
using RhuSettings;

namespace Tester
{
    public static class Program
    {
        public static bool SaveTest = true;

        public static void Main(string[] args)
        {
            Console.WriteLine("Settings Manager Test");


            Console.WriteLine("Testing Json");
            TestSettigs val = SettingsManager<TestSettigs>.loadSettingsFromString("");
            LogSettingsObject(val);

            Console.WriteLine("Testing XML");
            val = SettingsManager<TestSettigs>.loadSettingsFromString("", DataType.XML);
            LogSettingsObject(val);

            Console.WriteLine("Testing YAML");
            val = SettingsManager<TestSettigs>.loadSettingsFromString("", DataType.YAML);
            LogSettingsObject(val);

        }

        public static void LogSettingsObject(SettingsObject obj) 
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
            foreach (var field in fields)
            {
                SettingsField argfield = field.GetCustomAttribute<SettingsField>();
                if (argfield != null)
                {
                    string value = field.GetValue(obj)?.ToString();
                    string help = argfield.help;
                    string Path = argfield.Path.ToString();
                    string fieldval = field.Name;
                    string dataType = field.FieldType.Name;

                    Console.WriteLine($"FieldName:{fieldval} Path:{Path}  Value: {value} Help:{help}  Type{dataType}");
                }
            }
        }
    }
}
