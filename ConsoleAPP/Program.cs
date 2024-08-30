using ConsoleAPP;

internal class Program
{
    private static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("Select an action:");
            Console.WriteLine("1. Generate FizzBuzz Sequence from 1 to 100");
            Console.WriteLine("2. List Prime Numbers Up to a Specified Count");
            Console.WriteLine("3. Exit");

            int optionSelected;

            // Validate the user input
            if (int.TryParse(Console.ReadLine(), out optionSelected))
            {
                var fizzBuzz = new FizzBuzz();
                var primeNumbers = new PrimeNumbers();

                switch (optionSelected)
                {
                    case 1:
                        Console.Write("*****FizzBuzz Sequence from 1 to 100******\n");
                        Console.WriteLine(fizzBuzz.ListFizzBuzz());
                        break;
                    case 2:
                        int number;
                        bool isValidNumber = false;

                        while (!isValidNumber)
                        {
                            Console.Write("Please enter a number: ");
                            string input = Console.ReadLine()??"";

                            if (int.TryParse(input, out number))
                            {
                                isValidNumber = true; // The input is a valid number

                                Console.WriteLine("The prime numbers are these: " + primeNumbers.GetPrimeNumbers(number));
                            }
                            else
                            {
                                Console.WriteLine("Error: The input is not a valid number. Please try again.");
                            }
                        }
                        break;
                    case 3:
                        // Exit the application
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return; // Exits the while loop and ends the program
                    default:
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Error: The input is not a valid number. Please try again.");
            }

            Console.WriteLine("");
            Console.WriteLine("///");

        }
    }
}