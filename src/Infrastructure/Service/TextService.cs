using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Rihal.ReelRise.Infrastructure.Service;
public class TextService
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
}
