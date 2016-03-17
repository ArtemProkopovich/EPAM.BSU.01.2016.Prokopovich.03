using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task1;

namespace Polynom_TestLibrary
{
    [TestFixture]
    public class Polynom_TestClass
    {
        [Test]
        [TestCase(new double[] {0, 1, 2, 3},Result= "3,00*x^3+2,00*x^2+1,00*x^1+0,00")]
        [TestCase(new double[] { 0, -1, 2, -3 }, Result = "-3,00*x^3+2,00*x^2-1,00*x^1+0,00")]
        [TestCase(new double[0], Result = "")]
        [TestCase(null,ExpectedException = typeof(ArgumentNullException))]
        public string Polynom_ToString_Test(double[] array)
        {
            return new Polynom(array).ToString();
        }

        [Test]
        [TestCase(new double[] {0, 1, 2, 3}, new double[] {5, 6, 7, 8}, Result = "11,00*x^3+9,00*x^2+7,00*x^1+5,00")]
        [TestCase(new double[] {0, 1, -2, 3}, new double[] {-5, -6, -7, 8}, Result = "11,00*x^3-9,00*x^2-5,00*x^1-5,00")]
        [TestCase(new double[] {0, 1, -2, 3}, new double[] {-5, 6}, Result = "3,00*x^3-2,00*x^2+7,00*x^1-5,00")]
        [TestCase(new double[] {0, 1}, new double[] {-5, 6, 7, -8}, Result = "-8,00*x^3+7,00*x^2+7,00*x^1-5,00")]
        [TestCase(new double[] { 0, 1, 2 }, new double[0], Result = "2,00*x^2+1,00*x^1+0,00")]
        [TestCase(new double[] { 0, 1, -2, 3 }, new double[] { 5, -1, 2, -3 }, Result = "5,00")]
        public string Polynom_Add_Test(double[] arr1, double[] arr2)
        {
            return (new Polynom(arr1) + new Polynom(arr2)).ToString();
        }

        [Test]
        [TestCase(new double[] { 0, 1, 2, 3 }, new double[] { 5, 6, 7, 8 }, Result = "-5,00*x^3-5,00*x^2-5,00*x^1-5,00")]
        [TestCase(new double[] { 0, 1, -2, 3 }, new double[] { -5, -6, -7, 8 }, Result = "-5,00*x^3+5,00*x^2+7,00*x^1+5,00")]
        [TestCase(new double[] { 0, 1, -2, 3 }, new double[] { -5, 6 }, Result = "3,00*x^3-2,00*x^2-5,00*x^1+5,00")]
        [TestCase(new double[] { 0, 1 }, new double[] { -5, 6, 7, -8 }, Result = "8,00*x^3-7,00*x^2-5,00*x^1+5,00")]
        [TestCase(new double[] { 0, 1, 2 }, new double[0], Result = "2,00*x^2+1,00*x^1+0,00")]
        [TestCase(new double[] { 0, 1, -2, 3 }, new double[] { 5, 1, -2, 3 }, Result = "-5,00")]
        public string Polynom_Substruct_Test(double[] arr1, double[] arr2)
        {
            return (new Polynom(arr1) - new Polynom(arr2)).ToString();
        }

        [Test]
        [TestCase(new double[] {0, 1}, new double[] {5, 6}, Result = "6,00*x^2+5,00*x^1+0,00")]
        [TestCase(new double[] {-1}, new double[] {5, 6, -9}, Result = "9,00*x^2-6,00*x^1-5,00")]
        [TestCase(new double[] {-1, 2}, new double[] {5, -6}, Result = "-12,00*x^2+16,00*x^1-5,00")]
        [TestCase(new double[0], new double[] {5, -6}, Result = "0,00")]
        [TestCase(new double[] {5, 6}, new double[] {0}, Result = "0,00")]
        [TestCase(new double[] {5, 6, 7}, new double[] {-1, 2}, Result = "14,00*x^3+5,00*x^2+4,00*x^1-5,00")]
        [TestCase(new double[] {5, 6, 7}, new double[] {0}, Result = "0,00")]
        public string Polynom_Multiply_Test(double[] arr1, double[] arr2)
        {
            return (new Polynom(arr1)*new Polynom(arr2)).ToString();
        }

        [Test]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, Result = true)]
        [TestCase(new double[] { -1 }, new double[] { 5, 6, -9 }, Result = false)]
        [TestCase(new double[0], new double[0], Result = true)]
        [TestCase(new double[0], null, Result = false)]
        public bool Polynom_Equals_Test(double[] arr1, double[] arr2)
        {
            if (arr2 == null)
                return new Polynom(arr1).Equals(null);
            return new Polynom(arr1).Equals(new Polynom(arr2));
        }

        [Test]
        public void Polynom_Equals_Ref_Test()
        {
            Polynom p = new Polynom(new double[] {0, 1, 2});
            object p1 = p;
            Assert.IsTrue(p.Equals(p1));
            Assert.IsFalse(p.Equals("123"));
        }

        [Test]
        [TestCase(new double[] { 0, 1 }, new double[] { 0, 1 }, Result = false)]
        [TestCase(new double[] { -1 }, new double[] { 5, 6, -9 }, Result = false)]
        public bool Polynom_GetHashCode_Test(double[] arr1, double[] arr2)
        {
            Polynom p = new Polynom(arr1);
            Polynom p1 = p;
            Assert.AreEqual(p1.GetHashCode(), p.GetHashCode());
            return p.GetHashCode() == new Polynom(arr2).GetHashCode();
            
        }
    }
}
