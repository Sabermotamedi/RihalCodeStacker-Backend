namespace Rihal.ReelRise.Application.Common.Utilities;
public static class DecimalExtensions
{
    public static string ToEnglishWords(this decimal budget)
    {
        string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string[] teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        string[] thousands = { "", "thousand", "million", "billion", "trillion" };

        if (budget == 0)
            return "Zero";

        if (budget < 0)
            return "Negative " + Math.Abs(budget).ToEnglishWords();

        string words = "";

        int i = 0;
        while (budget > 0)
        {
            int chunk = (int)(budget % 1000);
            if (chunk != 0)
            {
                string chunkWords = "";

                if (chunk >= 100)
                {
                    chunkWords += ones[chunk / 100] + " hundred ";
                    chunk %= 100;
                }

                if (chunk >= 11 && chunk <= 19)
                {
                    chunkWords += teens[chunk - 11] + " ";
                }
                else if (chunk == 10 || chunk >= 20)
                {
                    chunkWords += tens[chunk / 10] + " ";
                    chunk %= 10;
                }

                if (chunk >= 1 && chunk <= 9)
                {
                    chunkWords += ones[chunk] + " ";
                }

                chunkWords += thousands[i];
                words = chunkWords + " " + words;
            }

            budget /= 1000;
            i++;
        }

        return words.Trim();
    }
}
