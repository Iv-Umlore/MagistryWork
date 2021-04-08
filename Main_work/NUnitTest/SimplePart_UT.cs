using Main_work.Function.FunctionPart;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTest
{
    public class SimplePart_UT
    {
        private double delta = 0.00000001;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void FirstTest()
        {
            Assert.Pass();
        }        

        #region Работа с числами

        [Test]
        public void CreateSimpePartDoubleWithDot_Test()
        {
            SimplePart sP = new SimplePart("5.2");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
        }

        [Test]
        public void CreateSimpePartDoubleWithComma_Test()
        {
            SimplePart sP = new SimplePart("5,2");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
        }

        [Test]
        public void GetValueDouble_Test()
        {
            SimplePart sP = new SimplePart("5.2");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
            Assert.True(Equals(5.2, sP.GetValue()));

        }

        [Test]
        public void GetValueInteger_Test()
        {
            SimplePart sP = new SimplePart("5");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
            Assert.AreEqual(5, sP.GetValue());

        }

        #endregion

        #region Работа со строкой

        [Test]
        public void DeleteSpaces_Test()
        {
            SimplePart sP = new SimplePart(" X ");

            Assert.AreEqual("X", sP.Part);
        }

        #endregion

        #region Работа с переменными

        [Test]
        public void CreateVarableSP_Test()
        {
            SimplePart sP = new SimplePart("X");

            Assert.True(sP.IsFinal);
            Assert.False(sP.IsValue);
        }

        [Test]
        public void GetVariable_Test()
        {
            SimplePart sP = new SimplePart("X");

            var variables = sP.GetVariables();

            Assert.AreEqual(variables, new List<string>() { "X" });
        }

        [Test]
        public void FixVariable_Test()
        {
            SimplePart sP = new SimplePart("x");

            sP.FixValue("x", 5.6);

            Assert.AreEqual(5.6, sP.GetValue(), delta);
        }

        [Test]
        public void TwiceFixVariable_Test()
        {
            SimplePart sP = new SimplePart("x");

            sP.FixValue("x", 5.6);
            sP.FixValue("x", 8.4);

            Assert.AreEqual(8.4, sP.GetValue(), delta);
        }

        [Test]
        public void CanselFixVariable_Test()
        {
            SimplePart sP = new SimplePart("x");

            sP.FixValue("x", 5.6);
            Assert.True(sP.IsValue);

            sP.CanselFix("x");
            Assert.False(sP.IsValue);
        }

        #endregion

        #region Работа с вложенными SimplePart

        [Test]
        public void CreateNegativeVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");
            Assert.False(sP.IsFinal);
            Assert.False(sP.IsValue);
        }

        [Test]
        public void GetVariblesNestedSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            var varibles = sP.GetVariables();

            Assert.AreEqual(varibles, new List<string>() { "x" });
        }

        [Test]
        public void FixValueNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            sP.FixValue("x", 2.1);

            Assert.Pass();
        }

        [Test]
        public void GetValueNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            sP.FixValue("x", 2.1);

            var result = sP.GetValue();

            Assert.AreEqual(-2.1, result, delta);
        }

        [Test]
        public void GetValueNegativeNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            sP.FixValue("x", -2.1);

            var result = sP.GetValue();

            Assert.AreEqual(2.1, result, delta);
        }

        [Test]
        public void GetValueSinNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("sinx");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Sin(3.14), result, delta);
        }

        [Test]
        public void GetValueCosNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("cosx");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Cos(3.14), result, delta);
        }

        [Test]
        public void GetValueDegreeSP_Test()
        {
            SimplePart sP = new SimplePart("x^2");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(9, result, delta);
        }

        [Test]
        public void GetValueMyltSP_Test1()
        {
            SimplePart sP = new SimplePart("x*4");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(12, result, delta);
        }

        [Test]
        public void GetValueMyltSP_Test2()
        {
            SimplePart sP = new SimplePart("4 *x");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(12, result, delta);
        }

        [Test]
        public void GetValueDivSP_Test1()
        {
            SimplePart sP = new SimplePart("x/4");

            sP.FixValue("x", 8);

            var result = sP.GetValue();

            Assert.AreEqual(2, result, delta);
        }

        [Test]
        public void GetValueDivSP_Test2()
        {
            SimplePart sP = new SimplePart("4/ x");

            sP.FixValue("x", 8);

            var result = sP.GetValue();

            Assert.AreEqual(0.5, result, delta);
        }

        [Test]
        public void GetValuePlusSP_Test1()
        {
            SimplePart sP = new SimplePart("x + 4");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(7, result, delta);
        }

        [Test]
        public void GetValuePlusSP_Test2()
        {
            SimplePart sP = new SimplePart("4 + x");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(7, result, delta);
        }

        [Test]
        public void GetValueMinusSP_Test1()
        {
            SimplePart sP = new SimplePart("x - 4");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(-1, result, delta);
        }

        [Test]
        public void GetValueMinusSP_Test2()
        {
            SimplePart sP = new SimplePart("3.2 - x");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(0.2, result, delta);
        }

        #region Скобки

        [Test]
        public void GetValueSinNestedBracketsVariableSP_Test()
        {
            SimplePart sP = new SimplePart("sin(x)");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Sin(3.14), result);
        }

        [Test]
        public void GetValueCosNestedBracketsVariableSP_Test()
        {
            SimplePart sP = new SimplePart("cos(x)");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Cos(3.14), result);
        }


        #endregion

        #endregion

        #region Работа с несколькими знаками

        [Test]
        public void PlusWithMinus_Test1()
        {
            SimplePart sP = new SimplePart("-3 + x + 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(0.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMinus_Test2()
        {
            SimplePart sP = new SimplePart("-3 + x - 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-8.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMinusWithBacket_Test1()
        {
            SimplePart sP = new SimplePart("-3 - (x + 4)");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-6.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMinusWithBacket_Test2()
        {
            SimplePart sP = new SimplePart("(-3 - x) + 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(0.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMyltiply_Test1()
        {
            SimplePart sP = new SimplePart("-3 + x * 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-7.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMyltiply_Test2()
        {
            SimplePart sP = new SimplePart("-3 * x + 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(7.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMyltiplyWithDegree_Test1()
        {
            SimplePart sP = new SimplePart("-3 ^ x + 4 * 2");

            sP.FixValue("x", 2.0);

            var value = sP.GetValue();

            //todo
            // Он воспринимает минус первый, как операцию, которая выполняется после степени. Т.е -3 не является целым

            Assert.AreEqual(17.0, value, delta);
        }

        [Test]
        public void PlusWithMyltiplyWithDegree_Test1_1()
        {
            SimplePart sP = new SimplePart("(-3) ^ x + 4 * 2");

            sP.FixValue("x", 2.0);

            var value = sP.GetValue();

            //todo
            // Скобки в данном случае являются самой первой обнаруживаемой операцией,
            // соответственно иж раскрытие происходит в самый последний момент

            Assert.AreEqual(17.0, value, delta);
        }

        [Test]
        public void PlusWithMyltiplyWithDegree_Test2()
        {
            SimplePart sP = new SimplePart("-3 + x * 4 ^ 2");

            sP.FixValue("x", 2.0);

            Assert.AreEqual(29.0, sP.GetValue(), delta);
        }

        [Test]
        public void PlusWithMyltiplyWithDegree_Test3()
        {
            SimplePart sP = new SimplePart("2 - 3 * x ^ 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-1.0, sP.GetValue(), delta);
        }

        [Test]
        public void DegreeWithBrackets_Test1()
        {
            SimplePart sP = new SimplePart("-3 ^ (x + 4)");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-27.0, sP.GetValue(), delta);
        }

        [Test]
        public void DegreeWithBrackets_Test2()
        {
            SimplePart sP = new SimplePart("-3 ^ (x + 4)");

            sP.FixValue("x", -2.0);

            Assert.AreEqual(9.0, sP.GetValue(), delta);
        }

        [Test]
        public void DoublePlus_Test2()
        {
            SimplePart sP = new SimplePart("3 + x + 4");

            sP.FixValue("x", -2.0);

            Assert.AreEqual(5.0, sP.GetValue(), delta);
        }

        #endregion

        #region Работа с несколькими переменными

        [Test]
        public void TwoVariablesCreate_Test()
        {
            SimplePart sP = new SimplePart("x + y");

            Assert.False(sP.IsFinal);
            Assert.False(sP.IsValue);
        }

        [Test]
        public void TwoVariablesSetValue_Test()
        {
            SimplePart sP = new SimplePart("x + y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 2.0);

            Assert.AreEqual(0.0, sP.GetValue(), delta);
        }

        [Test]
        public void TwoVariablesDiv_Test()
        {
            SimplePart sP = new SimplePart("x / y");

            sP.FixValue("x", 6.0);
            sP.FixValue("y", 1.5);

            Assert.AreEqual(4.0, sP.GetValue(), delta);
        }

        [Test]
        public void TwoVariablesMylt_Test()
        {
            SimplePart sP = new SimplePart("x * y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 3.5);

            Assert.AreEqual(-7.0, sP.GetValue(), delta);
        }

        [Test]
        public void TwoVariablesDegree_Test1()
        {
            SimplePart sP = new SimplePart("x ^ y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 2);

            Assert.AreEqual(4.0, sP.GetValue(), delta);
        }

        [Test]
        public void TwoVariablesDegree_Test2()
        {
            SimplePart sP = new SimplePart("x ^ y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 3);

            Assert.AreEqual(-8.0, sP.GetValue(), delta);
        }

        #endregion
    }
}
