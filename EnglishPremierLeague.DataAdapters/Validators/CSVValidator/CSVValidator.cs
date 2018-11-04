using EnglishPremierLeague.Common.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace EnglishPremierLeague.Data.Adapters.Validators.CSVValidator
{
	

	public class CSVValidator : DataValidator
	{
		private readonly ILogger<CSVValidator> logger;
		public char[] Delimiters { get; set; }
		public List<Column> Columns { get; set; }

		public CSVValidator(ILoggerFactory loggerFactory)
		{
			var columnList = new XmlSerializer(typeof(List<Column>), new XmlRootAttribute("Columns"));			
			Columns = (List<Column>) columnList.Deserialize(new FileStream(@".\Validators\CSVValidator\CSVTemplate.xml", FileMode.Open));
			Delimiters = new char[] { ',' };
			logger = loggerFactory.CreateLogger<CSVValidator>();
		}

		public override bool Validate(string rowData, bool isHeaderRow, out Team team)
		{
			bool isValid = false;
			team = null;
			var columnValues = rowData.Split(Delimiters);

			if (!ValidateColumnCount(columnValues.Length, Columns.Count))
			{
				logger.LogDebug("Column count does not match");
				if (isHeaderRow)
					throw new Exception("Column count does not match with the template");

				return isValid;
			}
				

			if (isHeaderRow)
			{
				foreach (var columnValue in columnValues)
				{
					if (!ValidateColumnName(columnValue, Columns.Find(t => t.Index == (Array.IndexOf(columnValues, columnValue) +1)).Name))
						return isValid;
				}
				isValid = true;
			}
			else
			{
				team = new Team();
				foreach (var columnValue in columnValues.Select((value, index) => new { index, value }))
				{
					Column column = Columns.Find(t => t.Index == (columnValue.index+1));
					var columnType = Type.GetType(column.Type);
					object convertedValue;
					if (ValidateColumnType(columnValue.value, columnType, out convertedValue))
					{
						var propertyInfo = (team.GetType()).GetProperty(column.PropertyName);
						if (propertyInfo != null)
							propertyInfo.SetValue(team, convertedValue);
					}
					else
					{
						return isValid;
					}
					
				}
				isValid = true;
			}

			return isValid;
		}

	}
}
