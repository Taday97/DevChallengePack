using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAPP
{
    public class PrimeNumbers
    {
        public string GetPrimeNumbers(int value)
        {
            var primeNumbers = new StringBuilder();

            for (int i=0 , count = 0; count < value; i++)
            {
                if (IsPrime(i))
                {
                    if (count > 0)
                    {
                        primeNumbers.Append(","); // Añadir coma antes de un nuevo número, excepto el primero
                    }
                    primeNumbers.Append(i.ToString());
                    
                    count++;
                }
            }
            return primeNumbers.ToString();
        }

        public bool IsPrime(int number)
        {
            bool isPrime = number switch
            {
                <= 1 => false,  // Numbers less than or equal to 1 are not prime
                2 => true,      // 2 is the only even prime number
                _ when number % 2 == 0 => false, // Exclude even numbers greater than 2
                _ => true       // Assume it's prime and verify further
            };

            if (isPrime)
            {
                // Check only odd numbers
                for (int i = 3; i <= Math.Sqrt(number); i += 2)
                {
                    if (number % i == 0)
                    {
                        isPrime = false; // If divisible by any number other than 1 and itself, it's not prime
                        break; // Exit the loop as soon as it's determined not to be prime
                    }
                }
            }

            return isPrime; // Return the result
        }
    }
}
