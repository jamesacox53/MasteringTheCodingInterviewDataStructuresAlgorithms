using System.Collections.Generic;

class Questions
{
    public static void Main(string[] args) {
        string test1 = "Hello World!";

        string reversedTest1 = ReverseString(test1);

        Console.WriteLine(reversedTest1);
        Console.ReadKey();
    }

    public static string ReverseString(string input) 
    {
        if (String.IsNullOrWhiteSpace(input)) 
            return input;

        return ReverseStringInput(input);
    }

    private static string ReverseStringInput(string input)
    {
        Stack<char> chars = new Stack<char>();

        foreach (char c in input)
        {
            chars.Push(c);
        }

        char[] reversedChars = chars.ToArray();

        string reversedString = new string(reversedChars);

        return reversedString;
    }





}
