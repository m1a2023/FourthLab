namespace FourthLab;

class RPN
{
	public static List<string> RewriteToRPN(string input)
	{

        Dictionary<char, int> operators = new Dictionary<char, int>
        {
            {'*', 2 },
            {'/', 2 },
            {'+', 1 },
            {'-', 1 },
        };
        
        List<string> tokens = new List<string>(); string num = null;

        for (int i = 0; i < input.Length; i++)
        {
            if (char.IsDigit(input[i]))
            {
                num += input[i];
            }

            else
            {
                if (input[i] != ' ')
                {
                    tokens.Add(num);
                    tokens.Add(input[i].ToString());
                    num = null;
                }
            }
        }
        if (!string.IsNullOrEmpty(num)) tokens.Add(num);

        Stack<string> stack = new Stack<string>();
        List<string> output = new List<string>();

        foreach (string token in tokens)
        {
            if (IsNumber(token))
                output.Add(token);

            else if (IsOperator(token))
            {
                while (stack.Count > 0 && operators.ContainsKey(token[0]) && operators[token[0]] <= operators[char.Parse(stack.Peek())])
                {
                    output.Add(stack.Pop().ToString());
                }
                stack.Push(token);
            }

            else if (token == "(")
                stack.Push(token);

            else if (token == ")")
            {
                while (stack.Count > 0 && stack.Peek() != "(")
                {
                    output.Add(stack.Pop().ToString());
                }

                stack.Pop();
            }

        }
        while (stack.Count > 0)
        {
            output.Add(stack.Pop());
        }

        return output;
    }

    public static double CalculateRPN(List<string> tokens)
    {
        Stack<double> stack = new Stack<double>();

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double number))
                stack.Push(number);
            
            else if (IsOperator(token))
            {
                double firstOperand = stack.Pop();
                double secondOperand = stack.Pop();
                double result = PerformOperation(token[0], firstOperand, secondOperand);

                stack.Push(result);
            }
        }
        return stack.Pop();
    }

    static double PerformOperation(char Operator, double fOperand, double sOperand)
    {
        return Operator switch
        {
            '*' => fOperand * sOperand,
            '/' => fOperand / sOperand,
            '+' => fOperand + sOperand,
            '-' => fOperand - sOperand,
        };
    }

    static bool IsNumber(string input)
    {
        return double.TryParse(input, out _);
    }

    static bool IsOperator(string input)
    {
        return input.Length == 1 && "*/+-".Contains(input);
    }
}