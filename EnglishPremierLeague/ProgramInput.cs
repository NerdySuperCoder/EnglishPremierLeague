using Microsoft.Extensions.Logging;

namespace EnglishPremierLeague
{
	public class ProgramInput
	{
		#region Public Properties
		public InputType Input { get; set; }
		public string FilePath { get; set; }
		public bool ContainsHeaderRow { get; set; }
		public string CSVTemplatePath { get; set; }
		public string DATTemplatePath { get; set; }
		public LogLevel LogLevel { get; set; }
		#endregion

	}

	public enum InputType
	{
		CSV,
		DAT
	}

}
