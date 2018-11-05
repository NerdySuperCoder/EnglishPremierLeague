using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishPremierLeague.Common.Entities
{
	[Serializable]
	public class Column
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public bool Mandatory { get; set; }
		public int Index { get; set; }
		public string PropertyName { get; set; }
		public int Length { get; set; }
		public int Offset { get; set; }
	}
}
