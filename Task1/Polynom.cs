using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Polynom
    {
        private readonly double[] coefficients;
        private static readonly double accuracy;

        public int Power => coefficients.Length;

        public double this[int index]
        {
            get
            {
                if (index > 0 || index < coefficients.Length)
                    return coefficients[index];
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        static Polynom()
        {
            accuracy = 0.0000001;
        }

        public Polynom()
        {
            coefficients = new double[0];
        }

        public Polynom(params double[] coef)
        {
            if (coef == null)
                throw new ArgumentNullException(nameof(coef));
            int i = coef.Length - 1;
            while (i > 0 && Math.Abs(coef[i]) < accuracy)
                i--;
            coefficients = new double[i + 1];
            for (int j = 0; j <= i; j++)
            {
                coefficients[j] = coef[j];
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (ReferenceEquals(obj, this))
                return true;
            return this.Equals(obj as Polynom);          
        }

        public bool Equals(Polynom p)
        {
            if (p?.Power != this.Power)
                return false;
            for (int i = 0; i < this.Power; i++)
                if (Math.Abs(p[i] - this[i]) > accuracy)
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            double hash = 0;
            for (int i = 0; i < Power; i++)
                hash += (this[i] + 5)*3 + 7;
            return (int) hash ^ Power;
        }

        public override string ToString()
        {
            if (Power == 0)
                return "";
            StringBuilder result = new StringBuilder();
            for (int i = Power - 1; i > 0; i--)
            {
                if (coefficients[i] >= 0 && i != Power - 1)
                    result.Append("+");
                result.AppendFormat($"{coefficients[i]:F2}*x^{i}");
            }
            if (coefficients[0] >= 0 && Power > 1)
                result.Append("+");
            result.Append($"{coefficients[0]:F2}");
            return result.ToString();
        }

        public static Polynom Add(Polynom lhs, Polynom rhs)
        {
            if (ReferenceEquals(lhs, null))
                throw new ArgumentNullException();
            if (ReferenceEquals(rhs, null))
                throw new ArgumentNullException();   
            int maxPower = Math.Max(lhs.Power, rhs.Power);
            double[] resultCoef = new double[maxPower];
            for (int i = 0; i < lhs.Power; i++)
                resultCoef[i] += lhs[i];
            for (int i = 0; i < rhs.Power; i++)
                resultCoef[i] += rhs[i];
            return new Polynom(resultCoef);
        }

        public static Polynom operator +(Polynom lhs, Polynom rhs)
        {
            return Add(lhs, rhs);
        }

        public Polynom Add(Polynom polynom)
        {
            return Add(this, polynom);
        }

        public static Polynom operator -(Polynom lhs, Polynom rhs)
        {
            return Substruct(lhs, rhs);
        }

        public static Polynom Substruct(Polynom lhs, Polynom rhs)
        {
            if (ReferenceEquals(lhs, null))
                throw new ArgumentNullException();
            if (ReferenceEquals(rhs, null))
                throw new ArgumentNullException();
            int maxPower = Math.Max(lhs.Power, rhs.Power);
            double[] resultCoef = new double[maxPower];
            for (int i = 0; i < lhs.Power; i++)
                resultCoef[i] += lhs[i];
            for (int i = 0; i < rhs.Power; i++)
                resultCoef[i] -= rhs[i];
            return new Polynom(resultCoef);
        }

        public Polynom Substruct(Polynom polynom)
        {
            return Substruct(this, polynom);
        }

        public static Polynom operator *(Polynom lhs, Polynom rhs)
        {
            return Multiply(lhs, rhs);
        }

        public static Polynom Multiply(Polynom lhs, Polynom rhs)
        {
            if (ReferenceEquals(lhs, null))
                throw new ArgumentNullException();
            if (ReferenceEquals(rhs, null))
                throw new ArgumentNullException();
            int maxPower = lhs.Power + rhs.Power - 1;
            double[] resultCoef = new double[maxPower];
            for (int i = 0; i < lhs.Power; i++)
                for (int j = 0; j < rhs.Power; j++)
                {
                    resultCoef[i + j] += lhs[i]*rhs[j];
                }
            return new Polynom(resultCoef);
        }

        public Polynom Multiply(Polynom polynom)
        {
            return Multiply(this, polynom);
        }
    }
}
