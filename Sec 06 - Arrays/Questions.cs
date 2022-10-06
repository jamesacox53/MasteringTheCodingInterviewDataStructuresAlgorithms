using System.Collections.Generic;

class Questions
{
    public static void Main(string[] args) {
       /*
        string test1 = "Hello World!";

        string reversedTest1 = ReverseString(test1);

        Console.WriteLine(reversedTest1);
        */

        List<int> array1 = new List<int> { 0, 3, 4, 31 };
        List<int> array2 = new List<int> { 4, 6, 30 };

        List<int>? sortedList = MergeSortedArrays(array1, array2);

        foreach(int elem in sortedList)
        {
            Console.WriteLine(elem);
        }

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


    public static List<int>? MergeSortedArrays(List<int> array1, List<int> array2)
    {
        ArrayValidation validation = IsInputValid(array1, array2);

        if (validation.ShouldExit)
            return validation.ReturnVal;

        return MergeSortedInputArrays(array1, array2);
    }

    private static ArrayValidation IsInputValid(List<int> array1, List<int> array2)
    {
        if (array1 == null && array2 == null)
            return new ArrayValidation(true, null);

        if (array1 == null || array1.Count == 0)
            return new ArrayValidation(true, array2);

        if (array2 == null || array2.Count == 0)
            return new ArrayValidation(true, array1);

        return new ArrayValidation(false, null);
    }

    private class ArrayValidation
    {
        public ArrayValidation(bool shouldExit, List<int>? returnVal)
        {
            ShouldExit = shouldExit;
            ReturnVal = returnVal;
        }

        public bool ShouldExit { get; private set; }
        public List<int>? ReturnVal { get; private set; }
    }

    private static List<int>? MergeSortedInputArrays(List<int> array1, List<int> array2)
    {
        int index1 = 0;
        int index2 = 0;

        List<int> mergedSortedArray = new List<int>();

        bool finished = false;
        while (!finished)
        {
            bool index1InArray = index1 < array1.Count;
            bool index2InArray = index2 < array2.Count;

            if (!index1InArray && !index2InArray)
            {
                finished = true;
            }
            else if (!index1InArray)
            {
                mergedSortedArray.Add(array2[index2]);
                index2++;
            }
            else if (!index2InArray)
            {
                mergedSortedArray.Add(array1[index1]);
                index1++;
            }
            else
            {
                if (array1[index1] < array2[index2])
                {
                    mergedSortedArray.Add(array1[index1]);
                    index1++;
                }
                else
                {
                    mergedSortedArray.Add(array2[index2]);
                    index2++;
                }
            }
        }

        return mergedSortedArray;
    }
}
