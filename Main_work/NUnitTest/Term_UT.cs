using Main_work.Function.FunctionPart;
using Main_work.HelpClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTest
{
    public class Term_UT
    {
        private double delta = 0.00000001;
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
            Term T = new Term("3 * 4", Operation.Plus);
            Assert.Pass();
        }

        [Test]
        public void A_Base_CreateWithVariable_Test()
        {
            Term T = new Term("3 * x", Operation.Plus);
            Assert.Pass();
        }

        [Test]
        public void A_Base_CreateWithVariable_GetVariable_Test()
        {
            Term T = new Term("3 * x", Operation.Plus);

            Assert.AreEqual(new List<string> { "x" }, T.GetVariables());
        }

        [Test]
        public void A_Base_CreateWithTwoVariable_GetVariable_Test()
        {
            Term T = new Term("3 * x * y", Operation.Plus);
            
            Assert.AreEqual(new List<string> { "x", "y" }, T.GetVariables());
        }

        #endregion

        #region Одиночные переменные

        [Test]
        public void B_AloneVariables_Myltiply_Test()
        {
            Term T = new Term("3 * (-x) * 4", Operation.Plus);
            T.FixValue("x", -2.0);

            var value = T.GetValue();

            Assert.AreEqual(24.0, T.GetValue(), delta);
        }

        [Test]
        public void B_AloneVariables_MinusInBrackets_Test()
        {
            Term T = new Term("x - (- 3)", Operation.Plus);

            T.FixValue("x", -2.0);

            Assert.AreEqual(1.0, T.GetValue(), delta);
        }

        [Test]
        public void B_AloneVariables_SinValue_Test()
        {
            Term T = new Term("sin(x - 3.14)", Operation.Plus);

            T.FixValue("x", 3.14);

            Assert.AreEqual(0.0, T.GetValue(), delta);
        }

        [Test]
        public void B_AloneVariables_Deegree_Test()
        {
            Term T = new Term("x ^ (x + 2)", Operation.Plus);

            T.FixValue("x", 2);

            Assert.AreEqual(16.0, T.GetValue(), delta);
        }
        
        [Test]
        public void B_AloneVariables_AllInBrackets_Test_1()
        {
            Term T = new Term("(x + 4 * 2)", Operation.Plus);

            T.FixValue("x", 6);

            Assert.AreEqual(14.0, T.GetValue(), delta);
        }

        [Test]
        public void B_AloneVariables_AllInBrackets_Test_2()
        {
            Term T = new Term("(x / 4 * 2)", Operation.Plus);

            T.FixValue("x", 6);

            Assert.AreEqual(3.0, T.GetValue(), delta);
        }

        [Test]
        public void B_AloneVariables_AllInBrackets_Tg_Test()
        {
            Term T = new Term("(sin x / cos x)", Operation.Plus);
            double expectedValue = Math.Sin(pi / 4) / Math.Cos(pi / 4);

            T.FixValue("x", pi / 4);

            Assert.AreEqual(expectedValue, T.GetValue(), delta);
        }

        [Test]
        public void B_AloneVariables_Degree_Test()
        {
            Term T = new Term("2 ^ (-2 * x + 4)", Operation.Plus);

            T.FixValue("x",3);

            Assert.AreEqual(0.25, T.GetValue(), delta);
        }

        #endregion

        #region Двойные скобки

        [Test]
        public void С_DoobleBrackets_SimpleOperations_Test()
        {
            Term T = new Term("((x + 4) / 2) * x", Operation.Plus);

            T.FixValue("x", 3);

            Assert.AreEqual(10.5, T.GetValue(), delta);
        }

        [Test]
        public void С_DoobleBrackets_NegativeNumber_Test_1()
        {
            Term T = new Term("x * ((-3) + x)", Operation.Plus);

            T.FixValue("x", 3);

            Assert.AreEqual(0.0, T.GetValue(), delta);
        }

        [Test]
        public void С_DoobleBrackets_NegativeNumber_Test_2()
        {
            Term T = new Term("x * ((-3) + x)", Operation.Plus);

            T.FixValue("x", 5);

            Assert.AreEqual(10.0, T.GetValue(), delta);
        }

        #endregion
    }
}