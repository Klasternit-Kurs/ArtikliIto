using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikliIto
{
	class Program
	{
		static List<Artikal> Artikli = new List<Artikal>();

		static void Main(string[] args)
		{

			/*int broj = 9;
			decimal brojSaOstatkom = broj;
			Console.WriteLine(broj / 2);
			Console.WriteLine(brojSaOstatkom / 2);

			double aa = 9.999999;
			int b = (int)aa;
			Console.WriteLine(aa);
			Console.WriteLine(b);*/

			

			string unos;
			do
			{
				Meni();
				unos = Console.ReadKey().KeyChar.ToString();
				Console.WriteLine();
				switch(unos)
				{
					case "1":
						Unos();
						break;
					case "2":
						Ispis();
						break;
					case "7":
						Console.WriteLine("Bye :)");
						break;
				}

			} while (unos != "7");

			Console.ReadKey();
		}

		static void Ispis()
		{
			Console.WriteLine("=============================================================");
			foreach(Artikal a in Artikli)
			{
				Console.WriteLine($"{a.sifra}-{a.naziv} Kolicina:{a.kolicina} Ulazna:{a.ucena} Marza:{a.marzaProc}% Izlazna:{a.DajIzlaznuCenu()}");
			}
			Console.WriteLine("=============================================================");
		}

		static void Unos()
		{
			Console.Write("Unesite sifru: ");
			string s = Console.ReadLine();
			Console.Write("Unesite naziv: ");
			string n = Console.ReadLine();
			Console.Write("Unesite ulaznu cenu: ");
			decimal c = decimal.Parse(Console.ReadLine());
			Console.Write("Unesite marzu: ");
			int m = int.Parse(Console.ReadLine());
			Console.Write("Unesite kolicinu: ");
			int k = int.Parse(Console.ReadLine());
			Artikli.Add(new Artikal(s, n, c, m, k));
		}

		static void Meni()
		{
			Console.WriteLine("1-Dodavanje artikla");
			Console.WriteLine("2-Stampanje artikla");
			Console.WriteLine("3-Izmena artikla");
			Console.WriteLine("4-Brisanje artikla");
			Console.WriteLine("5-Izdavanje racuna");
			Console.WriteLine("6-Pregled racuna");
			Console.WriteLine("7-Izlaz");
			Console.WriteLine("---------------------------");
			Console.Write("Unesite izbor: ");
		}
	}

	

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
