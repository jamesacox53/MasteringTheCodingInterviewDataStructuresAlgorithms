namespace Sec07
{
    public class Questions
    {
        public static void Main(string[] args)
        {
            HashTable<string> hashTable = new HashTable<string>(50);

            hashTable.Set("Hello", "World");

            Console.WriteLine(hashTable.Get("Hello"));
            Console.ReadKey();
        }
    }

    public class HashTable<T>
    {
        public int Size { get; private set; }
        private List<KeyValue>[] data;

        public HashTable(int size)
        {
            Size = size;
            data = new List<KeyValue>[size];
        }

        private int hash(string key)
        {
            int hash = 0;

            for (int i = 0; i < key.Length; i++)
            {
                hash = (hash + (int)key[i] * i) % Size;
            }

            return hash;
        }

        public bool Set(string key, T value)
        {
            int index = hash(key);

            KeyValue elem = new KeyValue(key, value);

            AddKeyValueToDataAtIndex(index, elem);

            return true;
        }

        private void AddKeyValueToDataAtIndex(int index, KeyValue keyValue)
        {
            if (data[index] == null)
            {
                data[index] = new List<KeyValue> { keyValue };
            }
            else
            {
                List<KeyValue> keyValues = data[index];

                for (int i = 0; i < keyValues.Count; i++)
                {
                    if (keyValues[i].Key == keyValue.Key)
                    {
                        keyValues[i] = keyValue;
                        return;
                    }
                }

                keyValues.Add(keyValue);
            }
        }

        public T Get(string key)
        {
            List <KeyValue> keyValues = data[hash(key)];

            foreach(KeyValue keyValue in keyValues)
            {
                if (keyValue.Key == key)
                    return keyValue.Value;
            }

            throw new Exception("Key doesn't exist in the hashtable");
        }

        private class KeyValue
        {
            public string Key { get; private set; }
            public T Value { get; private set; }

            public KeyValue(string key, T value)
            {
                Key = key;
                Value = value;
            }
        }
    }
}