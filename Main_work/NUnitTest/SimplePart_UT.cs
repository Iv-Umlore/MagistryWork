using Main_work.Function.FunctionPart;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace NUnitTest
{
    public class SimplePart_UT
    {
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

            Assert.AreEqual(5.6, sP.GetValue());
        }

        [Test]
        public void TwiceFixVariable_Test()
        {
            SimplePart sP = new SimplePart("x");

            sP.FixValue("x", 5.6);
            sP.FixValue("x", 8.4);

            Assert.AreEqual(8.4, sP.GetValue());
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

            Assert.AreEqual(-2.1, result);
        }

        [Test]
        public void GetValueSinNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("sinx");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Sin(3.14), result);
        }

        [Test]
        public void GetValueCosNestedVariableSP_Test()
        {
            SimplePart sP = new SimplePart("cosx");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Cos(3.14), result);
        }

        [Test]
        public void GetValueDegreeSP_Test()
        {
            SimplePart sP = new SimplePart("x^2");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(9, result);
        }

        #endregion
    }
}
