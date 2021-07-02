using System;
using System.Collections.Generic;
using System.Text;
using RhuSettings;

namespace Tester
{
    public class TestSettigs : SettingsObject
    {
        [SettingsField(new[] { "testotest", "tester" }, "This is for info")]
        string testthing1 = "string";

        [SettingsField("This is for int")]
        int intAndpathtest = 22;

        [SettingsField("This is for float")]
        float floatAndpathtest = 34.2f;

        [SettingsField("This is for Enum")]
        DataType enumTest = DataType.JSON;

        [SettingsField("This is pathTest", "/Trains")]
        string pathTestOne = "strap";

        [SettingsField("This is pathTest", "/Trains")]
        string pathTestTwo;

        [SettingsField("This is pathTest", "/Trains/SmallPath")]
        string pathTestThree;

        [SettingsField("This is big pathTest", "/This/Trains/EaSports/Flowers/Roses/Unicorns")]
        string BigPathTest = "trains";

        [SettingsField("This is testchild")]
        childObjectOne testChild;

        [SettingsField("This is testchild")]
        childObjectTwo testChild2;

        [SettingsField("This is testchild")]
        screenSettings screenThing;
    }

    public class childObjectOne : SettingsObject
    {
        [SettingsField("This is pathTest", "/Trains")]
        string pathTestTwo = "sports";

        [SettingsField("This is pathTest", "/Trains/SmallPath")]
        string pathTestThree = "es";
    }

    public class screenSettings : SettingsObject
    {
        [SettingsField("This is pathTest", "/resalution")]
        int x = 0;

        [SettingsField("This is pathTest", "/resalution")]
        int y = 0;

        [SettingsField("This is pathTest")]
        bool auto = true;
    }


    public class childObjectTwo : SettingsObject
    {
        [SettingsField("This is pathTest", "/es")]
        string pathTestTwo;

        [SettingsField("This is pathTest", "/egfsrtse/sef")]
        string pathTestThree;
    }
}
