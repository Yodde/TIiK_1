using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Specialized;
using System.Collections;

namespace TIiK_1 {
    class Program {
        static OrderedDictionary lz78Dcitionary = new OrderedDictionary();
        static string outStr = "";
        static StringBuilder outBuilder = new StringBuilder();
        static OrderedDictionary orderedDict = new OrderedDictionary();

        static void Main(string[] args) {
            //lz78Dcitionary.Add("", -1);

            ClearToUseAgain(lz78Dcitionary);

            //SortedDictionary<char, int> litPlDic = new SortedDictionary<char, int>();
            //SortedDictionary<char, int> infPlDic = new SortedDictionary<char, int>();
            //SortedDictionary<char, int> litEngDic = new SortedDictionary<char, int>();
            //SortedDictionary<char, int> infEngDic = new SortedDictionary<char, int>();

            //orderedDict.Add("DD", 3);
            //Console.WriteLine("Wczytanie literatury polskiej");
            //string litPlS = LoadFromFile(@"lit_pl.txt");
            //string litEngS = LoadFromFile(@"lit_ang.txt");
            //string infPLS = LoadFromFile(@"inf_pl.txt");
            //string infEngS = LoadFromFile(@"inf_ang.txt");

            //litPlDic = AddToDic(litPlS);
            //litEngDic = AddToDic(litEngS);
            //infPlDic = AddToDic(infPLS);
            //infEngDic = AddToDic(infEngS);

            //int litPlLength = 0, infPlLength = 0, litEngLength = 0, infEngLenght = 0;

            //Console.WriteLine("Literatura polska");
            //litPlLength = WriteAndCount(litPlDic);

            //Console.WriteLine("Informatyczne polskie");
            //infPlLength = WriteAndCount(infPlDic);

            //Console.WriteLine("Lietratura angielska");
            //litEngLength = WriteAndCount(litEngDic);

            //Console.WriteLine("Informatycze angielskie");
            //infEngLenght = WriteAndCount(infEngDic);


            //Console.WriteLine("Literatura polska");
            //WriteFrequency(litPlDic, litPlLength);

            //Console.WriteLine("Informatyczne polskie");
            //WriteFrequency(infPlDic, infPlLength);

            //Console.WriteLine("Lietratura angielska");
            //WriteFrequency(litEngDic, litEngLength);

            //Console.WriteLine("Informatycze angielskie");
            //WriteFrequency(infEngDic, infEngLenght);

            Menu();


            //string barbara = LoadFromFile(@"barbara_compress.txt");
            //ClearToUseAgain(orderedDict);
            //RebuildDictionary(orderedDict, barbara);
            //PrintDictionary(orderedDict);
            //string text = GetStringFromKeys(orderedDict);
            //Console.WriteLine(text);


            //string compressed = CompressString(barbara);
            //Console.WriteLine(compressed);
            //PrintDictionary(lz78Dcitionary);
            //WriteToFile(@"barbara_compress.txt", compressed);

            //ClearToUseAgain(lz78Dcitionary);
            ////CompressString(litPlS);

            //string compressedFile = LoadFromFile(@"barbara_compress.txt");


            //Console.ReadLine();
        }

