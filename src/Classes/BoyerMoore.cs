using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Pro_Swapper.Algorithms
{
    //Pro Swappper uses offsets now so this class isnt needed
    //https://stackoverflow.com/a/61947989/12897035
    /*class BoyerMoore
    {
        /// <summary>
        /// Finds the first occurrence of <paramref name="pattern"/> in a stream
        /// </summary>
        /// <param name="s">The input stream</param>
        /// <param name="pattern">The pattern</param>
        /// <returns>The index of the first occurrence, or -1 if the pattern has not been found</returns>
        public static long IndexOf(Stream s, byte[] pattern)
        {
            // Prepare the bad character array is done once in a separate step
            var badCharacters = MakeBadCharArray(pattern);

            // We now repeatedly read the stream into a buffer and apply the Boyer-Moore-Horspool algorithm on the buffer until we get a match
            var buffer = new byte[Math.Max(2 * pattern.Length, 4096)];
            s.Position = 0;
            long offset = 0; // keep track of the offset in the input stream
            while (true)
            {
                int dataLength;
                if (offset == 0)
                {
                    // the first time we fill the whole buffer
                    dataLength = s.Read(buffer, 0, buffer.Length);
                }
                else
                {
                    // Later, copy the last pattern.Length bytes from the previous buffer to the start and fill up from the stream
                    // This is important so we can also find matches which are partly in the old buffer
                    Array.Copy(buffer, buffer.Length - pattern.Length, buffer, 0, pattern.Length);
                    dataLength = s.Read(buffer, pattern.Length, buffer.Length - pattern.Length) + pattern.Length;
                }

                var index = IndexOf(buffer, dataLength, pattern, badCharacters);
                if (index >= 0)
                    return offset + index; // found!
                if (dataLength < buffer.Length)
                    break;
                offset += dataLength - pattern.Length;
            }

            return -1;
        }

        // --- Boyer-Moore-Horspool algorithm ---
        // (Slightly modified code from
        // https://stackoverflow.com/questions/16252518/boyer-moore-horspool-algorithm-for-all-matches-find-byte-array-inside-byte-arra)
        // Prepare the bad character array is done once in a separate step:
        private static int[] MakeBadCharArray(byte[] pattern)
        {
            var badCharacters = new int[256];

            for (long i = 0; i < 256; ++i)
                badCharacters[i] = pattern.Length;

            for (var i = 0; i < pattern.Length - 1; ++i)
                badCharacters[pattern[i]] = pattern.Length - 1 - i;

            return badCharacters;
        }

        // Core of the BMH algorithm
        private static int IndexOf(byte[] value, int valueLength, byte[] pattern, int[] badCharacters)
        {
            int index = 0;

            while (index <= valueLength - pattern.Length)
            {
                for (var i = pattern.Length - 1; value[index + i] == pattern[i]; --i)
                {
                    if (i == 0)
                        return index;
                }

                index += badCharacters[value[index + pattern.Length - 1]];
            }

            return -1;
        }
    }*/
}
