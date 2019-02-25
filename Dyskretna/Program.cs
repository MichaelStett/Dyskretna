using System;
using System.IO;

namespace Dyskretna
{
    static class Program
    {
        static void Main(string[] args)
        {
            var dPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var Wlc = File.ReadAllText(Path.Combine(dPath, ".\\welcome.txt"));            
            var Exp = File.ReadAllText(Path.Combine(dPath, ".\\test.txt"));
            Console.WriteLine(Wlc);
            Console.Write("Czy zdanie logiczne '{0}' jest prawdziwe? ", Exp);
            Console.WriteLine(EvaluateExpression.Calculate(Exp) ? "Tak!" : "Nie!");
            Console.WriteLine(Directory.GetCurrentDirectory());
        }
    }
}