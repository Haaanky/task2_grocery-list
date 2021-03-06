﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Inlämningsuppgift_2_2
{
    class Program
    {
        static Dictionary<string, int> items;
        static List<string> itemsKeyList;
        static List<int> itemsValueList;

        private static int inputKey;
        private static string stopKey;

        static void Main(string[] args)
        {
            try
            {
            InitiateApp();
            RunApp();
            }
            catch (Exception)
            {
                Console.WriteLine("Generic error message, please don't do that again it clearly didn't work ...");
            }
        }

        private static void InitiateApp()
        {
            Console.Title = "Fruit sallad";
            items = new Dictionary<string, int>(); //init dict & lists
            itemsKeyList = new List<string>();
            itemsValueList = new List<int>();
        }

        private static void RunApp()
        {
            do
            {
            PrintMenu();
            } while (SelectMenu());
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
            Console.WriteLine("5. Check cheapest");
            Console.WriteLine("6. Check most expensive");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
            Console.WriteLine("Press 1 - 7 to choose what to do");
            Console.WriteLine();
        }

        private static bool SelectMenu()
        {
            InputValidationCharInt();

            switch (inputKey)
            {
                case 1:
                    AddItems();
                    return true;
                case 2:
                    if (items.Count == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    RemoveItem();
                    return true;
                case 3:
                    if (items.Count == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    ViewList();
                    return true;
                case 4:
                    if (items.Count == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    ChangeItem();
                    return true;
                case 5:
                    if (items.Count == 0)
                    {
                        EmptyDict();
                        return true;
                    }
                    CheckMin();
                    return true;
                case 6:
                    if (items.Count == 0)
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
                    return true;
            }
        }

        private static void AddItems()
        {
            ConsoleKeyInfo input;

            do
            {
            int numberOfItems = AskForItems();
                for (int i = 0; i < numberOfItems; i++)
                {
                    Console.WriteLine("--------");
                    try
                    {
                    items.Add(ItemName(), ItemValue());
                    }
                    catch (ArgumentException)
                    {
                        Console.Clear();
                        Console.WriteLine("The name you entered already exists, please try again or use menu option 4. Change Item");
                        PressAnyKey();
                    }
                    Console.WriteLine("--------");
                }
                do
                {
                    Console.WriteLine($"You have entered {items.Count} items so far");
                    Console.WriteLine("Would you like to add more? Press y for yes and n for no");
                    input = Console.ReadKey(true);
                } while (input.Key != ConsoleKey.Y && input.Key != ConsoleKey.N);
                Console.Clear();
                PrintMenu();
            } while (input.Key == ConsoleKey.Y);
        }

        private static string ItemName()
        {
            Console.WriteLine("Name:");
            return Console.ReadLine();
        }

        private static int ItemValue()
        {
            Console.WriteLine("Price:");
            bool success = int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out int input);

            if (input < 1 || input > int.MaxValue)
                do
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if ((input < 1 && i < 3) || (input > int.MaxValue && i < 3))
                        {
                            Console.WriteLine("Please enter a valid number:");
                            success = int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out input);
                        }
                        else if ((input < 1 && i == 3) || (input > int.MaxValue && i == 3))
                        {
                            Console.WriteLine($"A valid number is between 1 and {int.MaxValue}");
                            Console.WriteLine("Please enter a valid number:");
                            success = int.TryParse(Console.ReadLine(), NumberStyles.Integer, null, out input);
                        }
                        if (input >= 1 && input <= int.MaxValue) break;
                    }
                } while (input < 1 || input > int.MaxValue);
            return input;
        }

        private static int AskForItems()
        {
            int entriesAmount = 0;
            ConsoleKeyInfo input;

            do
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine("How many items would you like to add?");
                        input = Console.ReadKey(true);

                        if (char.IsDigit(input.KeyChar))
                        {
                            entriesAmount = int.Parse(input.KeyChar.ToString());
                        }
                    }
                    else if (i < 3)
                    {
                        Console.WriteLine("Please enter a valid number (between 1-9)");
                        input = Console.ReadKey(true);

                        if (char.IsDigit(input.KeyChar))
                        {
                            entriesAmount = int.Parse(input.KeyChar.ToString());
                        }
                    }
                    else if (i == 3)
                    {
                        Console.WriteLine("Please just press on a button with a number between 1-9 on it");
                        input = Console.ReadKey(true);

                        if (char.IsDigit(input.KeyChar))
                        {
                            entriesAmount = int.Parse(input.KeyChar.ToString());
                        }
                    }
                    if (entriesAmount != 0) break;
                }
            } while (!(entriesAmount > 0 && entriesAmount < 10));
            return entriesAmount;
        }

        private static void RemoveItem()
        {
            itemsKeyList = items.Keys.ToList();
            itemsValueList = items.Values.ToList();
            ConsoleKeyInfo inputYesOrNo = new ConsoleKeyInfo((char) ConsoleKey.A, ConsoleKey.A, false, false, false);
            stopKey = "";
            int input;
            PrintDict();
            Console.WriteLine();
            if (items.Count == 1)
                Console.WriteLine("Choose the item you would like to remove by pressing 1 or n to cancel");
            else Console.WriteLine($"Choose the item you would like to remove by pressing 1 - {items.Count} or n to cancel");

            do
            {
            input = InputValidationCharInt();
                if (stopKey == "N")
                    return;
            } while (input < 1 || input > items.Count);

            Console.WriteLine();
            Console.WriteLine($"Do you really want to delete {itemsKeyList[input - 1]}?");
            Console.WriteLine("Press y for yes and n for no");
            while (inputYesOrNo.Key != ConsoleKey.Y && inputYesOrNo.Key != ConsoleKey.N)
            {
                inputYesOrNo = Console.ReadKey(true);
            }
            if (inputYesOrNo.Key == ConsoleKey.N)
                return;

            items.Remove(itemsKeyList[input - 1]);
            Console.WriteLine("-------------------------");
            Console.WriteLine($"You successfully removed {itemsKeyList[input - 1]}");
            PressAnyKey();
        }

        private static void ViewList()
        {
            PrintDict();
            PressAnyKey();
        }

        private static void PrintDict()
        {
            int i = 0;
            foreach (KeyValuePair<string, int> kvp in items)
            {
                i++;
                Console.Write($"{i}. ");
                Console.WriteLine("{0}, {1}kr", kvp.Key, kvp.Value);
            }
        }

        private static void ChangeItem()
        {
            itemsKeyList = items.Keys.ToList();
            itemsValueList = items.Values.ToList();

            int input;
            int inputItem;
            ConsoleKeyInfo inputKey;

            stopKey = "";
            PrintDict();
            Console.WriteLine();
            if (items.Count == 1)
                Console.WriteLine("Choose the item you would like to edit by pressing 1 or n to cancel");
            else Console.WriteLine($"Choose the item you would like to edit by pressing 1 - {items.Count} or n to cancel");

            do
            {
                inputItem = InputValidationCharInt();
                if (stopKey == "N")
                    goto Stop;
            } while (inputItem < 1 || inputItem > items.Count);

            Console.WriteLine("-------");
            Console.WriteLine($"{itemsKeyList[inputItem - 1]} selected for editing");
            Console.WriteLine();
            Console.WriteLine("Select what you would like to change or n to cancel");
            Console.WriteLine();
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Price");
            Console.WriteLine();

            do
            {
                input = InputValidationCharInt();
                if (stopKey == "N")
                    goto Stop;
            } while (input < 1 || input > 2);

            switch (input)
            {
                case 1:
                    //change name of item
                    items.Remove(itemsKeyList[inputItem - 1]);
                    string changeName = ItemName();
                    try
                    {
                        items.Add(changeName, itemsValueList[inputItem - 1]);
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine($"The name '{changeName}' already exists, would you like to change the price of '{changeName}' instead? (press y for yes and n for no)");
                        do
                        {
                            inputKey = Console.ReadKey(true);
                        } while (inputKey.Key != ConsoleKey.N && inputKey.Key != ConsoleKey.Y);
                        if (inputKey.Key == ConsoleKey.Y)
                            items[changeName] = ItemValue();
                        items.Add(itemsKeyList[inputItem - 1], itemsValueList[inputItem - 1]);
                    }
                    break;
                case 2:
                    //change price of item
                    items[itemsKeyList[inputItem - 1]] = ItemValue();
                    break;
                default:
                    break;
            }
            Stop:;
        }

        private static void CheckMin()
        {
            int minValue = items.Values.Min();
            var matches = items.Where(pair => pair.Value == minValue).Select(pair => pair.Key);
            string[] keys = matches.ToArray();

            if (keys.Length != 1)
                Console.WriteLine($"The cheapest ingredients are {string.Join(", ", keys)} and costs {minValue}kr");
            else Console.WriteLine($"The cheapest ingredient is {keys[0]} and costs {minValue}kr");

            PressAnyKey();
        }

        private static void CheckMax()
        {
            int maxValue = items.Values.Max();
            var matches = items.Where(pair => pair.Value == maxValue).Select(pair => pair.Key);
            string[] keys = matches.ToArray();

            if (keys.Length != 1)
                Console.WriteLine($"The most expensive ingredients are {string.Join(", ", keys)} and costs {maxValue}kr");
            else Console.WriteLine($"The most expensive ingredient is {keys[0]} and costs {maxValue}kr");

            PressAnyKey();
        }

        private static int InputValidationCharInt()
        {
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey(true);
                if (input.Key == ConsoleKey.N)
                {
                    stopKey = "N";
                    break;
                }
            } while (!char.IsDigit(input.KeyChar));
            bool success = int.TryParse(input.KeyChar.ToString(), NumberStyles.Integer, null, out inputKey);
            return inputKey;
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