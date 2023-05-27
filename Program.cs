using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

//Завдання 2.
//Користувач вводить з клавіатури слово для пошуку у файлі, шлях до файлу і слово для заміни.
//Додаток має змінити усі входження шуканого слова на слово для заміни.
//Статистику роботи додатку виведіть на екран.


namespace File_Text_Replace
{
    public class Text
    {
        private List<string> strings;
        public int Count { get; set; }

        public Text()
        {
            strings = new List<string>();
        }

        public void AddTextString(string text)
        {
            strings.Add(text);
        }

        public void PrintTexts()
        {
            foreach (string text in strings)
            {
                Console.WriteLine(text);
            }
        }

        public List<string> GetStringList()
        {
            return strings;
        }

        public Text Replace(string word, string wordToReplace)
        {
            Text newText = new Text();
            int replacementCount = 0;

            foreach (string str in strings)
            {
                string[] words = str.Split(' '); // Разбиваем строку на слова

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] == word)
                    {
                        words[i] = wordToReplace;
                        replacementCount++;
                    }
                }
                string replacedText = string.Join(" ", words); // Объединяем слова обратно в строку
                newText.AddTextString(replacedText);
            }
            Count = replacementCount;
            newText.Count = Count;
            return newText;
        }
    }


    public class FilesHandling // класс для записи - чтения файла с объектом
    {




        public Text ReadTextFile(string filePath)
        {
            Text listHolder = new Text();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    listHolder.AddTextString(line);
                }
            }
            return listHolder;
        }


        public void WriteTextFile(string filePath, Text textObject)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                List<string> stringList = textObject.GetStringList();
                foreach (string text in stringList)
                {
                    writer.WriteLine(text);
                }
            }
        }
    }



    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Введите путь: ");

            //string filePath = Console.ReadLine(); // раскоментировать для ручного ввода пути к текстовому многосрочному файлу
            string filePath = "FileToReplc.txt"; // что бы вручную не вводить, можно закоментировать

            var filesHandling = new FilesHandling();
            object loadedText = filesHandling.ReadTextFile(filePath);
            Text textForReplace = (Text)loadedText;
            textForReplace.PrintTexts();
            Console.WriteLine("\n___________________________________________________________________________________");
            Console.Write("Введите слово для поиска и замены: ");
            string word = Console.ReadLine();
            Console.Write("Введите замещающее слово: ");
            string wordToReplace = Console.ReadLine();
            Console.WriteLine("\n___________________________________________________________________________________");
            Text textAfterReplace = (textForReplace.Replace(word, wordToReplace));
            textAfterReplace.PrintTexts();
            Console.WriteLine("\n___________________________________________________________________________________");
            Console.WriteLine($"Всего замен для слова '{word}': {textAfterReplace.Count}");
            filePath = "FileAfterReplc.txt";
            filesHandling.WriteTextFile(filePath, textAfterReplace);
            filePath = "FileAfterReplc.txt";
            filesHandling.WriteTextFile(filePath, textAfterReplace);
        }
    }
}

