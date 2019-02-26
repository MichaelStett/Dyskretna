using System;
using System.IO;

namespace Dyskretna
{
    static partial class Program
    {
        private static string dPath { get; set; }
        private static string[] ExpList { get; set; }
        public static void Init()
        {
            dPath = Directory.GetCurrentDirectory();
            dPath = dPath.Remove(dPath.Length - 23);
            Console.WriteLine(File.ReadAllText(Path.Combine(dPath, ".\\welcome.txt")));
            ExpList = File.ReadAllText(Path.Combine(dPath, ".\\test.txt")).Split(Environment.NewLine);
        }
        public static void Loop()
        {
        foreach(var e in ExpList)
                Console.WriteLine("Czy zdanie logiczne '{0}' jest prawdziwe? {1}\n", e, EvaluateExp.Calculate(e) ? "Tak!" : "Nie!");
        }

        public static void Exit()
        {
            Console.WriteLine("Wartość zdania logicznego można zmienić pod ścieżką: \n{0}", dPath);
            Console.WriteLine("Autor: Michał Tymejczyk");
            Console.ReadKey();
        }
    }
}
