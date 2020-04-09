using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using lab_10;

namespace lab_11
{
    internal static class Program
    {
        static int _find;

        public static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная 11. Вариант 30. Главное меню. \n");
            MainMenu();
        }

        private static void MainMenu()
        {
            int userChoice;
            do
            {
                Console.Clear();
                Console.WriteLine(
                    "Задание 1. Stack, обычная коллекция. \nЗадание 2. Dictionary<K, T>, обобщенная коллекция. \nЗадание 3. Сравнение коллекций Stack<T> и Dictionary <K,T> \n0 - Выход");
                userChoice = InputIntInterval(0, 3, "\nВыберите номер задания от 0 до 3: ");
                Console.Clear();
                switch (userChoice)
                {
                    // task 1 using Stack without type (error in my variant)
                    case 1:
                        Console.WriteLine("Task 1.");
                        Console.WriteLine("Создайте коллекцию элементов, используется Stack");
                        Stack arr1 = Generator.CreateCollectionFirstTask(
                            InputIntInterval(1, 5, "Количество Factory от 1 до 5: "),
                            InputIntInterval(1, 5, "Количество WorkShop от 1 до 5: "),
                            InputIntInterval(1, 5, "Количество Shop от 1 до 5: ")
                        );
                        MenuTask1(arr1);
                        break;
                    // task 2
                    case 2:
                        Console.WriteLine("Task 2. Добавление элементов в коллекцию Dictionary реализовано по одному.");
                        MenuTask2(new Dictionary<string, Production>());
                        break;
                    // task 3
                    case 3:
                        Console.WriteLine("Task 3. Test collection.");
                        int length = InputIntInterval(0, 10000000, "Введите длину коллекции: ");
                        MenuTask3(new TestCollection(length));
                        break;
                }

                // if 0 then EXIT
            } while (userChoice != 0);
        }

