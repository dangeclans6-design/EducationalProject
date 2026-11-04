using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace BankApplication
{
    internal class ForEngineTest
    {
        internal static void Main(string[] args)
        {
            List<int> new_list1 = new List<int> { 1, 2, 3, 4 };
            List<int> new_list2 = new List<int> { 5, 6, 7, 8 };

            for (int i = 0; i < new_list1.Count; i++)
            {
                Console.Write(new_list1[i] + "\t");
            }

            Console.WriteLine();
            new_list1 = new_list2;

            for (int i = 0; i < new_list1.Count; i++)
            {
                Console.Write(new_list1[i] + "\t");
            }
        }
    }
}
