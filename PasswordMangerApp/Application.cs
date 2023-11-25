using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordMangerApp
{
    internal class Application
    {

        private static readonly Dictionary<string, string> _PasswordEnteries = new();

        public static void Run(string[] args)
        {
            ReadPasswords();
            while (true)
            {
                Console.WriteLine("Please Select an option: ");
                Console.WriteLine("1. List all Passwords");
                Console.WriteLine("2. Add/Change Password");
                Console.WriteLine("3. Get Password");
                Console.WriteLine("4. Delete Password");

                var selectedOption = Console.ReadLine();

                if (selectedOption == "1")
                    ListAllPasswords();
                else if (selectedOption == "2")
                    AddOrChangePassword();
                else if (selectedOption == "3")
                    GetPassword();
                else if (selectedOption == "4")
                    DeletePassword();
                else
                    Console.WriteLine("invalid option");
                Console.WriteLine("------------------------------------------");
            }

        }
        private static void ListAllPasswords()
        {
            foreach (var entry in _PasswordEnteries)
            {
                Console.WriteLine($"{entry.Key} = {entry.Value}");
            }
        }
        private static void AddOrChangePassword()
        {
            Console.WriteLine("Please Enter Website/App Name :");
            var appName = Console.ReadLine();
            Console.WriteLine("Please Enter Website/App Password :");
            var password = Console.ReadLine();

            if (_PasswordEnteries.ContainsKey(appName))
                _PasswordEnteries[appName] = password;
            else
                _PasswordEnteries.Add(appName, password);

            SavePasswords();

        }
        private static void GetPassword()
        {
            Console.WriteLine("Please Enter Website/App Name :");
            var appName = Console.ReadLine();

            if (_PasswordEnteries.ContainsKey(appName))
            {
                Console.WriteLine($" Your Password is : {_PasswordEnteries[appName]}");
            }
            else
            {
                Console.WriteLine("Password Not Found");
            }
        }
        private static void DeletePassword()
        {
            Console.WriteLine("Please Enter Website/App Name :");
            var appName = Console.ReadLine();

            if (_PasswordEnteries.ContainsKey(appName))
            {
                _PasswordEnteries.Remove(appName);
                SavePasswords();
            }
            else
            {
                Console.WriteLine("Password Not Found");
            }

        }
        private static void ReadPasswords()
        {
            if (File.Exists("password.txt"))
            {
                var passwordLine = File.ReadAllText("password.txt");
                foreach (var line in passwordLine.Split(Environment.NewLine))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf('=');
                        var appName = line.Substring(0, equalIndex);
                        var password = line.Substring(equalIndex + 1);
                        _PasswordEnteries.Add(appName, password);
                    }
                }
            }
            else
                throw new Exception("Loss File");
        }
        private static void SavePasswords()
        {
            var sb = new StringBuilder();
            foreach (var entry in _PasswordEnteries)
            {
                sb.AppendLine($"{entry.Key}={entry.Value}");
                File.WriteAllText("password.txt", sb.ToString());
            }
        }
    }
}