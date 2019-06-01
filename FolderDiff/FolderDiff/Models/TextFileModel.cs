using System.Text;
using System.Collections.Generic;

namespace FolderDiff.Models
{
	public class TextFileModel : FileModel
	{
		public Encoding GuessedEncoding { get; set; }

		public IEnumerable<string> Contents { get; set; }
	}
}
