using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Hangman
{
    public class Program
    {

        static string word = string.Empty;

        private static string input = string.Empty;

        public static string Input
        {
            get
            {
                return input;
            }
            
        }
             
        private static char? guess;

        public static char? Guess
        {
            get
            {
                //System.Nullable<char> c;

                if (guess.HasValue && !char.IsNumber(guess.Value))
                {
                    return guess;
                }
                //Console.WriteLine("Cannot contain numbers.");
                //Console.ReadLine();
                return null;
            }
            set
            {
                guess = value;
            }
        }


        //List of incorrect letters guessed
        static List<char> incorrectGuesses = new List<char>();
        
        //List of correct letters guessed
        static List<char> correctGuesses = new List<char>();

        static int guessesLeft = 0;
        static bool hasWon = false;
        static int lettersRevealed = 0;
        static string wordToUpper = string.Empty;
        static StringBuilder puzzleDisplay = null;

        /// <summary>
        /// 
        /// </summary>
        static void Initialize()
        {
            guessesLeft = int.Parse(ConfigurationManager.AppSettings["NumberOfGuesses"]);
            word = ConfigurationManager.AppSettings["WordToGuess"];
            wordToUpper = word.ToUpper();
            puzzleDisplay = new StringBuilder();
            for (int i = 0; i < word.Length; i++)
            {
                puzzleDisplay.Append("*");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Initialize();

            Console.WriteLine("Welcome to hangman!");
          
            while (!hasWon && guessesLeft > 0)
            {

                Console.WriteLine("You have " + guessesLeft + " guesses left");

                Console.WriteLine("Please enter your guess as a single letter:");

                input = Console.ReadLine().ToUpper();
                Guess = input[0];

                if (Guess.HasValue)
                {
                    Console.WriteLine("Your guess is " + Guess.Value);

                    //get the result of their first guess and check if they have already attempted this guess.
                    if (correctGuesses.Contains(Guess.Value))
                    {
                        Console.WriteLine("You've already correctly guessed this letter. Guess again.");
                        continue;
                    }
                    else if (incorrectGuesses.Contains(Guess.Value))
                    {
                        Console.WriteLine("You've already incorrectly guessed this letter. Guess again.");
                        continue;
                    }

                    if (wordToUpper.Contains(Guess.Value))
                    {
                        Console.WriteLine("Correct!");
                        correctGuesses.Add(Guess.Value);
                        for (int i = 0; i < wordToUpper.Length; i++)
                        {
                            if (wordToUpper[i] == Guess.Value)
                            {
                                puzzleDisplay[i] = wordToUpper[i];
                                lettersRevealed++;
                            }
                        }
                        if (lettersRevealed == word.Length)
                        {
                            hasWon = true;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Incorrect!");
                        incorrectGuesses.Add(Guess.Value);
                        guessesLeft--;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid entry. Try again.");
                }
                Console.WriteLine(puzzleDisplay.ToString());
            }

            if (hasWon)
            {
                Console.WriteLine("Winner, winner, chicken dinner!");
            }
            else
            {
                Console.WriteLine("Loser!");
            }
            Console.ReadLine();
        }
    }
}
