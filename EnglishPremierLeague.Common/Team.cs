using System;

namespace EnglishPremierLeague.Common
{
	public class Team
	{
		public string TeamName { get; set; }
		public int NumberOfPlayed { get; set; }
		public int NumberOfWins { get; set; }
		public int NumberOfLosses { get; set; }
		public int NumberOfDraws { get; set; }
		public int NumberOfGoalsScored { get; set; }
		public int NumberOfGoalsScoredAgainst { get; set; }
		public int Points { get; set; }
	}
}
