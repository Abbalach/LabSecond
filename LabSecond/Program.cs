using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSecond
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("число 1:");
                string number = Console.ReadLine();

                Console.WriteLine("с основы:");
                int Base = int.Parse(Console.ReadLine());

                var Number = ToDecimalSystem(number, Base);

                Console.WriteLine();
                Console.WriteLine(Number);
                Console.WriteLine();

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
        public static decimal ToDecimalSystem(string number, int fromBase)
        {
            decimal decNumber = 0;

            string[] NumberInString = number.Split(',');

            if (fromBase <= 10)
            {
                if (NumberInString[0].Any(symbol => Convert.ToInt32(symbol.ToString()) >= fromBase))
                {
                    return decimal.MinValue;
                }
                if (NumberInString.Length > 1)
                {
                    if (NumberInString[1].Any(symbol => Convert.ToInt32(symbol.ToString()) >= fromBase))
                    {
                        return decimal.MinValue;
                    }
                }
            }
            

            if (NumberInString[0].Any(symbol =>
            symbol != '1' &&
            symbol != '2' &&
            symbol != '3' &&
            symbol != '4' &&
            symbol != '5' &&
            symbol != '6' &&
            symbol != '7' &&
            symbol != '8' &&
            symbol != '9' &&
            symbol != '0' &&
            symbol != 'A' &&
            symbol != 'B' &&
            symbol != 'C' &&
            symbol != 'D' &&
            symbol != 'E' &&
            symbol != 'F' 
            ))
            {
                return decimal.MinValue;
            }
            if (NumberInString.Length > 1)
            {
                if (NumberInString[1].Any(symbol =>
                symbol != '1' &&
                symbol != '2' &&
                symbol != '3' &&
                symbol != '4' &&
                symbol != '5' &&
                symbol != '6' &&
                symbol != '7' &&
                symbol != '8' &&
                symbol != '9' &&
                symbol != '0' &&
                symbol != 'A' &&
                symbol != 'B' &&
                symbol != 'C' &&
                symbol != 'D' &&
                symbol != 'E' &&
                symbol != 'F'))
                {
                    return decimal.MinValue;
                }
            }

            NumberInString[0] = new string(NumberInString[0].Reverse().ToArray());

            for (int i = 0; i < NumberInString[0].Length; i++)
            {               
                if (BaseSixteen.Any(symbol => symbol.Value == NumberInString[0][i]))
                {
                    foreach (var symbol in BaseSixteen)
                    {
                        if (NumberInString[0][i] == symbol.Value)
                        {
                            
                            decNumber += symbol.Key * DecimalPow(fromBase, i);
                            break;
                        }
                    }
                }
                else
                {
                    decNumber += Convert.ToInt32(NumberInString[0][i].ToString()) * DecimalPow(fromBase, i);
                }
                
            }

            decimal FracPart = 0;
            if (NumberInString.Length > 1)
            {
                for (int i = 0; i < NumberInString[1].Length; i++)
                {
                    if (BaseSixteen.Any(symbol => symbol.Value == NumberInString[1][i]))
                    {
                        foreach (var symbol in BaseSixteen)
                        {
                            if (NumberInString[1][i] == symbol.Value)
                            {
                                FracPart += symbol.Key / DecimalPow(fromBase, i + 1);
                                break;
                            }
                        }
                    }
                    else
                    {
                        FracPart += Convert.ToInt32(NumberInString[1][i].ToString()) / DecimalPow(fromBase, i + 1);
                    }
                   
                }
            }
            return decNumber + FracPart;
        }

        public static string System(string number1, string number2, int Base, char symbol)
        {
            switch (symbol)
            {
                case '+':
                    {
                        var DecNumber = ToDecimalSystem(number1, Base) + ToDecimalSystem(number2, Base);

                        return ToAnotherSystem(DecNumber, Base);
                    }
                case '-':
                    {
                        var DecNumber = ToDecimalSystem(number1, Base) - ToDecimalSystem(number2, Base);

                        return ToAnotherSystem(DecNumber, Base);
                    }
                case '*':
                    {
                        var DecNumber = ToDecimalSystem(number1, Base) / ToDecimalSystem(number2, Base);

                        return ToAnotherSystem(DecNumber, Base);
                    }
                case '/':
                    {
                        var DecNumber = ToDecimalSystem(number1, Base) * ToDecimalSystem(number2, Base);

                        return ToAnotherSystem(DecNumber, Base);
                    }
                default:
                    return string.Empty;
            }
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
