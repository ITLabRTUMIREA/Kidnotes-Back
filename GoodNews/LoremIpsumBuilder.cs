using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GoodNews
{
    /// <summary>
    /// Lorem Ipsum Generator
    /// </summary>
    /// <remarks>
    /// Based on Javascript Version of Marcus Campbell - Version 2.0 (Copyright 2003 - 2005) 
    /// Open-source code under the GNU GPL: http://www.gnu.org/licenses/gpl.txt 
    /// </remarks>
    /// <example>
    /// LoremIpsumBuilder _lib = new LoremIpsumBuilder();
    /// string test = _lib.GetLetters();
    /// </example>
    public static class LoremIpsumBuilder
    {
        private static readonly Regex WordSplitter = new Regex(@"\w", RegexOptions.Compiled);

        private const string Original = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        private static readonly List<string> _arrOriginal = new List<string>();

        static LoremIpsumBuilder()
        {
            _arrOriginal = Regex.Matches(Original, @"\w+").Select(m => m.Value).ToList();
        }

        public static string GetLorem(Random generator, int min, int max)
        {
            var str = string.Join(' ', Enumerable.Repeat(_arrOriginal, generator.Next(min, max)).Select(list => list[generator.Next(list.Count)]));
            return str;
        }
    }
}