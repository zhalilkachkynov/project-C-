using System;
using System.IO;
using System.Text;

class Program
{
    static string logFile = "messages.txt";

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding  = Encoding.UTF8;

        Console.WriteLine("=== Простой шифратор Цезаря (латиница) ===");
        Console.WriteLine("Поддерживаются только латинские буквы A-Z и a-z. Прочие символы остаются без изменений.\n");

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1 - Зашифровать текст");
            Console.WriteLine("2 - Расшифровать текст");
            Console.WriteLine("0 - Выход");
            Console.Write("Ваш выбор: ");
            string choice = Console.ReadLine()?.Trim() ?? "";

            if (choice == "0")
            {
                Console.WriteLine("Выход из программы. Пока!");
                break;
            }

            if (choice != "1" && choice != "2")
            {
                Console.WriteLine("Неверный выбор. Введите 1, 2 или 0.\n");
                continue;
            }

            Console.Write("Введите текст: ");
            string input = Console.ReadLine() ?? "";

            int shift = ReadShift();

            string result;
            string mode;
            if (choice == "1")
            {
                result = CaesarLatinShift(input, shift);
                mode = $"Encrypt (shift = {shift})";
            }
            else
            {
                result = CaesarLatinShift(input, -shift);
                mode = $"Decrypt (shift = {shift})";
            }

            Console.WriteLine("\nРезультат:");
            Console.WriteLine(result + "\n");

            SaveToFile(mode, shift, input, result);
            Console.WriteLine($"Сохранено в файл: {logFile}\n");
        }
    }
    
    static int ReadShift()
    {
        while (true)
        {
            Console.Write("Введите целочисленный сдвиг (например 3): ");
            string? s = Console.ReadLine();
            if (int.TryParse(s, out int shift))
            {
                return shift;
            }
            Console.WriteLine("Некорректный ввод. Попробуйте снова.");
        }
    }
    
    static string CaesarLatinShift(string text, int shift)
    {
        var sb = new StringBuilder(text.Length);
        
        int s = shift % 26;

        foreach (char ch in text)
        {
            if (IsLatinUpper(ch))
            {
                sb.Append(ShiftCharWithinAlphabet(ch, s, 'A', 26));
            }
            else if (IsLatinLower(ch))
            {
                sb.Append(ShiftCharWithinAlphabet(ch, s, 'a', 26));
            }
            else
            {
                sb.Append(ch);
            }
        }

        return sb.ToString();
    }
    
    static char ShiftCharWithinAlphabet(char ch, int shift, char baseChar, int alphabetSize)
    {
        int baseCode = baseChar;                 
        int offset = ch - baseCode;              
        int newPos = ((offset + shift) % alphabetSize + alphabetSize) % alphabetSize;
        return (char)(baseCode + newPos);
    }

    static bool IsLatinUpper(char c) => c >= 'A' && c <= 'Z';
    static bool IsLatinLower(char c) => c >= 'a' && c <= 'z';

    
    static void SaveToFile(string mode, int shift, string original, string result)
    {
        try
        {
            string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var entry = new StringBuilder();
            entry.AppendLine("========================================");
            entry.AppendLine($"Time: {time}");
            entry.AppendLine($"Mode: {mode}");
            entry.AppendLine($"Shift: {shift}");
            entry.AppendLine("Original:");
            entry.AppendLine(original);
            entry.AppendLine("Result:");
            entry.AppendLine(result);
            entry.AppendLine();

            File.AppendAllText(logFile, entry.ToString(), Encoding.UTF8);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при записи в файл: " + ex.Message);
        }
    }
}