using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rihal.ReelRise.Application.Common.Interfaces;
public interface ITextService
{
    List<string> ExtractUrls(string text);

    public (int Id, string Name) GuessMovieTitle(string scrambled, Dictionary<int, string> movies);
}
