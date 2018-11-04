using EnglishPremierLeague.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace EnglishPremierLeague.Data.Adapters.Validators.CSVValidator
{
	

	public class CSVValidator : DataValidator
	{
		public char[] Delimiters { get; set; }
		public List<Column> Columns { get; set; }

		public CSVValidator()
		{
			var columnList = new XmlSerializer(typeof(List<Column>), new XmlRootAttribute("Columns"));
			
			Columns = (List<Column>) columnList.Deserialize(new FileStream(@".\Validators\CSVValidator\CSVTemplate.xml", FileMode.Open));
			Delimiters = new char[] { ',' };
		}

		public override bool Validate(string rowData, bool isHeaderRow, out Team team)
		{
			bool isValid = false;
			team = null;
			var columnValues = rowData.Split(Delimiters);

			if (!ValidateColumnCount(columnValues.Length, Columns.Count))
			{
				if (isHeaderRow)
					throw new Exception("Colum count does not match with the template");

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
				foreach (var columnValue in columnValues)
				{
					Column column = Columns.Find(t => t.Index == Array.IndexOf(columnValues, columnValue) + 1);
					var columnType = Type.GetType(column.Type);
					object convertedValue;
					if (ValidateColumnType(columnValue, columnType, out convertedValue))
					{
						var propertyInfo = (team.GetType()).GetProperty(column.PropertyName);
						if(propertyInfo != null)
							propertyInfo.SetValue(team, convertedValue);
					}
					
				}
				isValid = true;
			}

			return isValid;
		}

	}
}
