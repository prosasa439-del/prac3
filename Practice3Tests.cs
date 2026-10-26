using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Practice3_Collections;

namespace Practice3_Collections.Tests
{
    [TestClass]
    public class ArrayListTests
    {
        [TestMethod]
        public void ArrayList_CanStoreMixedTypes()
        {
            ArrayList list = new ArrayList();
            list.Add(42);
            list.Add("Hello_NSTU");

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(42, list[0]);
            Assert.AreEqual("Hello_NSTU", list[1]);
        }

        [TestMethod]
        public void ArrayList_RemoveElement_DecreasesCount()
        {
            ArrayList list = new ArrayList { 10, 20, 30 };
            list.Remove(20);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(-1, list.IndexOf(20));
        }

        [TestMethod]
        public void ArrayList_IndexOf_ReturnsCorrectPosition()
        {
            ArrayList list = new ArrayList { "Test1", "Target", "Test2" };
            int index = list.IndexOf("Target");

            Assert.AreEqual(1, index);
        }
    }

    [TestClass]
    public class QueueAndDictionaryTests
    {
        [TestMethod]
        public void Queue_EnqueueDequeue_PreservesFifoOrder()
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);

            Assert.AreEqual(10, queue.Dequeue());
            Assert.AreEqual(20, queue.Dequeue());
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void Dictionary_PopulatedFromQueue_ContainsGeneratedKeys()
        {
            Queue<int> queue = new Queue<int>(new[] { 100, 200, 300 });
            Dictionary<int, int> dict = new Dictionary<int, int>();
            int key = 1;
            foreach (var item in queue)
            {
                dict.Add(key++, item);
            }

            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual(100, dict[1]);
            Assert.AreEqual(200, dict[2]);
            Assert.AreEqual(300, dict[3]);
        }

        [TestMethod]
        public void Dictionary_ContainsValue_FindsItem()
        {
            Dictionary<int, int> dict = new Dictionary<int, int>
            {
                { 1, 100 },
                { 2, 200 }
            };

            Assert.IsTrue(dict.ContainsValue(100));
            Assert.IsFalse(dict.ContainsValue(999));
        }
    }

    [TestClass]
    public class MobileDeviceInterfaceTests
    {
        [TestMethod]
        public void CompareTo_SortsByPriceAscending()
        {
            MobileDevice cheap = new MobileDevice("A", "Model1", 10000m, 150);
            MobileDevice expensive = new MobileDevice("B", "Model2", 50000m, 150);

            Assert.IsTrue(cheap.CompareTo(expensive) < 0);
            Assert.IsTrue(expensive.CompareTo(cheap) > 0);
            Assert.AreEqual(0, cheap.CompareTo(cheap));
        }

        [TestMethod]
        public void GenericListSort_UsesIComparable()
        {
            List<MobileDevice> list = new List<MobileDevice>
            {
                new MobileDevice("A", "1", 50000m, 200),
                new MobileDevice("B", "2", 10000m, 200),
                new MobileDevice("C", "3", 30000m, 200)
            };

            list.Sort();

            Assert.AreEqual(10000m, list[0].Price);
            Assert.AreEqual(30000m, list[1].Price);
            Assert.AreEqual(50000m, list[2].Price);
        }

        [TestMethod]
        public void WeightComparer_SortsByWeightThenBrand()
        {
            MobileDevice d1 = new MobileDevice("Xiaomi", "A", 20000m, 220);
            MobileDevice d2 = new MobileDevice("Apple", "B", 80000m, 170);
            MobileDevice d3 = new MobileDevice("Asus", "C", 50000m, 170);

            List<MobileDevice> list = new List<MobileDevice> { d1, d2, d3 };
            list.Sort(new MobileDeviceWeightComparer());

            Assert.AreEqual("Apple", list[0].Brand);
            Assert.AreEqual("Asus", list[1].Brand);
            Assert.AreEqual("Xiaomi", list[2].Brand);
        }

        [TestMethod]
        public void Clone_PerformsDeepCopyOfWarranty()
        {
            MobileDevice original = new MobileDevice("Sony", "Xperia", 90000m, 180, 24, "Center1");
            MobileDevice clone = (MobileDevice)original.Clone();

            clone.Price = 40000m;
            clone.Warranty.Months = 6;
            clone.Warranty.ServiceCenter = "Center2";

            Assert.AreEqual(90000m, original.Price);
            Assert.AreEqual(24, original.Warranty.Months);
            Assert.AreEqual("Center1", original.Warranty.ServiceCenter);

            Assert.AreEqual(40000m, clone.Price);
            Assert.AreEqual(6, clone.Warranty.Months);
            Assert.AreEqual("Center2", clone.Warranty.ServiceCenter);
        }

        [TestMethod]
        public void Equals_FindsMatchingDeviceInDictionary()
        {
            Dictionary<int, MobileDevice> dict = new Dictionary<int, MobileDevice>
            {
                { 101, new MobileDevice("Xiaomi", "14 Pro", 65000m, 223) }
            };

            MobileDevice target = new MobileDevice("Xiaomi", "14 Pro", 65000m, 223);

            Assert.IsTrue(dict.ContainsValue(target));
        }
    }

    [TestClass]
    public class ObservableCollectionTests
    {
        [TestMethod]
        public void CollectionChanged_TriggersOnAdd()
        {
            ObservableCollection<MobileDevice> collection = new ObservableCollection<MobileDevice>();
            bool eventTriggered = false;
            NotifyCollectionChangedAction action = NotifyCollectionChangedAction.Reset;

            collection.CollectionChanged += (s, e) =>
            {
                eventTriggered = true;
                action = e.Action;
            };

            collection.Add(new MobileDevice("Google", "Pixel", 60000m, 180));

            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(NotifyCollectionChangedAction.Add, action);
        }

        [TestMethod]
        public void CollectionChanged_TriggersOnRemove()
        {
            MobileDevice dev = new MobileDevice("Google", "Pixel", 60000m, 180);
            ObservableCollection<MobileDevice> collection = new ObservableCollection<MobileDevice> { dev };
            bool eventTriggered = false;
            NotifyCollectionChangedAction action = NotifyCollectionChangedAction.Reset;

            collection.CollectionChanged += (s, e) =>
            {
                eventTriggered = true;
                action = e.Action;
            };

            collection.Remove(dev);

            Assert.IsTrue(eventTriggered);
            Assert.AreEqual(NotifyCollectionChangedAction.Remove, action);
        }
    }
}