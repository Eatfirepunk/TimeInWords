using System;
using System.Collections.Generic;
using System.Text;

namespace TimeInWordsChallenge
{
    internal class Program
    {
        /*
    * Complete the 'timeInWords' function below.
    *
    * The function is expected to return a STRING.
    * The function accepts following parameters:
    *  1. INTEGER h
    *  2. INTEGER m
    */
        //Dictionary to save the special notation when refering to time
        static Dictionary<int, string> keyWords = new Dictionary<int, string>(){
         {0,"o' clock"},
         {15,"quarter past"},
         {30,"half past"},
         {45,"quarter to"}
     };
        //Dictionary to hold string values for each number, this could have been an array
        // but on a dictionary it easier to diferentiate which value corresponds to which string
        // this array only counts to 29 because 30 is in the keyWords array and from 30 minutes on the count is done backwards
        static Dictionary<int, string> numbers = new Dictionary<int, string>(){
        {0,"zero"},
        {1,"one"},
        {2,"two"},
        {3,"three"},
        {4,"four"},
        {5,"five"},
        {6,"six"},
        {7,"seven"},
        {8,"eight"},
        {9,"nine"},
        {10,"ten"},
        {11,"eleven"},
        {12,"twelve"},
        {13,"thirteen"},
        {14,"fourteen"},
        {15,"fifteen"},
        {16,"sixteen"},
        {17,"seventeen"},
        {18,"eighteen"},
        {19,"nineteen"},
        {20,"twenty"},
        {21,"twenty one"},
        {22,"twenty two"},
        {23,"twenty three"},
        {24,"twenty four"},
        {25,"twenty five"},
        {26,"twenty six"},
        {27,"twenty seven"},
        {28,"twenty eight"},
        {29,"twenty nine"}
    };

        static string timeInWords(int h, int m)
        {
            //Declare our return variable which will be a string with the time in string format
            StringBuilder timeString = new StringBuilder();

            //We have to handle this special scenario as its the only one we return the hour at the                 //begining of the string
            if (m == 0)
            {
                //Append the hour at the beggining 
                timeString.Append(numbers[h]);
                timeString.Append(" ");
                //Append the o' clock at the end
                timeString.Append(keyWords[m]);
            }
            // handle special cases where the m variable corresponds to timespans of 15 minutes
            else if (m % 15 == 0)
            {
                // we need to display the next hour in case it's 45 minutes
                // but if it's 12 o clock the next hour is 1 and not 13
                int hour = m == 45 && h <= 11 ? h + 1 : h == 12 ? 1 : h;
                //Append the data from the keyword dictionary at the beggining
                // which corresponds to 15 minutes lapses quarter past, quarter to, half past
                timeString.Append(keyWords[m]);
                timeString.Append(" ");
                timeString.Append(numbers[hour]);
            }
            // in this scenario we will retrieve everything from the numbers dictionary, as there is no
            // special keyword handling
            else
            {
                // this will correspond to display the minutes past the hour i.e (one minute past five)
                if (m <= 30)
                {
                    // append the minutes that have passed after the hour
                    timeString.Append(numbers[m]);
                    // special case when the hour is just passed one minute we have to display minute not minutes
                    if (m == 1)
                    {
                        timeString.Append(" minute past ");
                    }
                    else // handle every other case before the half hour to display minutes instead of minute
                    {
                        timeString.Append(" minutes past ");
                    }
                    // append the hour at the end               
                    timeString.Append(numbers[h]);
                }
                // this will correspond to display the minutes to the hour i.e(thirteen minutes to six)
                else if (m > 30)
                {
                    // we need a new int to store how many minutes are left to the hour
                    // so that number will be the result of 60 - the minutes variable
                    int minutesTo = 60 - m;
                    //we also need the next hour while also handling the special case that 13 cannot follow 12 per constraint requirements in this case we will return 1
                    int nextHour = h == 12 ? 1 : h + 1;

                    //append the minutes that are left to the next hour
                    timeString.Append(numbers[minutesTo]);
                    // special case when the hour is just one minute from finish we append minute not minutes
                    if (minutesTo == 1)
                    {
                        timeString.Append(" minute to ");
                    }
                    else // handle every other case 
                    {
                        timeString.Append(" minutes to ");
                    }
                    // append the hour at the end               
                    timeString.Append(numbers[nextHour]);
                }

            }

            return timeString.ToString();
        }
        static void Main(string[] args)
        {
            //Variable to keep the program alive while trying different values
            bool tryAgain = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Write the time you want to convert to string in the following format hh:mm");
                string time = Console.ReadLine();

                // this is for debugging purposes we can write the time as one string formatted like hh:mm instead of first the hour and then the minutes
                if (!time.Contains(":") || time.Length > 5)
                {
                    Console.WriteLine("Invalid time string, please submit in format hh:mm include : in your string");
                }
                else
                {
                    var hourAndTime = time.Split(":");
                    int hour = int.Parse(hourAndTime[0]);
                    int minutes = int.Parse(hourAndTime[1]);

                    
                    // if the hour is more than 24 hours then this time is invalid , if the minutes are more than 59 then this time is also invalid
                    if (hour > 24 || minutes > 59)
                    {
                        Console.WriteLine("The hour should be submitted in 12 hour format and the minutes can't exceed 59");
                    }
                    else
                    {
                        // edge case if they submit the hour in 24 hour format if the time exceeds 12 then we now they submitted the time in 24 hours format so we just substract 12 to match our time constraints
                        hour = hour > 12 ? hour - 12 : hour;               
                        string result = timeInWords(hour, minutes);
                        Console.WriteLine(result);
                    }
                }
                Console.WriteLine("Try again ? y/n");
                tryAgain = Console.ReadKey().Key.ToString() == "Y";

            }
            while (tryAgain);
          
        }
    }
}
