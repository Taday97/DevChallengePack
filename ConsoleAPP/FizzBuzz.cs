using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAPP
{
    public class FizzBuzz
    {
        public string GetFizzBuzz(int value)
        {
            return value switch
            {
                int n when n % 3 == 0 && n % 5 == 0 => "FizzBuzz",
                int n when n % 3 == 0 => "Fizz",
                int n when n % 5 == 0 => "Buzz",
                _ => value.ToString()
            };
        }
        public string ListFizzBuzz()
        {
            List<int> a = Enumerable.Range(1, 100).ToList();
            var result = new StringBuilder();
            foreach (int value in a)
            {
                result.AppendLine(GetFizzBuzz(value));    
            }
            return result.ToString();
        }
    }
}
