using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikliIto
{
	class Racun
	{
		public string Rbr;
		public Dictionary<Artikal, int> ArtikliKolicine = new Dictionary<Artikal, int>();

		public decimal Total()
		{
			decimal total = 0;

			foreach(Artikal a in ArtikliKolicine.Keys)
			{
				total += a.DajIzlaznuCenu() * ArtikliKolicine[a];
			}

			return total;
		}
	}
}
