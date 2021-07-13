using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CirclePlugin
{
    static class ReadHelper
    {
        public static int ReadIntValue()
        {
            int retVal = 0;
            bool validValue = false;
            do
            {
                validValue = int.TryParse(Console.ReadLine(), out retVal);
                if (!validValue)
                {
                    Console.WriteLine("Please enter a valid number");
                }
            } while (!validValue);

            return retVal;
        }
    }
}
