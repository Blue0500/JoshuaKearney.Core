using JoshuaKearney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Testing {

    public class Program {

        public static void Main(string[] args) {
            int i = 4;

            Console.WriteLine(i.GetComparison().Invoke(4, 5));
            Console.Read();
        }
    }
}