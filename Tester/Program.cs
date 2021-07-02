using System;
using System.IO;
using System.Reflection;
using RhuSettings;

namespace Tester
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("Settings Manager Test");

            TestSettigs set;
            if (File.Exists("test.json"))
            {
                string text = File.ReadAllText("test.json");
                DataList liet = SettingsManager.getDataFromJson(text);
                DataList es = SettingsManager.getDataFromJson(@"{'screenThing':{ 'resalution': {
      'x': 110
    }
        }
    }");
                set = SettingsManager.loadSettingsObject<TestSettigs>(liet,es);
            }
            else
            {
                set = new TestSettigs();
            }
            DataList e = SettingsManager.getDataListFromSettingsObject(set,new DataList());
            string jsonstring = SettingsManager.getJsonFromData(e);
            File.WriteAllTextAsync("test.json", jsonstring);
            Console.WriteLine("Json: "+ jsonstring);

        }

    }
}
