using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NinjaTranslate {
    class Normalizer {
        public String normalize(String input) {
            input = removeInvalidWords(removeMultipleSpaces(removeRedundantInformation(removeBrackets(input))));
            if (input == null || input.Length == 0)
                return null;

            return input;
        }

        /// <summary>
        /// Checks if the string is valid. Removes every string that contains following characters: 
        /// '...', '...,' zahlen, '-', mehr als 3 wörter, 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static String removeInvalidWords(String input) {
            string pattern = @"^-|[\?§$%=*+~λ#]|[0-9]";
            Match m = Regex.Match(input, pattern, RegexOptions.IgnoreCase);
            if (m.Success || input.Split(' ').GetLength(0) > 3) {
                return null;
            }

            return input;
        }

        /// <summary>
        /// Removes following substrings in strings: 'to [take]', 'sb.', 'sth.', 'when sb.' 'what ...' 'until sb./sth.' 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static String removeRedundantInformation(String input) {
            // finds every "to" at the beginning of a string that in followed by a word.
            string regex1 = "\\Ato \\w+";
            //finds every 'sb.','sth.', 'sb./sth.' 'sth./sb.
            string regex2 = "sb\\.\\/sth\\.|sth\\.\\/sb\\.|sb\\.|sth\\.";

            //removes every "to " at the beginning of a string that is followed by a word.             
            Match m = Regex.Match(input, regex1, RegexOptions.IgnoreCase);
            if (m.Success && m.Index == 0) {
                input = input.Substring(m.Index + 3, input.Length - 3);
            }
            m = Regex.Match(input, regex2, RegexOptions.IgnoreCase);
            if (m.Success) {
                input = Regex.Replace(input, regex2, "");
            }
            return input;
        }

        /// <summary>
        /// Removes everything within brackets. Following brackets will be considered: (), [], {}, <>, "", '', »«
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static String removeBrackets(String input) {
            string regex = "(\\[.*?\\])|(\".*?\")|('.*?')|(\\(.*?\\))|(\\<.*?\\>)|(\\{.*?\\})|(\\».*?\\«)";
            return Regex.Replace(input, regex, "").Trim(); // removes leading and tailing whitespaces
        }

        /// <summary>
        /// Substitutes multiple whitespaces with one whitespace. 
        /// Also Removes removes leading and tailing whitespaces from String.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private static String removeMultipleSpaces(String input) {
            Regex regex = new Regex(@"[ ]{2,}");      // substitutes multiple whitespaces
            return regex.Replace(input, @" ").Trim(); // removes leading and tailing whitespaces
        }
    }
}
