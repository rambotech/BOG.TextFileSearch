using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOG.TextFileSearch.Entity
{
	internal record FileMatchInformation(string FileName, int LineNumber, string LineContent);
}