        public static void Menu() {
            bool go = true;
            while (go) {
                int i = -1;
                Console.WriteLine("\nKompresuj: 1 \nDekompresuj: 2\nWypisz: 3\nWyjście: 0\n");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out i))
                    continue;
                switch (i) {
                    case 1: {
                            ClearToUseAgain(lz78Dcitionary);
                            Console.WriteLine("Wpisz nazwę pliku");
                            string name = Console.ReadLine();
                            string text = LoadFromFile(name+".txt");
                            if (string.IsNullOrEmpty(text)) {
                                Console.WriteLine("Pusty plik, lub taki nie istnieje");
                                Console.ReadKey();
                                break;
                            }
                            string compressed = CompressString(text);
                            Console.WriteLine(compressed);
                            PrintDictionary(lz78Dcitionary);
                            string compressedName = string.Format("{0}_comp.txt", name);
                            WriteToFile(compressedName, compressed);
                            Console.WriteLine("Zapisano do pliku {0}", compressedName);
                            Console.ReadKey();
                            break;
                        }
                    case 2: {
                            Console.WriteLine("Wpisz nazwę pliku");
                            string name = Console.ReadLine();
                            string code = LoadFromFile(name+".txt");
                            if (string.IsNullOrEmpty(code)) {
                                Console.WriteLine("Pusty plik, lub taki nie istnieje");
                                Console.ReadKey();
                                break;
                            }
                            ClearToUseAgain(orderedDict);
                            RebuildDictionary(orderedDict, code);
                            PrintDictionary(orderedDict);
                            string text = GetStringFromKeys(orderedDict);
                            Console.WriteLine(text);
                        }
                        break;

                    case 3: {
                            int length;
                            SortedDictionary<char, int> lit = new SortedDictionary<char, int>();
                            Console.WriteLine("Podaj nazwę pliku");
                            string name = Console.ReadLine();
                            string text = LoadFromFile(name+".txt");
                            if (string.IsNullOrEmpty(text)) {
                                Console.WriteLine("Pusty plik, lub taki nie istnieje");
                                Console.ReadKey();
                                break;
                            }
                            lit = AddToDic(text);
                            length = WriteAndCount(lit);
                            WriteFrequency(lit, length);
                            break;
                        }
                    case 0:
                        go = false;
                        break;
                    default:
                        Console.WriteLine("Brak takiej opcji");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void ClearToUseAgain(OrderedDictionary dict) {
            dict.Clear();
            AddFirstToDict(dict);
            outStr = "";
            outBuilder.Clear();
        }
        public static void AddFirstToDict(OrderedDictionary dict) {
            dict.Add("", -1);
        }
        public static SortedDictionary<char, int> AddToDic(string str) {
            SortedDictionary<char, int> dic = new SortedDictionary<char, int>();
            foreach (var s in str) {
                var c = char.ToLower(s);
                // if (char.IsLetter(c)) {
                try {
                    dic[c]++;
                }
                catch (KeyNotFoundException) {
                    dic.Add(c, 1);
                }
                //  }
            }
            return dic;
        }
        public static string LoadFromFile(string path) {
            string str = "";
            try {
                using (StreamReader reader = new StreamReader(path)) {
                    str = reader.ReadToEnd();
                }
            }
            catch (FileNotFoundException) {
                return null;
            }
            return str;
        }

        public static void WriteToFile(string path, string str) {
            using (StreamWriter writer = new StreamWriter(path)) {
                foreach (var c in str) {
                    writer.Write(c);
                }
            }
        }

        public static int WriteAndCount(SortedDictionary<char, int> dic) {
            int letters = 0;
            foreach (var d in dic) {
                Console.WriteLine(d);
                letters += d.Value;
            }
            Console.WriteLine("Liczba liter = {0}", letters);
            return letters;
        }

        public static double KeyFrequency(int value, int numberOfLetters) {
            return ((double)value / (double)numberOfLetters);
        }

        public static void WriteFrequency(SortedDictionary<char, int> dic, int numberOfLetters) {
            List<Letters> list = new List<Letters>();
            foreach (var d in dic) {
                double dFrequency = KeyFrequency(d.Value, numberOfLetters);
                list.Add(new Letters { letter = d.Key, frequency = dFrequency, count = d.Value });
            }
            List<Letters> sorted = list.OrderByDescending(l => l.frequency).ToList();
            foreach (var l in sorted) {
                Console.WriteLine("Znak: {0}\tLiczba liter: {1}\tProcent: {2}%\tEntropia: {3}", l.letter, l.count, l.frequency * 100.0, EntropyForLetter(l.frequency));
            }
            Entropy(list);
        }

        public static double EntropyForLetter(double frequency) {
            return frequency * Math.Log(1 / frequency, 2);
        }
        public static void Entropy(List<Letters> list) {
            double entropy = 0;
            foreach (var i in list) {
                entropy += i.frequency * Math.Log(1 / i.frequency, 2);
            }
            Console.WriteLine("Entropia dla tekstu {0} liczba znaków {1}", entropy, list.Count);
        }

        public class Letters {
            public char letter;
            public double frequency;
            public int count;
        }

        public class Compare {
            public string Name { get; set; }
            public int LettersCount { get; set; }
            public double Entropy { get; set; }
            public List<Letters> Top5 { get; set; }

        }

        public static void XML() {

        }

        public static string CompressString(string inStr) {
            string key = "";
            bool complexPhrase = false;
            foreach (var s in inStr) {
                key += s;
                if (complexPhrase) {
                    if (AddStrToDict(key)) {
                        complexPhrase = false;
                        key = "";
                    }
                }
                else {
                    if (!AddNextCharToDict(key)) {
                        complexPhrase = true;
                    }
                    else {
                        complexPhrase = false;
                        key = "";
                    }
                }
            }
            outStr = outBuilder.ToString();
            //Console.WriteLine(outStr);
            //PrintDictionary(lz78Dcitionary);
            return outStr;
        }

        //public static string Decompression(string str) {

        //}

        public static bool AddNextCharToDict(string key) {
            if (!ContainsKey(lz78Dcitionary, key)) {
                lz78Dcitionary.Add(key, 0);
                //
                AppendString(key, 0);
                //
                return true;
            }
            else {
                return false;
            }
        }
        public static bool AddStrToDict(string key) {
            if (!ContainsKey(lz78Dcitionary, key)) {
                string subKey = key.Substring(0, key.Length - 1);
                int subkeyIndex = GetIndex(lz78Dcitionary, subKey);
                if (subkeyIndex == 0)
                    throw new IndexOutOfRangeException("Key index cannot be zero");
                lz78Dcitionary.Add(key, subkeyIndex);
                //
                AppendString(key.Substring(key.Length - 1), subkeyIndex);
                //

                return true;
            }
            return false;
        }


        public static int GetIndex(OrderedDictionary dict, string key) {
            if (dict.Count == 1)
                return 0;
            for (int i = 1; i < dict.Count; i++) {
                if (dict.Cast<DictionaryEntry>().ElementAt(i).Key.ToString() == key) {
                    return i;
                }
            }
            return 0;
        }

        public static bool ContainsKey(OrderedDictionary dict, string key) {
            if (dict.Contains(key))
                return true;
            return false;

        }
        public static void AppendString(string key, int index) {
            outBuilder.Append("(");
            outBuilder.Append(index);
            outBuilder.Append(',');
            outBuilder.Append(key);
            outBuilder.Append(')');
        }
        public static void PrintDictionary(OrderedDictionary dict) {
            Console.WriteLine();
            Console.WriteLine("  {0}  |  {1}", 'i', 'w');
            Console.WriteLine("  {0} | {1}", "--", "--");
            for (int i = 1; i < dict.Count; i++) {
                string key = GetKeyFromDictionary(dict, i);
                string value = dict[i].ToString();
                Console.WriteLine("  {0}  |  {1}", value, key);
            }
        }

        public static string GetKeyFromDictionary(OrderedDictionary dict, int index) {
            return dict.Cast<DictionaryEntry>().ElementAt(index).Key.ToString();
        }
        public static void AddRowToDictionary(OrderedDictionary dict, int index, string key) {
            if (index == 0) {
                dict.Add(key, index);
            }
            else {
                string pre = GetKeyFromDictionary(dict, index);
                dict.Add(pre + key, index);
            }
        }
        public static void RebuildDictionary(OrderedDictionary dict, string compressedText) {
            string key = "";
            string longIndex = "";
            int index = 0;
            const char openB = '(';
            const char closeB = ')';
            const char comma = ',';
            bool isBraceOpened = false;
            bool isNumbertaken = false;
            bool isKeyTaken = false;
            foreach (var c in compressedText) {
                if (isKeyTaken && isNumbertaken && isBraceOpened && c == closeB) {
                    AddRowToDictionary(dict, index, key);
                    isBraceOpened = false;
                    isNumbertaken = false;
                    isKeyTaken = false;
                    key = "";
                    longIndex = "";
                    index = 0;
                }
                if (c == openB && !isBraceOpened) {
                    isBraceOpened = true;
                    continue;
                }
                if (isBraceOpened) {
                    if (!isNumbertaken) {
                        if (c != comma)
                            longIndex += c;
                        else {
                            index = int.Parse(longIndex);
                            isNumbertaken = true;
                            continue;
                        }
                    }
                    else {
                        key = c.ToString();
                        isKeyTaken = true;
                    }
                }
            }
        }

        public static string GetStringFromKeys(OrderedDictionary dict) {
            string text = "";
            for (int i = 1; i < dict.Count; i++) {
                text += GetKeyFromDictionary(dict, i);
            }
            return text;
        }
    }
}
