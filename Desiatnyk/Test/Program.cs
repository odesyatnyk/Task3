using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1;
using Task2;

namespace Test
{
    partial class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> tree = new BinaryTree<int>(2, 3, 5, 2, 4);
            foreach (var item in tree)
            {
                Console.WriteLine(item);
            }
            
            Console.ReadKey();
        }
    }
}
