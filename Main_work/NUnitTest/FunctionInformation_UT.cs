using Main_work.Function;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTest
{
    public class FunctionInformation_UT
    {
        private double delta = 0.0001;
        private double pi = 3.1415926535;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AA_First_Test()
        {
            Assert.Pass();
        }

        #region Создание и базовые вещи
        
        [Test]
        public void A_Base_Create_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("3 * 4");
            Assert.Pass();
        }

        [Test]
        public void A_Base_GetTermCount_Test1()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("3 * 4");
            Assert.AreEqual(1, FI.GetTermCount());
        }

        [Test]
        public void A_Base_GetTermCount_Test2()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("3 * 4 + 5");
            Assert.AreEqual(2, FI.GetTermCount());
        }

        [Test]
        public void A_Base_GetTermCount_Test3()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("3 + 4 - 5 + 7 - 10 + 160");
            Assert.AreEqual(6, FI.GetTermCount());
        }

        [Test]
        public void A_Base_GetTermCount_Test4()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("3 + (4 - 5) * 7 - 10 ^ 160");
            Assert.AreEqual(3, FI.GetTermCount());
        }

        [Test]
        public void A_Base_TwoTermsWithOneVariables_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("3 * x + 5");
            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x" }, FI.GetVariables());
        }

        [Test]
        public void A_Base_ThreeTermsWithThreeVariables_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("y - 3 * x + z");
            Assert.AreEqual(3, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "y", "x", "z" }, FI.GetVariables());
        }

        [Test]
        public void A_Base_FixValueAndGetValue_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("x");
            FI.FixValue("x", 2.0);

            Assert.AreEqual(1, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x" }, FI.GetVariables());
            Assert.AreEqual(2.0, FI.GetValue(), delta);
        }

        [Test]
        public void A_Base_TwoVariablesFixValue_ByDict_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("x + y");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 5.0),
                new KeyValuePair<string, double>("y", 3.5)
            });


            var value =  FI.GetValue(dict);

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "y" }, FI.GetVariables());
            Assert.AreEqual(8.5, value, delta);
        }

        [Test]
        public void A_Base_Equals_DictValue_And_FixValue_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("x * z + y / p");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 6),
                new KeyValuePair<string, double>("y", 2),
                new KeyValuePair<string, double>("z", 8),
                new KeyValuePair<string, double>("p", 6),
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", 6);
            FI.FixValue("y", 2);
            FI.FixValue("z", 8);
            FI.FixValue("p", 6);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "z", "y", "p" }, FI.GetVariables());
            Assert.AreEqual(48.3333333333, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta);
        }

        #endregion

        #region Куча разных кейсов

        [Test]
        public void B_DifferentTestCase_Test_1()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("(sin (x) ^ 2) + (cos (x) ^ 2)");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 6)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", 6);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x"}, FI.GetVariables());
            Assert.AreEqual(1, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_2()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("sin x ^ 2 + cos x ^ 2");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 6)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", 6);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x" }, FI.GetVariables());
            Assert.AreEqual(1, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_3()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("sin (x) ^ 2 + cos (x) ^ 2");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 6)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", 6);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x" }, FI.GetVariables());
            Assert.AreEqual(1, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_4()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("sin(x * z) * 2 + y");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", pi),
                new KeyValuePair<string, double>("y", 3),
                new KeyValuePair<string, double>("z", 0.5)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", pi);
            FI.FixValue("y", 3);
            FI.FixValue("z", 0.5);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "z", "y" }, FI.GetVariables());
            Assert.AreEqual(5.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_5()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("y + sin(x * z) * 2");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", pi),
                new KeyValuePair<string, double>("y", 3),
                new KeyValuePair<string, double>("z", 0.5)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", pi);
            FI.FixValue("y", 3);
            FI.FixValue("z", 0.5);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "y", "x", "z" }, FI.GetVariables());
            Assert.AreEqual(5.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_6()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("cos(z + w) ^ 2 - x * p + y");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 4),
                new KeyValuePair<string, double>("y", 11),
                new KeyValuePair<string, double>("z", pi / 2),
                new KeyValuePair<string, double>("w", pi / 2),
                new KeyValuePair<string, double>("p", 3)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", 4);
            FI.FixValue("y", 11);
            FI.FixValue("z", pi / 2);
            FI.FixValue("w", pi / 2);
            FI.FixValue("p", 3);

            var secondValue = FI.GetValue();

            Assert.AreEqual(3, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "z", "w", "x", "p", "y" }, FI.GetVariables());
            Assert.AreEqual(0.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_7()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("2 ^( x ^ (y^2) ) - 2 ^ ( y ^ (x^2))");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", 2),
                new KeyValuePair<string, double>("y", 1)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", 2);
            FI.FixValue("y", 1);

            var secondValue = FI.GetValue();

            Assert.AreEqual(2, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "y" }, FI.GetVariables());
            Assert.AreEqual(2.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");
        }

        [Test]
        public void B_DifferentTestCase_Test_8()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("2 * (sin (x)) + 3 * (cos(y)) - (sin (z) / cos(z))");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", pi / 2),
                new KeyValuePair<string, double>("y", pi),
                new KeyValuePair<string, double>("z", pi / 4)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", pi / 2);
            FI.FixValue("y", pi);
            FI.FixValue("z", pi / 4);

            var secondValue = FI.GetValue();

            Assert.AreEqual(3, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "y", "z" }, FI.GetVariables());
            Assert.AreEqual(-2.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");

        }
        
        #endregion

        //todo Особый кейс, когда несколько операторов идут друг за другом

        [Test]
        public void E_SomeOperators_One_After_Another_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("2 * sin (x) + 3 * cos(y) - sin (z) / cos(z)");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", pi / 2),
                new KeyValuePair<string, double>("y", pi),
                new KeyValuePair<string, double>("z", pi / 4)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", pi / 2);
            FI.FixValue("y", pi);
            FI.FixValue("z", pi / 4);

            var secondValue = FI.GetValue();

            Assert.AreEqual(3, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "y", "z" }, FI.GetVariables());
            Assert.AreEqual(-2.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");

        }

        //todo подумать над валидацией

        [Test]
        public void E_Sim_Instead_Sin_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("2 * sim (x) + 3 * cos(y) - sin (z) / cos(z)");
            var dict = new Dictionary<string, double>(new List<KeyValuePair<string, double>>{
                new KeyValuePair<string, double>("x", pi / 2),
                new KeyValuePair<string, double>("y", pi),
                new KeyValuePair<string, double>("z", pi / 4)
            });

            var firstValue = FI.GetValue(dict);

            FI.FixValue("x", pi / 2);
            FI.FixValue("y", pi);
            FI.FixValue("z", pi / 4);

            var secondValue = FI.GetValue();

            Assert.AreEqual(3, FI.GetTermCount());
            Assert.AreEqual(new List<string>() { "x", "y", "z" }, FI.GetVariables());
            Assert.AreEqual(-2.0, firstValue, delta);
            Assert.AreEqual(firstValue, secondValue, delta, "Несовпадение значений при вычислении функции двумя способами");

        }

        [Test]
        public void E_Test()
        {
            FunctionInformation_V2 FI = new FunctionInformation_V2("20 * -(x + 4)");
            Assert.AreEqual(1, FI.GetTermCount());
        }
    }
}
