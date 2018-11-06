using System;
using Xunit;
using EnglishPremierLeague;
using EnglishPremierLeague.Data.Adapters;
using Moq;
using EnglishPremierLeague.Data.Adapters.Validators;
using EnglishPremierLeague.Data.Adapters.Validators.CSVValidator;


namespace EnglishPremierLeague.Tests
{
    public class CSVValidatorTests : IClassFixture<Program>
    {
		//[Fact(Skip ="Testing")]        

		//public void Test1()
		//{
		//    Program p = new Program();
		//    Assert.Equal(4, p.Add(2, 2));
		//}

		//[Theory]
		//[InlineData(2,2,4)]
		//[InlineData(1, 2, 3)]
		//[InlineData(2, -1, 1)]
		//[InlineData(2, -12, -10)]
		//public void Test2(int value1, int value2, int expected)
		//{
		//    Program p = new Program();
		//    Assert.Equal(expected, p.Add(value1, value2));
		//}
		[Fact]
		public void TestRepository()
		{
			
		}

    }
}
