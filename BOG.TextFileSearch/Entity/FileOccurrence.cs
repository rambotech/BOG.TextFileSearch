using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.TextFileSearch.Entity
{
	internal record FileOccurrence(string FileContent, List<int> MatchIndexes);
}
