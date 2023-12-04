namespace FourthLab;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        List<string> reversePolishNotation = new List<string>();
        reversePolishNotation = RPN.RewriteToRPN(input);
        double result = RPN.CalculateRPN(reversePolishNotation);
        Console.WriteLine(result);

    }
}