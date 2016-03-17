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

        public int Power => coefficients.Length;

        public double this[int index] => coefficients[index];

        public Polynom()
        {
            coefficients = new double[0];
        }

        public Polynom(double[] coef)
        {
            if (coef == null)
                throw new ArgumentNullException(nameof(coef));
            int i = coef.Length - 1;
            while (i > 0 && coef[i] == 0)
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
                if (p[i] != this[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            return coefficients.GetHashCode();
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

        public static Polynom Add(Polynom pol1, Polynom pol2)
        {        
            int maxPower = Math.Max(pol1.Power, pol2.Power);
            double[] resultCoef = new double[maxPower];
            for (int i = 0; i < pol1.Power; i++)
                resultCoef[i] += pol1[i];
            for (int i = 0; i < pol2.Power; i++)
                resultCoef[i] += pol2[i];
            return new Polynom(resultCoef);
        }

        public static Polynom operator +(Polynom pol1, Polynom pol2)
        {
            return Add(pol1, pol2);
        }

        public Polynom Add(Polynom polynom)
        {
            return Add(this, polynom);
        }

        public static Polynom operator -(Polynom pol1, Polynom pol2)
        {
            return Substruct(pol1, pol2);
        }

        public static Polynom Substruct(Polynom pol1, Polynom pol2)
        {
            int maxPower = Math.Max(pol1.Power, pol2.Power);
            double[] resultCoef = new double[maxPower];
            for (int i = 0; i < pol1.Power; i++)
                resultCoef[i] += pol1[i];
            for (int i = 0; i < pol2.Power; i++)
                resultCoef[i] -= pol2[i];
            return new Polynom(resultCoef);
        }

        public Polynom Substruct(Polynom polynom)
        {
            return Substruct(this, polynom);
        }

        public static Polynom operator *(Polynom pol1, Polynom pol2)
        {
            return Multiply(pol1, pol2);
        }

        public static Polynom Multiply(Polynom pol1, Polynom pol2)
        {
            int maxPower = pol1.Power + pol2.Power - 1;
            double[] resultCoef = new double[maxPower];
            for (int i = 0; i < pol1.Power; i++)
                for (int j = 0; j < pol2.Power; j++)
                {
                    resultCoef[i + j] += pol1[i]*pol2[j];
                }
            return new Polynom(resultCoef);
        }

        public Polynom Multiply(Polynom polynom)
        {
            return Multiply(this, polynom);
        }
    }
}
