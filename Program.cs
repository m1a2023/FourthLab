namespace FourthLab;

class Program
{
    static void Main()
    {
        string input = Console.ReadLine();

        List<string> output = new List<string>();
        output = RPN.RewriteToRPN(input);

        Console.WriteLine(output);

        foreach (string  element in  output)
        {
            Console.WriteLine(element);
        }
    }
}