using System;
using System.Collections.Generic;
using System.Text;
using RhuSettings;

namespace Tester
{
    public class TestSettigs : SettingsObject
    {
        [SettingsField(new[] { "testotest", "tester" }, "This is for info")]
        string testthing1;

        [SettingsField("This is for int")]
        int intAndpathtest;

        [SettingsField("This is for float")]
        float floatAndpathtest;

        [SettingsField("This is for Enum")]
        DataType enumTest;

        [SettingsField("This is pathTest", "/Trains")]
        string pathTestOne;

        [SettingsField("This is pathTest", "/Trains")]
        string pathTestTwo;

        [SettingsField("This is pathTest", "/Trains/SmallPath")]
        string pathTestThree;

        [SettingsField("This is big pathTest", "/This/Trains/EaSports/Flowers/Roses/Unicorns")]
        string BigPathTest;
    }
}
