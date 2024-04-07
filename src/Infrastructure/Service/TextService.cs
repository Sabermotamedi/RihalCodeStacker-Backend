using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Rihal.ReelRise.Application.Common.Interfaces;

namespace Rihal.ReelRise.Infrastructure.Service;
public class TextService : ITextService
{
    public List<string> ExtractUrls(string text)
    {
        List<string> urls = new List<string>();

        string pattern = @"(?<url>(http[s]?|ftp):\/\/[^'\s]+)";

        MatchCollection matches = Regex.Matches(text, pattern, RegexOptions.IgnoreCase);

        foreach (Match match in matches)
        {
            string url = match.Groups["url"].Value;
            urls.Add(url);
        }

        return urls;
    }


    public (int Id, string Name) GuessMovieTitle(string scrambled, Dictionary<int, string> movies)
    {
        var scrambledParts = scrambled.Split(' ').Select(part => NormalizeString(part)).ToList();

        foreach (var movie in movies)
        {
            var movieParts =  movie.Value.Split(' ');

            var normalizedMovieParts = movieParts.Select(part => NormalizeString(part)).ToList();

            bool allPartsMatch = false;
            for (int i = 0; i < normalizedMovieParts.Count; i++)
            {
                if (scrambledParts[0] == normalizedMovieParts[i])
                {
                    allPartsMatch = true;
                    break;
                }
            }

            if (allPartsMatch)
            {
                return (movie.Key, movie.Value);
            }
        }

        return (0, "No match found");
    }

    private string NormalizeString(string input)
    {
        // Convert to lowercase and sort the characters
        return new string(input.ToLower().OrderBy(c => c).ToArray());
    }
}
