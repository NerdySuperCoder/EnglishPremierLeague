using EnglishPremierLeague.BusinessServices.Validators;
using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public class BusinessService : IBusinessService
	{
		public List<Team> Teams
		{
			get
			{
				return validatedTeams;
			}
		}
		public IBusinessValidator BusinessValidator { get; set; }
		private List<Team> validatedTeams;

		public BusinessService(IEnumerable<Team> teams)
		{
			validatedTeams = new List<Team>();
			BusinessValidator = new BusinessValidator();
			BusinessValidator.Validate(teams, out validatedTeams);
		}

		public string GetTeamLeader()
		{
			throw new NotImplementedException();
		}

		public Team GetTeamWithLowDifferenceInGoals()
		{
			return Teams.OrderByDescending(t => t.GoalDifference).FirstOrDefault();
		}
	}
}
