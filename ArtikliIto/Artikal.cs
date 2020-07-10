using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikliIto
{
	class Artikal
	{
		public string sifra;
		public string naziv;
		public decimal ucena;
		public int marzaProc;
		public int kolicina;

		public decimal DajIzlaznuCenu()
		{
			return ucena * (1 + (decimal)marzaProc / 100);
		}

		public Artikal(string a, string b, decimal c, int d, int e)
		{
			sifra = a;
			naziv = b;
			ucena = c;
			marzaProc = d;
			kolicina = e;
		}
	}
}
