using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> tokenizerlist = new List<string>();
            Console.WriteLine("A1.cs has Class1, Class2, Struct1 implmentaion");
            Console.WriteLine("Reading token from A1.cs and adding token in tokenizerlist manually since it is time consuming to do this task automatically.");
            tokenizerlist.Add("Class1");
            tokenizerlist.Add("Class2");
            tokenizerlist.Add("Struct1");

            Console.WriteLine("Searching in B2.cs file that whether it has used any token from  tokenizerlist");
            Console.WriteLine("List of token found which is used in B2 from A1:");
            string file = Path.GetFullPath("../../../B2/B2.cs");
            string contents = System.IO.File.ReadAllText(file);
            bool i = false;
            foreach (var b in tokenizerlist)
            {
                if (contents.Contains(b))
                {
                    i = true;
                    Console.WriteLine(b + " Found!");
                }
            }
            if (i)
            {
                Console.WriteLine("B1 depends on A1");
            }
            else
            {
                Console.WriteLine("B1 dont depends on A1");
            }
            Console.ReadLine();
        }
    }
}