        private static void MenuTask1(Stack arr)
        {
            int userChoice;
            Console.Clear();

            do
            {
                Console.WriteLine("Task 1.");
                Console.WriteLine(
                    "Выберите пункт меню: \n1. Добавить элемент \n2. Удалить элемент \n3. Кол-во элементов заданного класса \n4. Печать элементов опр. класса \n5. Нахождение класса с самым большим кол-вом объектов \n" +
                    "6. Вывести коллекцию (foreach) \n7. Клонирование коллекции \n8. Сортировка и поиск с опр. кол-вом рабочих \n0. Выход в главное меню");
                userChoice = InputIntInterval(0, 8, "Выберите пункт меню от 0 до 8: ");
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        int objNumber = InputIntInterval(1, 3,
                            "Введите тип объекта для вставки (Factory-1, Workshop-2, Shop-3): ");
                        switch (objNumber)
                        {
                            case 1:
                                arr.Push(Generator.GenerateObject(new Factory()));
                                break;
                            case 2:
                                arr.Push(Generator.GenerateObject(new Workshop()));
                                break;
                            case 3:
                                arr.Push(Generator.GenerateObject(new Shop()));
                                break;
                        }

                        Console.WriteLine("Элемент добавлен в начало.");
                        break;
                    case 2:
                        arr.Pop();
                        Console.WriteLine("Элемент удален из стека.");
                        break;
                    case 3:
                        objNumber = InputIntInterval(1, 3,
                            "Выберите класс для вывода (Factory-1, Workshop-2, Shop-3): ");
                        int number = 0;

                        foreach (Production item in arr)
                        {
                            if (item is Factory && objNumber == 1)
                            {
                                number += 1;
                            }
                            else if (item is Workshop && objNumber == 2)
                            {
                                number += 1;
                            }
                            else if (item is Shop && objNumber == 3)
                            {
                                number += 1;
                            }
                        }

                        Console.WriteLine($"Количество элементов: {number}.");
                        break;
                    case 4:
                        objNumber = InputIntInterval(1, 3,
                            "Выберите класс для вывода (Factory-1, Workshop-2, Shop-3): ");
                        foreach (Production item in arr)
                        {
                            if (item is Factory && objNumber == 1)
                            {
                                item.ShowInfo();
                            }
                            else if (item is Workshop && objNumber == 2)
                            {
                                item.ShowInfo();
                            }
                            else if (item is Shop && objNumber == 3)
                            {
                                item.ShowInfo();
                            }
                        }

                        Console.WriteLine("Вывод окончен.");
                        break;
                    case 5:
                        int numberFactory = 0;
                        int numberWorkShop = 0;
                        int numberShop = 0;


                        foreach (Production item in arr)
                        {
                            if (item is Factory)
                            {
                                numberFactory += 1;
                            }
                            else if (item is Workshop)
                            {
                                numberWorkShop += 1;
                            }
                            else if (item is Shop)
                            {
                                numberShop += 1;
                            }
                        }

                        int max = Math.Max(numberShop, Math.Max(numberFactory, numberWorkShop));

                        if (max == numberShop)
                        {
                            Console.WriteLine("Кол-во Shop - max: " + max);
                        }

                        if (max == numberWorkShop)
                        {
                            Console.WriteLine("Кол-во WorkShop - max: " + max);
                        }

                        if (max == numberFactory)
                        {
                            Console.WriteLine("Кол-во Factory - max: " + max);
                        }

                        break;
                    case 6:
                        foreach (Production item in arr)
                        {
                            item.ShowInfo();
                        }

                        break;
                    case 7:
                        var copy = new Stack();
                        foreach (Production item in arr)
                        {
                            copy.Push(item.Clone());
                        }

                        Console.WriteLine("Стек скопирован.");
                        break;
                    case 8:
                        Array tmp = arr.ToArray();
                        Array.Sort(tmp, new SortByWorkersNumber());
                        arr = new Stack();
                        foreach (var item in tmp)
                        {
                            arr.Push(item);
                        }

                        Console.WriteLine("Стек отсортирован.");

                        foreach (Production item in arr)
                        {
                            item.ShowInfo();
                        }

                        _find = InputIntInterval(0, 100, "Введите кол-во рабочих для поиска элемента: ");

                        Console.WriteLine("Номер элемента: " + (Array.FindIndex(arr.ToArray(), Predicat) + 1));
                        break;
                }

                // if 0 then EXIT
            } while (userChoice != 0);
        }


        private static void MenuTask2(Dictionary<String, Production> arr)
        {
            int userChoice;
            Console.Clear();

            do
            {
                Console.WriteLine("Task 2.");
                Console.WriteLine(
                    "Выберите пункт меню: \n1. Добавить элемент \n2. Удалить элемент \n3. Кол-во элементов заданного класса \n4. Печать элементов опр. класса \n5. Нахождение класса с самым большим кол-вом объектов \n" +
                    "6. Вывести коллекцию (foreach) \n7. Клонирование коллекции \n8. Сортировка и поиск с опр. кол-вом рабочих \n0. Выход в главное меню");
                userChoice = InputIntInterval(0, 8, "Выберите пункт меню от 0 до 8: ");
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Введите ключ элемента (не может повторяться): ");
                        string key;
                        do
                        {
                            key = Console.ReadLine();
                        } while (arr.ContainsKey(key));

                        int objNumber = InputIntInterval(1, 3,
                            "Введите тип объекта для вставки (Factory-1, Workshop-2, Shop-3): ");
                        switch (objNumber)
                        {
                            case 1:
                                arr.Add(key, Generator.GenerateObject(new Factory()));
                                break;
                            case 2:
                                arr.Add(key, Generator.GenerateObject(new Workshop()));
                                break;
                            case 3:
                                arr.Add(key, Generator.GenerateObject(new Shop()));
                                break;
                        }

                        break;
                    case 2:
                        Console.WriteLine("Введите ключ элемента: ");
                        do
                        {
                            key = Console.ReadLine();
                        } while (!arr.ContainsKey(key));

                        arr.Remove(key);
                        Console.WriteLine("Элемент удален.");
                        break;
                    case 3:
                        objNumber = InputIntInterval(1, 3,
                            "Выберите класс для вывода (Factory-1, Workshop-2, Shop-3): ");
                        int number = 0;

                        foreach (KeyValuePair<string, Production> entry in arr)
                        {
                            if (entry.Value is Factory && objNumber == 1)
                            {
                                number += 1;
                            }
                            else if (entry.Value is Workshop && objNumber == 2)
                            {
                                number += 1;
                            }
                            else if (entry.Value is Shop && objNumber == 3)
                            {
                                number += 1;
                            }
                        }

                        Console.WriteLine($"Количество элементов: {number}.");
                        break;
                    case 4:
                        objNumber = InputIntInterval(1, 3,
                            "Выберите класс для вывода (Factory-1, Workshop-2, Shop-3): ");
                        foreach (KeyValuePair<string, Production> entry in arr)
                        {
                            if (entry.Value is Factory && objNumber == 1)
                            {
                                Console.Write(entry.Key + " - ");
                                entry.Value.ShowInfo();
                            }
                            else if (entry.Value is Workshop && objNumber == 2)
                            {
                                Console.Write(entry.Key + " - ");

                                entry.Value.ShowInfo();
                            }
                            else if (entry.Value is Shop && objNumber == 3)
                            {
                                Console.Write(entry.Key + " - ");

                                entry.Value.ShowInfo();
                            }
                        }

                        Console.WriteLine("Вывод окончен.");
                        break;
                    case 5:
                        int numberFactory = 0;
                        int numberWorkShop = 0;
                        int numberShop = 0;


                        foreach (KeyValuePair<string, Production> entry in arr)
                        {
                            if (entry.Value is Factory)
                            {
                                numberFactory += 1;
                            }
                            else if (entry.Value is Workshop)
                            {
                                numberWorkShop += 1;
                            }
                            else if (entry.Value is Shop)
                            {
                                numberShop += 1;
                            }
                        }

                        int max = Math.Max(numberShop, Math.Max(numberFactory, numberWorkShop));

                        if (max == numberShop)
                        {
                            Console.WriteLine("Кол-во Shop - max: " + max);
                        }

                        if (max == numberWorkShop)
                        {
                            Console.WriteLine("Кол-во WorkShop - max: " + max);
                        }

                        if (max == numberFactory)
                        {
                            Console.WriteLine("Кол-во Factory - max: " + max);
                        }

                        break;
                    case 6:
                        foreach (KeyValuePair<string, Production> entry in arr)
                        {
                            Console.Write(entry.Key + " - ");
                            entry.Value.ShowInfo();
                        }

                        break;
                    case 7:
                        Dictionary<string, Production> tmp = new Dictionary<string, Production>();
                        foreach (KeyValuePair<string, Production> entry in arr)
                        {
                            tmp.Add(entry.Key, (Production) entry.Value.Clone());
                        }

                        Console.WriteLine("Dictionary скопирован.");
                        break;
                    case 8:
                        Console.WriteLine("Dictionary не подлежит сортировке.");
                        break;
                }

                // if 0 then EXIT
            } while (userChoice != 0);
        }

        private static void MenuTask3(TestCollection collection)
        {
            int userChoice;
            Console.Clear();

            do
            {
                Console.WriteLine(
                    "1. Добавить элемент \n2. Измерить время нахождения \n3. Вывести коллекцию \n0. Выход в главное меню");
                userChoice = InputIntInterval(0, 3, "\nВыберите номер от 0 до 2: ");
                Console.Clear();
                string key;
                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("Введите уникальный ключ: ");
                        do
                        {
                            key = Console.ReadLine();
                        } while (collection.collection_1String.Contains(key));

                        collection.Add(key, new Shop {ShopName = key}, Generator.GenerateObject(new Shop()));

                        break;
                    case 2:
                        Console.WriteLine(
                            "Скорость нахождения первого, центрального, последнего и несуществующего элемента в коллекциях 1 типа:");

                        List<string> col1_list_string = collection.collection_1String.ToList();
                        List<Shop> col1_list_keyt = collection.collection_1TKey.ToList();

                        Shop tmpShop = new Shop {ShopName = "asdgjtjdfgajstjgjadjfKFEKSAKTGKSA"};

                        // first
                        Console.WriteLine("Коллекция типа Stack<string>");
                        Stopwatch stopWatch = new Stopwatch();

                        stopWatch.Start();
                        var contains = collection.collection_1String.Contains(col1_list_string[0]);
                        contains = collection.collection_1String.Contains(col1_list_string[col1_list_string.Count - 1]);
                        contains = collection.collection_1String.Contains(col1_list_string[col1_list_string.Count % 2]);
                        contains = collection.collection_1String.Contains("asdgjtjdfgajstjgjadjfKFEKSAKTGKSA");
                        stopWatch.Stop();

                        TimeSpan ts = stopWatch.Elapsed;

                        Console.WriteLine("RunTime " + ts);

                        // second
                        Console.WriteLine("\nКоллекция типа Stack<TKey>");
                        stopWatch = new Stopwatch();

                        stopWatch.Start();
                        contains = collection.collection_1TKey.Contains(col1_list_keyt[0]);
                        contains = collection.collection_1TKey.Contains(col1_list_keyt[col1_list_keyt.Count - 1]);
                        contains = collection.collection_1TKey.Contains(col1_list_keyt[col1_list_keyt.Count % 2]);
                        contains = collection.collection_1TKey.Contains(tmpShop);
                        stopWatch.Stop();

                        ts = stopWatch.Elapsed;

                        Console.WriteLine("RunTime " + ts + "\n");

                        // поиск по ключу
                        Console.Write("Введите ключ: ");
                        key = Console.ReadLine();
                        Shop TKey;
                        TKey = col1_list_string.Contains(key)
                            ? col1_list_keyt[col1_list_string.IndexOf(key)]
                            : new Shop {ShopName = "asdgjtjdfgajstjgjadjfKFEKSAKTGKSA"};

                        // second Key
                        Console.WriteLine("\nКоллекция типа Dictionary<string, TValue> поиск по ключу");
                        stopWatch = new Stopwatch();

                        stopWatch.Start();
                        contains = collection.collection_2StringTValue.ContainsKey(key);
                        stopWatch.Stop();

                        ts = stopWatch.Elapsed;

                        Console.WriteLine("RunTime " + ts + "\n");

                        // second Key
                        Console.WriteLine("\nКоллекция типа Dictionary<TKey, TValue> поиск по ключу");
                        stopWatch = new Stopwatch();

                        stopWatch.Start();
                        contains = collection.collection_2TKeyTValue.ContainsKey(TKey);
                        stopWatch.Stop();

                        ts = stopWatch.Elapsed;

                        Console.WriteLine("RunTime " + ts + "\n");

                        // поиск по value
                        Console.Write("Введите название Shop для поиска: ");
                        string value = Console.ReadLine();
                        Shop TValue = new Shop {ShopName = value};

                        // second Key
                        Console.WriteLine("\nКоллекция типа Dictionary<string, TValue> поиск по значению");
                        stopWatch = new Stopwatch();

                        stopWatch.Start();
                        contains = collection.collection_2StringTValue.ContainsValue(TValue);
                        stopWatch.Stop();

                        ts = stopWatch.Elapsed;

                        Console.WriteLine("RunTime " + ts + "\n");

                        // second value
                        Console.WriteLine("\nКоллекция типа Dictionary<TKey, TValue> поиск по значению");
                        stopWatch = new Stopwatch();

                        stopWatch.Start();
                        contains = collection.collection_2TKeyTValue.ContainsValue(TValue);
                        stopWatch.Stop();

                        ts = stopWatch.Elapsed;

                        Console.WriteLine("RunTime " + ts + "\n");

                        break;
                    case 3:
                        foreach (KeyValuePair<string, Shop> entry in collection.collection_2StringTValue)
                        {
                            Console.Write(entry.Key + " - ");
                            entry.Value.ShowInfo();
                        }

                        break;
                }

                // if 0 then EXIT
            } while (userChoice != 0);
        }


        private static int InputIntInterval(int min, int max, string msg)
        {
            Console.Write(msg);
            bool flag = int.TryParse(Console.ReadLine(), out var result);

            return flag
                ? result >= min && result <= max ? result : InputIntInterval(min, max, msg)
                : InputIntInterval(min, max, msg);
        }


        private static bool Predicat(object obj)
        {
            Production temp = (Production) obj;
            if (temp.WorkersNumber == _find)
            {
                return true;
            }

            return false;
        }

        private static class Generator
        {
            public static Random randomizer = new Random();

            public static Stack CreateCollectionFirstTask(int numberFactory, int numberWorkshop, int numberShop)
            {
                Stack tmp = new Stack();
                for (int i = 0; i < numberFactory; i++)
                {
                    tmp.Push(GenerateObject(new Factory()));
                }

                for (int i = 0; i < numberWorkshop; i++)
                {
                    tmp.Push(GenerateObject(new Workshop()));
                }

                for (int i = 0; i < numberShop; i++)
                {
                    tmp.Push(GenerateObject(new Shop()));
                }

                return tmp;
            }

            public static Factory GenerateObject(Factory tmp)
            {
                tmp.FactoryName = "factory_id-" + randomizer.Next(1, 125414);
                tmp.WorkersNumber = randomizer.Next(1, 100);
                return tmp;
            }

            public static Workshop GenerateObject(Workshop tmp)
            {
                tmp.ManagersNumber = randomizer.Next(1, 20);
                tmp.WorkersNumber = randomizer.Next(1, 100);
                return tmp;
            }

            public static Shop GenerateObject(Shop tmp)
            {
                tmp.ShopName = "shop_id-" + randomizer.Next(1, 14512);
                tmp.MainWorkerNumber = randomizer.Next(1, 15);
                tmp.WorkersNumber = randomizer.Next(1, 100);
                return tmp;
            }
        }

        public class TestCollection
        {
            public Stack<string> collection_1String;
            public Stack<Shop> collection_1TKey;

            public Dictionary<Shop, Shop> collection_2TKeyTValue;
            public Dictionary<string, Shop> collection_2StringTValue;

            public int Length;


            public TestCollection(int length)
            {
                // init collections 
                collection_1String = new Stack<string>();
                collection_1TKey = new Stack<Shop>();

                collection_2StringTValue = new Dictionary<string, Shop>();
                collection_2TKeyTValue = new Dictionary<Shop, Shop>();

                Length = length;

                for (int i = 0; i < Length; i++)
                {
                    // generate for keys collections
                    Shop tmpT = Generator.GenerateObject(new Shop());
                    string tmpString = "keyString-" + i;

                    // add to keys collections
                    collection_1String.Push(tmpString);
                    collection_1TKey.Push(tmpT);

                    // generate value
                    Shop tmpValue = Generator.GenerateObject(new Shop());

                    // add to key-value collections
                    collection_2TKeyTValue.Add(tmpT, tmpValue);
                    collection_2StringTValue.Add(tmpString, tmpValue);
                }
            }

            public void Add(string keyString, Shop keyT, Shop value)
            {
                if (!collection_1String.Contains(keyString) && !collection_1TKey.Contains(keyT))
                {
                    Length += 1;

                    // add to keys collections
                    collection_1String.Push(keyString);
                    collection_1TKey.Push(keyT);

                    // add to key-value collections
                    collection_2TKeyTValue.Add(keyT, value);
                    collection_2StringTValue.Add(keyString, value);
                }
                else
                {
                    throw new Exception("Duplicate key. It's must be unique.");
                }
            }
        }
    }
}