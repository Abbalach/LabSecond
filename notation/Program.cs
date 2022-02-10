using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notation
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("число:");
                decimal number = decimal.Parse(Console.ReadLine());

                Console.WriteLine("в основу:");
                int Base = int.Parse(Console.ReadLine());

                var NewNumber = ToAnotherSystem(number, Base);

                Console.WriteLine();
                Console.WriteLine(NewNumber);
            }
            
           
        }

        public static decimal DecimalPow(decimal Base, decimal grade)
        {
            double answer = Math.Pow(Convert.ToDouble(Base), Convert.ToDouble(grade));

            return Convert.ToDecimal(answer);
        }

        public static string ToAnotherSystem(decimal number, int Base)
        {

            decimal grade = number;
            bool helper = true;

            int counter = 0;
            while (helper)
            {
                if (grade / Base < 1)
                {
                    break;
                }
                grade = grade / Base;
                counter++;
            }
            grade = counter;
            helper = true;
            List<decimal> coeffs = new List<decimal>();
            List<decimal> AddCoeffs = new List<decimal>();


            AddCoeffs.Add(number / DecimalPow(Base, grade));
            coeffs.Add(Math.Truncate(AddCoeffs[0]));

            counter = 0;

            int pointCoordinate = 10000;
            while (helper)
            {
                if (counter == grade + 1)
                {
                    pointCoordinate = Convert.ToInt32(grade + 1);
                }
                if (counter >= grade && AddCoeffs[counter] == coeffs[counter])
                {
                    break;
                }
                if (counter >= grade + 8)
                {
                    break;
                }

                AddCoeffs.Add(Base * (AddCoeffs[counter] - coeffs[counter]));
                coeffs.Add(Math.Truncate(AddCoeffs[counter + 1]));
                counter++;
            }
            string NewNumber = "";

            for (int i = 0; i < coeffs.Count; i++)
            {
                if (i == pointCoordinate)
                {
                    NewNumber += ",";
                }
                if (BaseSixteen.Any(symbol => symbol.Key == coeffs[i]))
                {
                    foreach (var symbol in BaseSixteen)
                    {
                        if (coeffs[i] == symbol.Key)
                        {
                            NewNumber += symbol.Value;
                            break;
                        }
                    }
                }
                else
                {
                    if (coeffs[i] >= 16)
                    {
                        NewNumber += " " + coeffs[i] + " ";
                    }
                    else
                    {
                        NewNumber += coeffs[i];
                    }
                    
                }

            }
            return NewNumber;
        }
        public static decimal ToDecimalSystem(decimal number, int fromBase)
        {
            decimal decNumber = 0;

            string[] NumberInString = number.ToString().Split(',');

            if (NumberInString[0].Any(symbol => Convert.ToInt32(symbol.ToString()) >= fromBase))
            {
                return decimal.MinValue;
            }
            if (NumberInString[1].Any(symbol => Convert.ToInt32(symbol.ToString()) >= fromBase))
            {
                return decimal.MinValue;
            }

            NumberInString[0] = new string(NumberInString[0].Reverse().ToArray());

            for (int i = 0; i < NumberInString[0].Length; i++)
            {
                decNumber += Convert.ToInt32(NumberInString[0][i].ToString()) * DecimalPow(fromBase, i);
            }

            decimal FracPart = 0;
            if (NumberInString.Length > 1)
            {
                for (int i = 0; i < NumberInString[1].Length; i++)
                {
                    FracPart += Convert.ToInt32(NumberInString[1][i].ToString()) / DecimalPow(fromBase, i+1);
                }
            }
            return decNumber + FracPart;
        }

        static public Dictionary<int, char> BaseSixteen = new Dictionary<int, char>()
        {
            { 10, 'A'},
            { 11, 'B'},
            { 12, 'C'},
            { 13, 'D'},
            { 14, 'E'},
            { 15, 'F'}
        };
    }
}
