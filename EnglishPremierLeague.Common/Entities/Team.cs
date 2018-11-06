using System;

namespace EnglishPremierLeague.Common.Entities
{
	public class Team
	{
		#region Public Properties
		public string TeamName { get; set; }
		public int NumberOfPlayed { get; set; }
		public int NumberOfWins { get; set; }
		public int NumberOfLosses { get; set; }
		public int NumberOfDraws { get; set; }
		public int NumberOfGoalsScored { get; set; }
		public int NumberOfGoalsScoredAgainst { get; set; }
		public int Points { get; set; }

		public int GoalDifference
		{
			get
			{
				return Math.Abs(NumberOfGoalsScored - NumberOfGoalsScoredAgainst);
			}
		} 
		#endregion
	}
}
