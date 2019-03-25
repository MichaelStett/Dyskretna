using System;
using System.IO;

namespace Dyskretna
{
    partial class Program
    {
        /// <summary>
        /// Path To Current Directory.
        /// </summary>
        private static string ThePath { get; set; }
        /// <summary>
        /// List that Contains all expressions.
        /// </summary>
        private static string[] ExpList { get; set; }
        /// <summary>
        /// Reads content of .txt's.
        /// </summary>
        public static void Init()
        {
            ThePath = Directory.GetCurrentDirectory();
            ThePath = ThePath.Remove(ThePath.Length - 23);
            ThePath = Path.Combine(ThePath, ".\\Pliki");
            Console.WriteLine(File.ReadAllText(Path.Combine(ThePath, ".\\welcome.txt")));
            ExpList = File.ReadAllText(Path.Combine(ThePath, ".\\test.txt")).Split(Environment.NewLine);
        }
        /// <summary>
        /// Writes boolean answer for each expression.
        /// </summary>
        public static void Loop()
        {
            foreach (var e in ExpList)
                Console.WriteLine("- Czy zdanie logiczne '{0}' jest tautologią? {1}\n", e, EvExp.Calculate(e) ? "Tak!" : "Nie!");
        }
        /// <summary>
        /// Prints ending messege.
        /// </summary>
        public static void Exit()
        {
            Console.WriteLine("- Wartość zdania logicznego można zmienić pod ścieżką: \n- {0}", ThePath);
            Console.WriteLine(File.ReadAllText(Path.Combine(ThePath, ".\\bye.txt")));
        }
    }
}
