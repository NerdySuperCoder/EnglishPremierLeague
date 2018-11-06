using System;
using Xunit;
using EnglishPremierLeague;
using EnglishPremierLeague.Data.Adapters;
using Moq;
using EnglishPremierLeague.Data.Adapters.Validators;
using EnglishPremierLeague.Data.Adapters.Validators.CSVValidator;


namespace EnglishPremierLeague.Tests
{
    public class IntegrationTests : IClassFixture<Program>
    {
		
		[Fact]
		public void TestCSVWithValidData()
		{
			string[] passingArguments = new string[]
			{
				"-csv",
				"-filepath",
				@".\IntegrationTests\TestData\ValidData.csv"
			};

			var lowDifferenceTeam = Program.EnglishPremierLeague(passingArguments);

			Assert.Contains("8. Aston_Villa", lowDifferenceTeam);
			
		}

		[Fact]
		public void TestDATWithValidData()
		{
			string[] passingArguments = new string[]
			{
				"-dat",
				"-filepath",
				@".\IntegrationTests\TestData\ValidData.dat"
			};

			var lowDifferenceTeam = Program.EnglishPremierLeague(passingArguments);

			Assert.Contains("8. Aston_Villa", lowDifferenceTeam);

		}

		[Fact]
		public void TestUnorderdColumnsCSV()
		{
			string[] passingArguments = new string[]
			{
				"-csv",
				"-filepath",
				@".\IntegrationTests\TestData\UnorderedColumns.csv"
			};

			var exception = Assert.Throws<Exception>(() => Program.EnglishPremierLeague(passingArguments));

			Assert.Equal("Columns do not match the template", exception.Message);

		}

		[Fact]
		public void TestWithLessColumnsCSV()
		{
			string[] passingArguments = new string[]
			{
				"-csv",
				"-filepath",
				@".\IntegrationTests\TestData\LessColumns.csv"
			};
						
			var exception = Assert.Throws<Exception>(() => Program.EnglishPremierLeague(passingArguments));

			Assert.Equal("Column count does not match with the template", exception.Message);
		}

		[Fact]
		public void TestWithMoreColumnsCSV()
		{
			string[] passingArguments = new string[]
			{
				"-csv",
				"-filepath",
				@".\IntegrationTests\TestData\MoreColumns.csv"
			};

			var exception = Assert.Throws<Exception>(() => Program.EnglishPremierLeague(passingArguments));

			Assert.Equal("Column count does not match with the template", exception.Message);
		}


		[Fact]
		public void TestEmptyCSV()
		{
			string[] passingArguments = new string[]
			{
				"-csv",
				"-filepath",
				@".\IntegrationTests\TestData\Empty.csv"
			};

			var exception = Assert.Throws<Exception>(() => Program.EnglishPremierLeague(passingArguments));

			Assert.Equal("File is Empty", exception.Message);
		}

		[Fact]
		public void TestCSVWithSomeInValidData()
		{
			string[] passingArguments = new string[]
			{
				"-csv",
				"-filepath",
				@".\IntegrationTests\TestData\ValidDataWithSomeInvalidRows.csv"
			};

			var lowDifferenceTeam = Program.EnglishPremierLeague(passingArguments);

			Assert.Contains("8. Aston_Villa", lowDifferenceTeam);

		}
	}
}
