using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quartalsarbeit_GR1.Models
{
	public class DataPoint
	{
			public DataPoint(string kategorie, int anzahl)
			{
				this.Kategorie = kategorie;
				this.Anzahl = anzahl;
			}

			public string Kategorie = "";

			public int Anzahl = 0;
		
	}
}