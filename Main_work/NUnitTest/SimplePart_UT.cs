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
        public void A_Number_CreateSimpePartDoubleWithDot_Test()
        {
            SimplePart sP = new SimplePart("5.2");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
        }

        [Test]
        public void A_Number_CreateSimpePartDoubleWithComma_Test()
        {
            SimplePart sP = new SimplePart("5,2");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
        }

        [Test]
        public void A_Number_GetValueDouble_Test()
        {
            SimplePart sP = new SimplePart("5.2");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
            Assert.True(Equals(5.2, sP.GetValue()));

        }

        [Test]
        public void A_Number_GetValueInteger_Test()
        {
            SimplePart sP = new SimplePart("5");

            Assert.True(sP.IsFinal);
            Assert.True(sP.IsValue);
            Assert.AreEqual(5, sP.GetValue());

        }

        #endregion

        #region Работа со строкой

        [Test]
        public void B_Line_DeleteSpaces_Test()
        {
            SimplePart sP = new SimplePart(" X ");

            Assert.AreEqual("X", sP.Part);
        }

        #endregion

        #region Работа с переменными

        [Test]
        public void C_Variables_CreateVarableSP_Test()
        {
            SimplePart sP = new SimplePart("X");

            Assert.True(sP.IsFinal);
            Assert.False(sP.IsValue);
        }

        [Test]
        public void C_Variables_GetVariable_Test()
        {
            SimplePart sP = new SimplePart("X");

            var variables = sP.GetVariables();

            Assert.AreEqual(variables, new List<string>() { "X" });
        }

        [Test]
        public void C_Variables_FixVariable_Test()
        {
            SimplePart sP = new SimplePart("x");

            sP.FixValue("x", 5.6);

            Assert.AreEqual(5.6, sP.GetValue(), delta);
        }

        [Test]
        public void C_Variables_TwiceFixVariable_Test()
        {
            SimplePart sP = new SimplePart("x");

            sP.FixValue("x", 5.6);
            sP.FixValue("x", 8.4);

            Assert.AreEqual(8.4, sP.GetValue(), delta);
        }

        [Test]
        public void C_Variables_CanselFixVariable_Test()
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
        public void D_Nested_CreateNegativeVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");
            Assert.False(sP.IsFinal);
            Assert.False(sP.IsValue);
        }

        [Test]
        public void D_Nested_GetVariblesSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            var varibles = sP.GetVariables();

            Assert.AreEqual(varibles, new List<string>() { "x" });
        }

        [Test]
        public void D_Nested_FixValueVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            sP.FixValue("x", 2.1);

            Assert.Pass();
        }

        [Test]
        public void D_Nested_GetValueVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            sP.FixValue("x", 2.1);

            var result = sP.GetValue();

            Assert.AreEqual(-2.1, result, delta);
        }

        [Test]
        public void D_Nested_GetValueNegativeVariableSP_Test()
        {
            SimplePart sP = new SimplePart("-x");

            sP.FixValue("x", -2.1);

            var result = sP.GetValue();

            Assert.AreEqual(2.1, result, delta);
        }

        [Test]
        public void D_Nested_GetValueSinVariableSP_Test()
        {
            SimplePart sP = new SimplePart("sinx");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Sin(3.14), result, delta);
        }

        [Test]
        public void D_Nested_GetValueCosVariableSP_Test()
        {
            SimplePart sP = new SimplePart("cosx");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Cos(3.14), result, delta);
        }

        [Test]
        public void D_Nested_GetValueDegreeSP_Test()
        {
            SimplePart sP = new SimplePart("x^2");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(9, result, delta);
        }

        [Test]
        public void D_Nested_GetValueMyltSP_Test1()
        {
            SimplePart sP = new SimplePart("x*4");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(12, result, delta);
        }

        [Test]
        public void D_Nested_GetValueMyltSP_Test2()
        {
            SimplePart sP = new SimplePart("4 *x");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(12, result, delta);
        }

        [Test]
        public void D_Nested_GetValueDivSP_Test1()
        {
            SimplePart sP = new SimplePart("x/4");

            sP.FixValue("x", 8);

            var result = sP.GetValue();

            Assert.AreEqual(2, result, delta);
        }

        [Test]
        public void D_Nested_GetValueDivSP_Test2()
        {
            SimplePart sP = new SimplePart("4/ x");

            sP.FixValue("x", 8);

            var result = sP.GetValue();

            Assert.AreEqual(0.5, result, delta);
        }

        [Test]
        public void D_Nested_GetValuePlusSP_Test1()
        {
            SimplePart sP = new SimplePart("x + 4");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(7, result, delta);
        }

        [Test]
        public void D_Nested_GetValuePlusSP_Test2()
        {
            SimplePart sP = new SimplePart("4 + x");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(7, result, delta);
        }

        [Test]
        public void D_Nested_GetValueMinusSP_Test1()
        {
            SimplePart sP = new SimplePart("x - 4");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(-1, result, delta);
        }

        [Test]
        public void D_Nested_GetValueMinusSP_Test2()
        {
            SimplePart sP = new SimplePart("3.2 - x");

            sP.FixValue("x", 3);

            var result = sP.GetValue();

            Assert.AreEqual(0.2, result, delta);
        }
        
        [Test]
        public void D_Nested_Uncorrect_Order_of_Operations_Test1()
        {
            SimplePart sP = new SimplePart("-3 * x");

            sP.FixValue("x", 2.0);

            var value = sP.GetValue();

            //todo
            // Скобки в данном случае являются самой первой обнаруживаемой операцией,
            // соответственно иж раскрытие происходит в самый последний момент

            Assert.AreEqual(-6.0, value, delta);
        }

        [Test]
        public void D_Nested_Uncorrect_Order_of_Operations_Test2()
        {
            SimplePart sP = new SimplePart("(-3) * x");

            sP.FixValue("x", -3.0);

            var value = sP.GetValue();

            //todo
            // Скобки в данном случае являются самой первой обнаруживаемой операцией,
            // соответственно иж раскрытие происходит в самый последний момент

            Assert.AreEqual(9.0, value, delta);
        }

        #region Скобки

        [Test]
        public void D_B_Brackets_GetValueSinsVariableSP_Test()
        {
            SimplePart sP = new SimplePart("sin(x)");

            sP.FixValue("x", 3.14);

            var result = sP.GetValue();

            Assert.AreEqual(Math.Sin(3.14), result);
        }

        [Test]
        public void D_B_Brackets_GetValueCosVariableSP_Test()
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
        public void E_SomeOperations_PlusWithMinus_Test1()
        {
            SimplePart sP = new SimplePart("-3 + x + 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(0.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMinus_Test2()
        {
            SimplePart sP = new SimplePart("-3 + x - 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-8.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMinusWithBacket_Test1()
        {
            SimplePart sP = new SimplePart("-3 - (x + 4)");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-6.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMinusWithBacket_Test2()
        {
            SimplePart sP = new SimplePart("(-3 - x) + 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(0.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMyltiply_Test1()
        {
            SimplePart sP = new SimplePart("-3 + x * 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-7.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMyltiply_Test2()
        {
            SimplePart sP = new SimplePart("-3 * x + 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(7.0, sP.GetValue(), delta);
        }
        
        [Test]
        public void E_SomeOperations_PlusWithMyltiplyWithDegree_Test1_1()
        {
            SimplePart sP = new SimplePart("-3 ^ x + 4 * 2");

            sP.FixValue("x", 2.0);

            var value = sP.GetValue();

            //todo
            // Он воспринимает минус первый, как операцию, которая выполняется после степени. Т.е -3 не является целым

            Assert.AreEqual(17.0, value, delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMyltiplyWithDegree_Test1_2()
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
        public void E_SomeOperations_PlusWithMyltiplyWithDegree_Test1_3()
        {
            SimplePart sP = new SimplePart("x ^ -3 + 4 * 2");

            sP.FixValue("x", 2.0);

            var value = sP.GetValue();

            //todo
            // Скобки в данном случае являются самой первой обнаруживаемой операцией,
            // соответственно иж раскрытие происходит в самый последний момент

            Assert.AreEqual(8.125, value, delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMyltiplyWithDegree_Test2()
        {
            SimplePart sP = new SimplePart("-3 + x * 4 ^ 2");

            sP.FixValue("x", 2.0);

            Assert.AreEqual(29.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_PlusWithMyltiplyWithDegree_Test3()
        {
            SimplePart sP = new SimplePart("2 - 3 * x ^ 4");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-1.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_DegreeWithBrackets_Test1()
        {
            SimplePart sP = new SimplePart("-3 ^ (x + 4)");

            sP.FixValue("x", -1.0);

            Assert.AreEqual(-27.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_DegreeWithBrackets_Test2()
        {
            SimplePart sP = new SimplePart("-3 ^ (x + 4)");

            sP.FixValue("x", -2.0);

            Assert.AreEqual(9.0, sP.GetValue(), delta);
        }

        [Test]
        public void E_SomeOperations_DoublePlus_Test2()
        {
            SimplePart sP = new SimplePart("3 + x + 4");

            sP.FixValue("x", -2.0);

            Assert.AreEqual(5.0, sP.GetValue(), delta);
        }

        #endregion

        #region Работа с несколькими переменными

        [Test]
        public void F_SomeVariables_TwoVariablesCreate_Test()
        {
            SimplePart sP = new SimplePart("x + y");

            Assert.False(sP.IsFinal);
            Assert.False(sP.IsValue);
        }

        [Test]
        public void F_SomeVariables_TwoVariablesGetVariable_Test()
        {
            SimplePart sP = new SimplePart("x + y");

            Assert.AreEqual(new List<string> { "x", "y" }, sP.GetVariables());
        }

        [Test]
        public void F_SomeVariables_TwoVariablesSetValue_Test()
        {
            SimplePart sP = new SimplePart("x + y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 2.0);

            Assert.AreEqual(0.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_TwoVariablesDiv_Test()
        {
            SimplePart sP = new SimplePart("x / y");

            sP.FixValue("x", 6.0);
            sP.FixValue("y", 1.5);

            Assert.AreEqual(4.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_TwoVariablesMylt_Test()
        {
            SimplePart sP = new SimplePart("x * y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 3.5);

            Assert.AreEqual(-7.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_TwoVariablesDegree_Test1()
        {
            SimplePart sP = new SimplePart("x ^ y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 2);

            Assert.AreEqual(4.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_TwoVariablesDegree_Test2()
        {
            SimplePart sP = new SimplePart("x ^ y");

            sP.FixValue("x", -2.0);
            sP.FixValue("y", 3);

            Assert.AreEqual(-8.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_OneVariablesDegree_Test()
        {
            SimplePart sP = new SimplePart("x ^ x");

            sP.FixValue("x", 3.0);

            Assert.AreEqual(27.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_OneXTwoYSumWithMyltiply_Test()
        {
            SimplePart sP = new SimplePart("x + y * x");

            sP.FixValue("x", 2.0);
            sP.FixValue("y", 3);

            Assert.AreEqual(8.0, sP.GetValue(), delta);
        }

        [Test]
        public void F_SomeVariables_OneXTwoYSumWithMyltiplyWithBackets_Test()
        {
            SimplePart sP = new SimplePart("(x + y) * x");

            sP.FixValue("x", 2.0);
            sP.FixValue("y", 3);

            var value = sP.GetValue();

            Assert.AreEqual(10.0, value, delta);
        }

        [Test]
        public void F_ThreeVariables_X_Mylt_Y_Plus_Z_Test()
        {
            SimplePart sP = new SimplePart("x * y + z");

            sP.FixValue("x", 2.0);
            sP.FixValue("y", 3);
            sP.FixValue("z", -3);

            var value = sP.GetValue();

            Assert.AreEqual(3.0, value, delta);
        }

        [Test]
        public void F_ThreeVariables_X_Mylt_B_Y_Plus_Z_B_Test()
        {
            SimplePart sP = new SimplePart("x * (y + z)");

            sP.FixValue("x", 2.0);
            sP.FixValue("y", 3);
            sP.FixValue("z", -3);

            var value = sP.GetValue();

            Assert.AreEqual(0.0, value, delta);
        }

        #endregion
    }
}
