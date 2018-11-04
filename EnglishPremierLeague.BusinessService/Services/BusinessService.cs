using EnglishPremierLeague.BusinessServices.Validators;
using EnglishPremierLeague.Common.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnglishPremierLeague.BusinessServices.Services
{
	public class BusinessService : IBusinessService
	{
		private readonly IBusinessValidator _businessValidator;
		private readonly ILogger<BusinessService> _logger;

		private IEnumerable<Team> _teams;

		public List<Team> ValidTeams
		{
			get
			{
				return validatedTeams;
			}
		}
		//public IBusinessValidator BusinessValidator { get; set; }
		private List<Team> validatedTeams;

		//public BusinessService(IEnumerable<Team> teams)
		//{
		//	validatedTeams = new List<Team>();
		//	BusinessValidator = new BusinessValidator();
		//	BusinessValidator.Validate(teams, out validatedTeams);
		//}

		public BusinessService(IBusinessValidator businessValidator, ILoggerFactory loggerFactory)
		{
			_businessValidator = businessValidator;
			_logger = loggerFactory.CreateLogger<BusinessService>();
		}

		public void SetRepository(IEnumerable<Team> teams)
		{
			_teams = teams;
			_businessValidator.Validate(_teams, out validatedTeams);
		}

		

		public string GetTeamLeader()
		{
			throw new NotImplementedException();
		}

		public Team GetTeamWithLowDifferenceInGoals()
		{
			return ValidTeams.OrderBy(t => t.GoalDifference).FirstOrDefault();
		}
	}
}
