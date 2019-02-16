using System;
using System.Collections.Generic;
using System.Globalization;

namespace Inlämningsuppgift_2
{
    class Program
    {
        static Dictionary<int, string> items;
        private static int enteredItems;
        private static int inputKey;

        static void Main(string[] args)
        {
            StartGame();
            RunApp();
        }

        private static void StartGame()
        {
            //init dict
            items = new Dictionary<int, string>();
        }

        private static void RunApp()
        {
            do
            {
            PrintMenu();
            } while (SelectMenu() == true);
        }

        private static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Grocery list app");
            Console.WriteLine();
            Console.WriteLine("1. Add item");
            Console.WriteLine("2. Remove item");
            Console.WriteLine("3. View list");
            Console.WriteLine("4. Modify item");
            Console.WriteLine("5. Check most inexpensive");
            Console.WriteLine("6. Check most expensive");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
            Console.WriteLine("Press 1 - 7 to choose what to do");
        }

        private static bool SelectMenu()
        {
            InputValidationChar();

            switch (inputKey)
            {
                case 1:
                    if (enteredItems == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    AddItem();
                    return true;
                case 2:
                    if (enteredItems == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    RemoveItem();
                    return true;
                case 3:
                    if (enteredItems == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    PrintDict();
                    return true;
                case 4:
                    if (enteredItems == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    ChangeItem();
                    return true;
                case 5:
                    if (enteredItems == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    CheckMin();
                    return true;
                case 6:
                    if (enteredItems == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    CheckMax();
                    return true;
                case 7:
                    Environment.Exit(0);
                        return false;
                default:
                    return false;
            }
        }

        private static void AddItem()
        {
            throw new NotImplementedException();
        }

        private static void RemoveItem()
        {
            throw new NotImplementedException();
        }

        private static void PrintDict()
        {
            throw new NotImplementedException();
        }

        private static void ChangeItem()
        {
            throw new NotImplementedException();
        }

        private static void CheckMin()
        {
            throw new NotImplementedException();
        }

        private static void CheckMax()
        {
            throw new NotImplementedException();
        }

        private static void InputValidationChar()
        {
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey(true);
            } while (!char.IsDigit(input.KeyChar));
            bool success = int.TryParse(input.KeyChar.ToString(), NumberStyles.Integer, null, out inputKey);
        }

        private static void EmptyDict()
        {
            Console.WriteLine();
            Console.WriteLine("There are no items to evaluate, please enter an item first");
            PressAnyKey();
        }

        private static void PressAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}