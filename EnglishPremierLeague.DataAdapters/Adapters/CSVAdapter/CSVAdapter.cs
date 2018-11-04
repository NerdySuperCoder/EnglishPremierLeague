using EnglishPremierLeague.Common;
using EnglishPremierLeague.DataAdapters.Parsers;
using EnglishPremierLeague.DataAdapters.Parsers.CSVParser;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace EnglishPremierLeague.DataAdapters.CSVAdapter
{
	public class CSVAdapter : DataAdapter
	{
		public IParser CSVParser { get; set; }

		public CSVAdapter()
		{
			CSVParser = new CSVParser();
		}

		public override IEnumerable<Team> GetData(string FilePath, bool containsHeaderRow)
		{
			List<Team> teamStandings = new List<Team>();

			//Check for file exists or not
			if (!File.Exists(FilePath))
				throw new FileNotFoundException();
			

			using (TextReader csvReader = new StreamReader(FilePath))
			{
				string line;
				bool headerRow = true;
				while ((line = csvReader.ReadLine()) != null)
				{					
					var team = CSVParser.Parse(line, headerRow);
					if (!headerRow && team!=null)
						teamStandings.Add(team);

					headerRow = false;
				}
				
			}
			return teamStandings;
		}
	}
}
