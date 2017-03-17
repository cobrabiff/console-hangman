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

        //Encapsulation
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
                return ' ';
            }
            set
            {
                guess = value;
            }
        }

        string[] wordBank = { "framework", "dotnet", "programming", "development", "microsoft" };

        //List of incorrect letters guessed
        //static List<char> incorrectGuesses = new List<char>();
        static char[] incorrectGuesses = new char[26];
        static char[] alphabet = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        //alternative
        static int incorrectGuessCount = 0;

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
        static bool initialize()
        {
            guessesLeft = int.Parse(ConfigurationManager.AppSettings["NumberOfGuesses"]);

            word = ConfigurationManager.AppSettings["WordToGuess"];

            //Random random = new Random((int)DateTime.Now.Ticks);

            if (word != null && word != string.Empty)
            {
                wordToUpper = word.ToUpper();
                puzzleDisplay = new StringBuilder();
                for (int i = 0; i < word.Length; i++)
                {
                    puzzleDisplay.Append("*");
                }
                return true;
            }
            else
            {
                Console.WriteLine("Word is not valid.");
                //Console.ReadLine();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            if (initialize())
            {

                Console.WriteLine("Welcome to hangman!");

                Console.WriteLine(puzzleDisplay.ToString());

                while (!hasWon && guessesLeft > 0)
                {

                    Console.WriteLine("You have " + guessesLeft + " guesses left");

                    Console.WriteLine("Please enter your guess as a single letter:");


                    Console.WriteLine("Letters chosen:");

                    for (int i = 0; i < incorrectGuesses.Length; i++)
                    {
                        Console.Write(incorrectGuesses[i]);
                    }
                    Console.WriteLine();

                    input = Console.ReadLine().ToUpper();



                    Guess = input[0];

                    Console.WriteLine("Your guess is " + Guess.Value);

                    //get the result of their first guess and check if they have already attempted this guess.
                    if (correctGuesses.Contains(Guess.Value))
                    {
                        Console.Clear();
                        Console.WriteLine("You've already correctly guessed this letter. Guess again.");
                        Console.WriteLine(puzzleDisplay.ToString());
                        continue;
                    }
                    else if (incorrectGuesses.Contains(Guess.Value))
                    {
                        Console.Clear();
                        Console.WriteLine("You've already incorrectly guessed this letter. Guess again.");
                        Console.WriteLine(puzzleDisplay.ToString());
                        continue;
                    }

                    if (wordToUpper.Contains(Guess.Value))
                    {
                        Console.Clear();
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
                        Console.Clear();

                        Console.WriteLine("Incorrect!");

                        incorrectGuesses[incorrectGuessCount] = Guess.Value;

                        //for(int i = 0; i < alphabet.Length; i++)
                        //{
                        //    if (alphabet[i] == Guess.Value)
                        //    {
                        //        incorrectGuesses[i] = alphabet[i];
                        //    }
                        //}

                        incorrectGuessCount++;

                        //incorrectGuesses.Add(Guess.Value);

                        guessesLeft--;
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
                //Console.ReadLine();
            }
            Console.ReadLine();
        }
    }
}