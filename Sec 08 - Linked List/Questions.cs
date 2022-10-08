namespace Sec08
{
    class Questions
    {
        public static void Main(string[] args)
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            Console.WriteLine("Append:");
            
            linkedList.Append(5);
            linkedList.Append(16);
            linkedList.Append(20);

            Console.WriteLine(linkedList.ToString());
            Console.WriteLine();

            Console.WriteLine("Prepend:");
            linkedList.Prepend(44);

            Console.WriteLine(linkedList.ToString());

            Console.ReadKey();
        }
    }

    class LinkedList<T>
    {
        public Node<T>? Head { get; private set; } 

        public Node<T>? Tail { get; private set; }

        public int Length { get; private set; }

        public LinkedList<T> Prepend(T value)
        {
            Node<T> node = new Node<T>(value, null);

            if (Head == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                node.NextNode = Head;
                Head = node;
            }

            Length++;

            return this;
        }

        public LinkedList<T> Append(T value)
        {
            Node<T> node = new Node<T>(value, null);
            
            if (Tail == null)
            {
                Head = node;
                Tail = node;
            }
            else
            {
                Tail.NextNode = node;
                Tail = node;
            }

            Length++;

            return this;
        }

        public override string ToString()
        {
            List<string> strings = new List<string>();

            bool finished = false;
            Node<T>? curr = Head;

            while (!finished && curr != null)
            {
                string value = curr.GetValueString();
                
                strings.Add(value);
                curr = curr.NextNode;
            }

            return string.Join(" --> ", strings.ToArray());
        }

        
    }

    class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? NextNode { get; set; }

        public Node(T value, Node<T>? nextNode)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }

        public string GetValueString()
        {
            if (Value == null)
            {
                return "null";
            }
            else
            {
                string? str = Value.ToString();
                if (str == null)
                {
                    return "null";
                }
                else
                {
                    return str;
                }
            }
        }
    }
}