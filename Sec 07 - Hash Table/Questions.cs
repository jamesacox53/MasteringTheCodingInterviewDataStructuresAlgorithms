namespace Sec07
{
    public class Questions
    {
        public static void Main(string[] args)
        {
            HashTable<string> hashTable = new HashTable<string>(50);

            hashTable.Set("Hello", "World");
            hashTable.Set("Bonjour", "Monde");
            hashTable.Set("Hola", "Mundo");
            hashTable.Set("Hallo", "Welt");

            Console.WriteLine("Get and Set:");
            
            Console.WriteLine(hashTable.Get("Hello"));
            Console.WriteLine(hashTable.Get("Bonjour"));
            Console.WriteLine(hashTable.Get("Hola"));
            Console.WriteLine(hashTable.Get("Hallo"));

            Console.WriteLine();
            Console.WriteLine("keys:");

            List<string> keys = hashTable.keys();

            foreach(string key in keys)
            {
                Console.WriteLine(key);
            }

            
            List<char> chars1 = new List<char>() { '2', '5', '1', '2', '3', '5', '1', '2', '4' };
            List<char> chars2 = new List<char>() { '2', '1', '1', '2', '3', '5', '1', '2', '4' };
            List<char> chars3 = new List<char>() { '2', '3', '4', '5' };


            Console.WriteLine();
            Console.WriteLine("First Recurring Character1:");
            
            // should be 2
            Console.WriteLine(GetFirstRecurringCharacter1(chars1));

            // should be 1
            Console.WriteLine(GetFirstRecurringCharacter1(chars2));

            // should be null 
            Console.WriteLine(GetFirstRecurringCharacter1(chars3));

            Console.WriteLine();
            Console.WriteLine("First Recurring Character2:");

            // should be 2
            Console.WriteLine(GetFirstRecurringCharacter2(chars1));

            // should be 2
            Console.WriteLine(GetFirstRecurringCharacter2(chars2));

            // should be null
            Console.WriteLine(GetFirstRecurringCharacter2(chars3));

            Console.ReadKey();
        }

        public static char? GetFirstRecurringCharacter1(List<char> characters)
        {
            HashSet<char> chars = new HashSet<char>();

            foreach (char c in characters)
            {
                if (chars.Contains(c))
                {
                    return c;
                }

                chars.Add(c);
            }

            return null;
        }

        public static char? GetFirstRecurringCharacter2(List<char> characters)
        {
            LoadedDictionary loadedDictionaryObj = GetLoadedDictionary(characters);

            if (loadedDictionaryObj.RepeatedFirstCharacter) 
                return characters[0];

            Dictionary<char, List<int>> loadedDictionary = loadedDictionaryObj.LoadedDictionaryElem;

            for (int i = 1; i < characters.Count; i++)
            {
                List<int> indexes = loadedDictionary[characters[i]];

                if (indexes.Count > 1) 
                    return characters[i];
            }

            return null;
        }

        private static LoadedDictionary GetLoadedDictionary(List<char> characters)
        {
            Dictionary<char, List<int>> loadedDictionary = new Dictionary<char, List<int>>();

            loadedDictionary.Add(characters[0], new List<int> {0});

            for (int i = 1; i < characters.Count; i++)
            {
                char c = characters[i];

                if (c == characters[0])
                    return new LoadedDictionary(true, loadedDictionary);

                if (loadedDictionary.ContainsKey(c))
                {
                    List<int> list = loadedDictionary[c];
                    list.Add(i);
                }
                else
                {
                    loadedDictionary.Add(c, new List<int>() {i});
                }
            }

            return new LoadedDictionary(false, loadedDictionary);
        }

        private class LoadedDictionary
        {
            public bool RepeatedFirstCharacter { get; private set; }
            public Dictionary<char, List<int>> LoadedDictionaryElem { get; private set; }

            public LoadedDictionary(bool repeatedFirstCharacter, Dictionary<char, List<int>> loadedDictionary)
            {
                RepeatedFirstCharacter = repeatedFirstCharacter;
                LoadedDictionaryElem = loadedDictionary;
            }
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

        public List<string> keys()
        {
            List<string> keys = new List<string>();

            for(int i = 0; i < data.Length; i++)
            {
                List<KeyValue> bucket = data[i];

                if (bucket == null) continue;

                foreach(KeyValue keyValue in bucket)
                {
                    keys.Add(keyValue.Key);
                }
            }

            return keys;
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