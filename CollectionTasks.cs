using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Practice3_Collections
{
    public static class CollectionTasks
    {
        public static void RunTask1_ArrayList()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("ЗАДАНИЕ 1: Работа с необобщенной коллекцией ArrayList");
            Console.WriteLine("==========================================================");

            ArrayList arrayList = new ArrayList();
            Random rnd = new Random(42);

            for (int i = 0; i < 5; i++)
            {
                arrayList.Add(rnd.Next(10, 100));
            }
            Console.WriteLine("а) После добавления 5 случайных чисел:");
            PrintArrayList(arrayList);

            string addedString = "Hello_NSTU";
            arrayList.Add(addedString);
            Console.WriteLine($"\nb) Добавлена строка \"{addedString}\":");
            PrintArrayList(arrayList);

            object elementToRemove = arrayList[1];
            arrayList.Remove(elementToRemove);
            Console.WriteLine($"\nc) Удален элемент со значением {elementToRemove}:");

            Console.WriteLine($"d) Текущее количество элементов: {arrayList.Count}");
            PrintArrayList(arrayList);

            object searchTarget = "Hello_NSTU";
            int foundIndex = arrayList.IndexOf(searchTarget);
            Console.WriteLine($"\ne) Поиск элемента \"{searchTarget}\": " +
                              (foundIndex != -1 ? $"Найден на позиции (индекс) {foundIndex}" : "Не найден"));
            Console.WriteLine();
        }

        private static void PrintArrayList(ArrayList list)
        {
            Console.Write("   Элементы: ");
            foreach (var item in list)
            {
                Console.Write($"[{item}] ");
            }
            Console.WriteLine();
        }

        public static void RunTask2_GenericPrimitive()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("ЗАДАНИЕ 2 (Вариант 2): Queue<int> и Dictionary<int, int>");
            Console.WriteLine("==========================================================");

            Queue<int> queue = new Queue<int>(new[] { 15, 25, 35, 45, 55, 65 });

            Console.Write("a) Исходная очередь Queue<int>: ");
            foreach (int num in queue) Console.Write($"{num} ");
            Console.WriteLine();

            int n = 2;
            Console.WriteLine($"\nb) Удаляем из очереди n = {n} элементов (метод Dequeue):");
            for (int i = 0; i < n && queue.Count > 0; i++)
            {
                int removed = queue.Dequeue();
                Console.WriteLine($"   Удален элемент: {removed}");
            }

            queue.Enqueue(100);
            queue.Enqueue(200);
            Console.Write("\nc) Очередь после добавления новых элементов (100, 200): ");
            foreach (int num in queue) Console.Write($"{num} ");
            Console.WriteLine();

            Dictionary<int, int> dict = new Dictionary<int, int>();
            int keyCounter = 1;
            foreach (int item in queue)
            {
                dict.Add(keyCounter++, item);
            }

            Console.WriteLine("\ne) Вторая коллекция Dictionary<int, int>:");
            foreach (KeyValuePair<int, int> kvp in dict)
            {
                Console.WriteLine($"   Ключ: {kvp.Key} -> Значение: {kvp.Value}");
            }

            int targetValue = 100;
            bool exists = dict.ContainsValue(targetValue);
            Console.WriteLine($"\nf) Поиск значения {targetValue} в словаре: {(exists ? "Найдено!" : "Не найдено")}");
            foreach (var kvp in dict)
            {
                if (kvp.Value == targetValue)
                    Console.WriteLine($"   Значение {targetValue} соответствует ключу {kvp.Key}");
            }
            Console.WriteLine();
        }

        public static void RunTask3_CustomTypeAndInterfaces()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("ЗАДАНИЕ 3: Queue<MobileDevice>, Dictionary и Интерфейсы");
            Console.WriteLine("==========================================================");

            Queue<MobileDevice> deviceQueue = new Queue<MobileDevice>();
            deviceQueue.Enqueue(new MobileDevice("Samsung", "Galaxy S24", 85000m, 168));
            deviceQueue.Enqueue(new MobileDevice("Apple", "iPhone 15", 95000m, 171));
            deviceQueue.Enqueue(new MobileDevice("Xiaomi", "14 Pro", 65000m, 223));
            deviceQueue.Enqueue(new MobileDevice("ASUS", "ROG Phone 8", 105000m, 225));

            Console.WriteLine("a) Первая коллекция Queue<MobileDevice>:");
            foreach (MobileDevice dev in deviceQueue)
            {
                Console.WriteLine("   " + dev);
            }

            MobileDevice dequeued = deviceQueue.Dequeue();
            Console.WriteLine($"\nb) Удален 1 элемент из начала очереди: {dequeued.Brand} {dequeued.Model}");

            MobileDevice honor = new MobileDevice("Honor", "Magic 6", 75000m, 206);
            deviceQueue.Enqueue(honor);
            Console.WriteLine($"c) Добавлен новый элемент: {honor.Brand} {honor.Model}");

            Dictionary<int, MobileDevice> deviceDict = new Dictionary<int, MobileDevice>();
            int id = 101;
            foreach (MobileDevice dev in deviceQueue)
            {
                deviceDict.Add(id++, dev);
            }

            Console.WriteLine("\ne) Вторая коллекция Dictionary<int, MobileDevice>:");
            foreach (KeyValuePair<int, MobileDevice> item in deviceDict)
            {
                Console.WriteLine($"   ID [{item.Key}] => {item.Value}");
            }

            MobileDevice searchDevice = new MobileDevice("Xiaomi", "14 Pro", 65000m, 223);
            Console.WriteLine($"\nf) Поиск устройства {searchDevice.Brand} {searchDevice.Model} в словаре:");
            if (deviceDict.ContainsValue(searchDevice))
            {
                Console.WriteLine("   Устройство успешно найдено в словаре!");
            }

            Console.WriteLine("\n--- Демонстрация IComparable (сортировка по Цене) ---");
            List<MobileDevice> sortableList = new List<MobileDevice>(deviceQueue);
            sortableList.Sort();
            foreach (var d in sortableList) Console.WriteLine("   " + d);

            Console.WriteLine("\n--- Демонстрация IComparer (сортировка по Весу) ---");
            sortableList.Sort(new MobileDeviceWeightComparer());
            foreach (var d in sortableList) Console.WriteLine("   " + d);

            Console.WriteLine("\n--- Демонстрация ICloneable (глубокое копирование) ---");
            MobileDevice original = new MobileDevice("Sony", "Xperia 1", 90000m, 187, 24, "Sony Center");
            MobileDevice clone = (MobileDevice)original.Clone();
            clone.Price = 50000m;
            clone.Warranty.Months = 6;

            Console.WriteLine("   Оригинал: " + original);
            Console.WriteLine("   Клон (изменен): " + clone);
            Console.WriteLine("   (Гарантия оригинала не изменилась — глубокое копирование работает корректно)");
            Console.WriteLine();
        }

        public static void RunTask4_ObservableCollection()
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine("ЗАДАНИЕ 4: Наблюдаемая коллекция ObservableCollection<T>");
            Console.WriteLine("==========================================================");

            ObservableCollection<MobileDevice> observableDevices = new ObservableCollection<MobileDevice>();
            observableDevices.CollectionChanged += OnDevicesCollectionChanged;

            Console.WriteLine("Добавляем элементы в ObservableCollection...");
            var dev1 = new MobileDevice("Google", "Pixel 8", 68000m, 187);
            var dev2 = new MobileDevice("OnePlus", "12", 72000m, 220);
            observableDevices.Add(dev1);
            observableDevices.Add(dev2);

            Console.WriteLine("\nУдаляем элемент из ObservableCollection...");
            observableDevices.Remove(dev1);
        }

        private static void OnDevicesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (MobileDevice newItem in e.NewItems)
                        Console.WriteLine($"   [Событие Add]: В коллекцию добавлен -> {newItem}");
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (MobileDevice oldItem in e.OldItems)
                        Console.WriteLine($"   [Событие Remove]: Из коллекции удален -> {oldItem}");
                    break;
            }
        }
    }
}