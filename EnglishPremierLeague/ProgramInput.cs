using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague
{
	public class ProgramInput
	{
		public InputType Input { get; set; }
		public string FilePath { get; set; }
		public bool ContainsHeaderRow { get; set; }
		public string CSVTemplatePath { get; set; }
		public string DATTemplatePath { get; set; }
		public LogLevel LogLevel { get; set; }

	}

	public enum InputType
	{
		CSV,
		DAT
	}

}
