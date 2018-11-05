using EnglishPremierLeague.BusinessServices.Validators;
using EnglishPremierLeague.Common.Entities;
using EnglishPremierLeague.Data.Adapters;
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

		public IEnumerable<Team> ValidTeams
		{
			get
			{
				return _teams;
			}
		}
		//public IBusinessValidator BusinessValidator { get; set; }


		//public BusinessService(IEnumerable<Team> teams)
		//{
		//	validatedTeams = new List<Team>();
		//	BusinessValidator = new BusinessValidator();
		//	BusinessValidator.Validate(teams, out validatedTeams);
		//}

		public BusinessService(IDataAdapter dataAdapter, IBusinessValidator businessValidator, ILoggerFactory loggerFactory)
		{
			_businessValidator = businessValidator;
			_teams = dataAdapter.GetRepository();
			_logger = loggerFactory.CreateLogger<BusinessService>();
		}

		//public void SetRepository(IEnumerable<Team> teams)
		//{
		//	_teams = teams;
		//	_businessValidator.Validate(_teams, out validatedTeams);
		//}

		

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
