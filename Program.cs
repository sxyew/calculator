using System;

namespace Ideagen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            Console.WriteLine(calc.Calculate("1 + 1"));
            Console.WriteLine(calc.Calculate("2 * 2"));
            Console.WriteLine(calc.Calculate("1 + 2 + 3"));
            Console.WriteLine(calc.Calculate("6 / 2"));
            Console.WriteLine(calc.Calculate("11 + 23"));
            Console.WriteLine(calc.Calculate("11.1 + 23"));
            Console.WriteLine(calc.Calculate("1 + 1 * 3"));
            Console.WriteLine(calc.Calculate("( 11.5 + 15.4 ) + 10.1"));
            Console.WriteLine(calc.Calculate("23 - ( 29.3 - 12.5 )"));
            Console.WriteLine(calc.Calculate("10 - ( 2 + 3 * ( 7 - 5 ) )"));
        }
    }

    public class Calculator
    {
        // Main function
        public double Calculate(string sum) {
            // Initialise value
            double value = 0;

            // Search for all occurence of bracket
            while(sum.IndexOf("(") >= 0)
            {
                // Get innermost bracket
                String bracketString = FindBracketString(sum);

                // Evaluate innermost bracket
                double bracketValue = EvaluateString(bracketString.Trim('(').Trim(')'));

                // Replace innermost bracket with evaluated value
                sum = sum.Replace(bracketString, bracketValue.ToString());
            }       

            // To prevent evaluating negative value as a subtraction,
            // only evaluate string if string does not start with '-',
            // if negative value, show value.
            if (!sum.StartsWith("-"))
            {
                value = EvaluateString(sum);
            }
            else
            {
                value = Double.Parse(sum);
            }
            
            return value;
        }

        // Find innermost bracket and return as string
        public string FindBracketString(string sum)
        {
            // Search for the last occurence of open bracket,
            // from that position, locate the next closing bracket,
            // then construct the bracket string.
            int lastOpenBracket = sum.LastIndexOf("(");
            int lastCloseBracket = sum.IndexOf(")", lastOpenBracket) + 1;
            int length = lastCloseBracket - lastOpenBracket - 1;
            string bracketString = sum.Substring(lastOpenBracket, lastCloseBracket - lastOpenBracket);

            return bracketString;
        }

        // Evaluate string going by BODMAS/PEMDAS order
        public double EvaluateString(string sum)
        {
            sum = EvaluatePower(sum);
            sum = EvaluateDivisionMultiplication(sum);
            sum = EvaluateAdditionSubtraction(sum);

            return Double.Parse(sum);
        }

        // Evaluate the Over/Power-of operator
        public string EvaluatePower(string sum)
        {
            // Split string by space
            string[] inputs = sum.Trim().Split(' ');

            // Find last operator position,
            // for power-of operators, it does not follow the left to right sequence
            // e.g. 2 ^ 2 ^ 3 is evaluated as 2 ^ ( 2 ^ 3 ) instead, hence the usage of LastIndexOf
            int powerIndex = Array.LastIndexOf(inputs, "^");

            // Loop while operator exists
            while(powerIndex >= 0)
            {
                string powerString = String.Empty;
                double value = 0;

                // Calculate value by [x] ^ [y],
                // x = position before the operator,
                // y = position after the operator.
                value = Math.Pow(Double.Parse(inputs[powerIndex - 1]), Double.Parse(inputs[powerIndex + 1]));

                // Construct string to replace with calculated value
                powerString = inputs[powerIndex - 1] + " ^ " + inputs[powerIndex + 1];

                // Replace string with value
                sum = sum.Replace(powerString, value.ToString());

                // Find for next occurence
                inputs = sum.Trim().Split(' ');
                powerIndex = Array.LastIndexOf(inputs, "^");
            }

            return sum.Trim();
        }

        // Evaluate the Division/Multiplication operator
        public string EvaluateDivisionMultiplication(string sum)
        {
            // Split string by space
            string[] inputs = sum.Trim().Split(' ');

            // Find operator positions
            int multiplyIndex = Array.IndexOf(inputs, "*");
            int divideIndex = Array.IndexOf(inputs, "/");

            // Loop while operators exist
            while(multiplyIndex >= 0 || divideIndex >= 0)
            {
                // Initialise values
                int index = 0;
                string operatorString = String.Empty;
                string dmString = String.Empty;
                double value = 0;

                // Determine the first occurence of operators from left to right,
                // initialise the index and operator type.
                if (divideIndex == -1 || (multiplyIndex != -1 && multiplyIndex < divideIndex))
                {
                    index = multiplyIndex;
                    operatorString = "*";
                }
                else
                {
                    index = divideIndex;
                    operatorString = "/";
                }

                // Calculate based on operator type
                switch (operatorString)
                {
                    case "*":
                        // Calculate value by [x] * [y]
                        // x = position before the operator,
                        // y = position after the operator.
                        value = Double.Parse(inputs[index - 1]) * Double.Parse(inputs[index + 1]);
                        
                        // Construct string to replace
                        dmString = inputs[index - 1] + " * " + inputs[index + 1];
                        
                        break;
                    case "/":
                        // Calculate value by [x] / [y]
                        // x = position before the operator,
                        // y = position after the operator.
                        value = Double.Parse(inputs[index - 1]) / Double.Parse(inputs[index + 1]);
                        
                        // Construct string to replace
                        dmString = inputs[index - 1] + " / " + inputs[index + 1];
                        
                        break;
                }

                // Replace string with value
                sum = sum.Replace(dmString, Math.Round(value, 2).ToString());

                // Find for next occurence
                inputs = sum.Trim().Split(' ');
                multiplyIndex = Array.IndexOf(inputs, "*");
                divideIndex = Array.IndexOf(inputs, "/");
            }

            return sum;
        }

        // Evaluate the Addition/Subtraction operator
        public string EvaluateAdditionSubtraction(string sum)
        {
            // Split string by space
            string[] inputs = sum.Trim().Split(' ');

            // Find operator positions
            int plusIndex = Array.IndexOf(inputs, "+");
            int minusIndex = Array.IndexOf(inputs, "-");

            // Loop while operators exist
            while(plusIndex >= 0 || minusIndex >= 0)
            {
                // Initialise values
                int index = 0;
                string operatorString = String.Empty;
                string asString = String.Empty;
                double value = 0;

                // Determine the first occurence of operators from left to right,
                // initialise the index and operator type.
                if (minusIndex == -1 || (plusIndex != -1 && plusIndex < minusIndex))
                {
                    index = plusIndex;
                    operatorString = "+";
                }
                else
                {
                    index = minusIndex;
                    operatorString = "-";
                }

                // Calculate based on operator type
                switch (operatorString)
                {
                    case "+":
                        // Calculate value by [x] + [y]
                        // x = position before the operator,
                        // y = position after the operator.
                        value = Double.Parse(inputs[index - 1]) + Double.Parse(inputs[index + 1]);
                        
                        // Construct string to replace
                        asString = inputs[index - 1] + " + " + inputs[index + 1];
                        
                        break;
                    case "-":
                        // Calculate value by [x] - [y]
                        // x = position before the operator,
                        // y = position after the operator.
                        value = Double.Parse(inputs[index - 1]) - Double.Parse(inputs[index + 1]);
                        
                        // Construct string to replace
                        asString = inputs[index - 1] + " - " + inputs[index + 1];
                        
                        break;
                }

                // Replace string with value
                sum = sum.Replace(asString, Math.Round(value, 2).ToString());

                // Find for next occurence
                inputs = sum.Trim().Split(' ');
                plusIndex = Array.IndexOf(inputs, "+");
                minusIndex = Array.IndexOf(inputs, "-");
            }

            return sum;
        }        
    }
}
