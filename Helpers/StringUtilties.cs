using System;
using System.Text.RegularExpressions;

namespace Helpers
{
    public static class StringUtilities
    {
        /// <summary>
        /// A <see cref="string[]"/> array containing all the values used to express a number in words.
        /// </summary>
        static string[] NumberWords = { "Zero", "One", "Two", "Three", "Four ", "Five", "Six ", "Seven ", "Eight ", "Nine ", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty", "Thirty", "Fourty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety", "Hundred", "Thousand", "Million", "Billion" };

        /// <summary>
        /// The Regular Expression <see cref="string"/> that does all the work of parsing the input <see cref="string"/> into its components.
        /// </summary>
        const string Reg = @"^((((?<hundredmillions>\b\S+\b )?(hundred ))?(?<tenmillions>\b\S+(ty|teen)\b ))?(?<millions>\b\S+\b )?(million ))?(((?<hundredthousands>\b\S+\b )?(hundred ))?((?<tenthousands>\b\S+(ty|teen)\b )?((?<thousands>\b\S+\b )?(thousand ))))?((?<hundreds>\b\S+\b )?(hundred ))?(?<tens>\b\S+(ty|teen)\b)?( )?(?<remainder>\b\S+\b)?$";

        /// <summary>
        /// Converts a number to a word equivalent. Can handle any <see cref="int"/> value.
        /// </summary>
        /// <param name="words"><see cref="int"/> containing the numerical value to convert.</param>
        /// <returns><see cref="string"/> containing the string representation of the value.</returns>
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        /// <summary>
        /// Converts words to a numerical equivalent. Can handle any <see cref="int"/> value.
        /// </summary>
        /// <param name="words"><see cref="string"/> containing the words to convert.</param>
        /// <returns><see cref="int"/> value of the words.</returns>
        public static double WordsToNumber(string words)
        {
            var rex = new Regex(Reg);

            words = words.ToLower();
            words = words.Replace(" and ", " ");
            words = words.Replace("-", " ");
            if (words == "zero")
                return 0;

            double number = 0;

            var m = rex.Match(words);

            if (m.Success)
            {
                if (m.Groups["hundredmillions"].Success && m.Groups["hundredmillions"].Value != "")
                {
                    number += GetRemainder(m.Groups["hundredmillions"].Value.Trim()) * 100000000;
                }

                if (m.Groups["tenmillions"].Success && m.Groups["tenmillions"].Value != "")
                {
                    number += GetTens(m.Groups["tenmillions"].Value.Trim()) * 1000000;
                }

                if (m.Groups["millions"].Success && m.Groups["millions"].Value != "" && m.Groups["millions"].Value != "million")
                {
                    number += GetRemainder(m.Groups["millions"].Value.Trim()) * 1000000;
                }

                if (m.Groups["hundredthousands"].Success && m.Groups["hundredthousands"].Value != "")
                {
                    number += GetRemainder(m.Groups["hundredthousands"].Value.Trim()) * 100000;
                }

                if (m.Groups["tenthousands"].Success && m.Groups["tenthousands"].Value != "")
                {
                    number += GetTens(m.Groups["tenthousands"].Value.Trim()) * 1000;
                }

                if (m.Groups["thousands"].Success && m.Groups["thousands"].Value != "" && m.Groups["thousands"].Value != "thousand")
                {
                    number += GetRemainder(m.Groups["thousands"].Value.Trim()) * 1000;
                }

                if (m.Groups["hundreds"].Success && m.Groups["hundreds"].Value != "" && m.Groups["hundreds"].Value != "hundred")
                {
                    number += GetRemainder(m.Groups["hundreds"].Value.Trim()) * 100;
                }

                if (m.Groups["tens"].Success && m.Groups["tens"].Value != "")
                {
                    number += GetTens(m.Groups["tens"].Value.Trim());
                }


                if (m.Groups["remainder"].Success && m.Groups["remainder"].Value != "")
                {
                    number += GetRemainder(m.Groups["remainder"].Value.Trim());
                }

                if (m.Groups["negative"].Success)
                {
                    number = 0 - number;
                }
            }
            return number;
        }

        /// <summary>
        /// Converts words to their numerical equivalent from zero to ninety (0 - 90).
        /// </summary>
        /// <param name="word"><see cref="string"/> containing the text to convert. Must be one of:
        /// "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
        /// </param>
        /// <returns><see cref="int"/> value of the input string.</returns>
        public static int GetTens(string word)
        {
            int number = 0;
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            int count = 0;
            foreach (var item in tensMap)
            {
                if (item == word)
                {
                    number += count * 10;
                    break;
                }
                count++;
            }
            return number;
        }

        /// <summary>
        /// Converts words to their numerical equivalent from zero to nineteen (0 - 19).
        /// </summary>
        /// <param name="word"><see cref="string"/> containing the text to convert. Must be one of:
        /// "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        /// </param>
        /// <returns><see cref="int"/> value of the input string.</returns>
        public static int GetRemainder(string word)
        {
            int number = 0;
            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

            int count = 0;
            foreach (var item in unitsMap)
            {
                if (item == word)
                {
                    number += count;
                    break;
                }
                count++;
            }
            return number;
        }

        /// <summary>
        /// Replaces words like "Sixty-three" or "Four Hundred" with their numerical equivalent, i.e.: 63 or 400.
        /// </summary>
        /// <param name="input"><see cref="string"/> containing the text to convert.</param>
        /// <returns><see cref="string"/> containing the converted text.</returns>
        public static string ReplaceWordsWithNumbers(string input)
        {
            char[] sep = { ' ' };
            string[] words = input.Split(sep);
            int count = 0;
            foreach (var word in words)
            {
                foreach (var item in NumberWords)
                {
                    if (word.ToLower().StartsWith(item.ToLower()))
                    {
                        words[count] = WordsToNumber(word.ToLower()).ToString();
                    }
                }
                count++;
            }
            return string.Join(" ", words);
        }

        /// <summary>
        /// Converts a <see cref="string"/> describing a numerical value to a 
        /// <see cref="long"/> containing that value.
        /// </summary>
        /// <param name="input"><see cref="string"/> describing the numerical value.</param>
        /// <returns><see cref="long"/> containing the numerical value.</returns>
        public static long GetBytesFromReadable(string input)
        {
            long output = 0;
            int digits;
            string suffix;
            string[] parts = new string[2];
            if (input.Contains(" "))
            {
                parts = input.Split(' ');
                digits = (int)Convert.ToDouble(parts[0]);
                suffix = parts[1];
            }
            else
            {
                digits = int.Parse(input);
                suffix = "";
            }

            if (suffix.Equals("EB"))
            {
                output = (digits << 50);
            }
            else if (suffix.Equals("PB"))
            {
                output = (digits << 40);
            }
            else if (suffix.Equals("TB"))
            {
                output = (digits << 30);
            }
            else if (suffix.Equals("GB"))
            {
                output = (digits << 20);
            }
            else if (suffix.Equals("MB"))
            {
                output = (digits << 10);
            }
            else if (suffix.Equals("KB"))
            {
                output = digits;
            }
            output *= 1024;
            return output;
        }

        /// <summary>
        /// Converts a <see cref="long"/> computer data measurement
        /// into a human-readable <see cref="string"/> describing that number.
        /// Returns the human-readable file size for an arbitrary, 64-bit file size
        /// The default format is "0.### XB", e.g. "4.2 KB" or "1.434 GB"
        /// </summary>
        /// <param name="i"><see cref="long"/> containing the computer data measurement.</param>
        /// <returns><see cref="string"/> describing the human-readable computer data measurement.</returns>
        public static string GetBytesReadable(long i)
        {
            // Get absolute value
            long absolute_i = (i < 0 ? -i : i);
            // Determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute_i >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (i >> 50);
            }
            else if (absolute_i >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (i >> 40);
            }
            else if (absolute_i >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (i >> 30);
            }
            else if (absolute_i >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (i >> 20);
            }
            else if (absolute_i >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (i >> 10);
            }
            else if (absolute_i >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = i;
            }
            else
            {
                return i.ToString("0 B"); // Byte
            }
            // Divide by 1024 to get fractional value
            readable = (readable / 1024);
            // Return formatted number with suffix
            return readable.ToString("0 ") + suffix;
        }
    }
}
