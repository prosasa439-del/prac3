using System;
using System.Text;

namespace Practice3_Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            CollectionTasks.RunTask1_ArrayList();
            CollectionTasks.RunTask2_GenericPrimitive();
            CollectionTasks.RunTask3_CustomTypeAndInterfaces();
            CollectionTasks.RunTask4_ObservableCollection();

            Console.WriteLine("\nВсе задания успешно выполнены. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}