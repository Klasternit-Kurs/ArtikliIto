using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtikliIto
{
	//TODO izmena -- Korisnik unese sifru artikla i mi mu ponudimo sta da izmeni
	//Jedan nacin sa d/n pitanjima. Izmena imena?(d/n) --Izmena ulazne cene(d/n)
	//Drugi nacin da se napravi meni kada se nadje sifra, pa da moze da izabere sta sve menja
	class Program
	{
		static List<Artikal> Artikli = new List<Artikal>();
		static List<Racun> Racuni = new List<Racun>();

		static void Main(string[] args)
		{			
			/*uint k = 0;
			Console.WriteLine(k);

			k--;
		
			Console.WriteLine(k);

			byte b = 255;
			Console.WriteLine(b);
			checked
			{
				b++;
			}
			Console.WriteLine(b);
			Console.ReadKey();

			int broj = 9;
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
					case "3":
						//Domaci :P 
						break;
					case "4":
						Brisanje();
						break;
					case "5":
						Izdavanje();
						break;
					case "6":
						PrikazRacuna();
						break;
					case "7":
						Console.WriteLine("Bye :)");
						break;
				}

			} while (unos != "7");

			Console.ReadKey();
		}

		static void PrikazRacuna()
		{
			Console.WriteLine("=============================================================");
			foreach (Racun r in Racuni)
			{
				Console.WriteLine($"Racun: {r.Rbr}.");
				Console.WriteLine("---------------------");
				for(int indeks = 0; indeks < r.Art.Count; indeks++)
				{
					Console.WriteLine($"|{r.Art[indeks].sifra}-{r.Art[indeks].naziv} | {r.Art[indeks].DajIzlaznuCenu()} | {r.Kolicina[indeks]} | {r.Kolicina[indeks] * r.Art[indeks].DajIzlaznuCenu()}|");
				}
				Console.WriteLine("---------------------");
			}
			Console.WriteLine("=============================================================");
		}

		static void Izdavanje()
		{
			//TODO napraviti petlju tako da trazi sifru od korisnika dokle god ne naidje na validnu
			//TODO proveriti kolicinu na stanju pre prodaje, ne smemo da prodamo vise nego sto imamo :)
			//TODO srediti tako da ne ispisuje da nema sifre i kada treba i kada ne :)
			Racun r = new Racun();
			char u = ' ';
			do
			{
				Console.Write("Unesite sifru: ");
				string sifra = Console.ReadLine();
				foreach (Artikal a in Artikli)
				{
					if (a.sifra == sifra)
					{
						//TODO Proveriti da li artikal vec postoji na racunu,
						//te ako postoji, dodati kolicino na vec postojeci unos
						Console.Write("Unesite kolicinu: ");
						r.Rbr = (Racuni.Count + 1).ToString();
						r.Art.Add(a);
						int kolicina = int.Parse(Console.ReadLine());
						r.Kolicina.Add(kolicina);
						a.kolicina -= kolicina;
						//a.kolicina -= r.Kolicina[r.Kolicina.Count - 1];
						Console.Write("Nastavite unos?(d/n): ");
						u = Console.ReadKey().KeyChar;
						Console.WriteLine();
					}
				}
				Console.WriteLine("Sifre nema :(");
			} while (u != 'n');
			Racuni.Add(r);
		}

		static void Brisanje()
		{
			Console.Write("Unesite sifru: ");
			string sifra = Console.ReadLine();
			foreach (Artikal a in Artikli)
			{
				if (a.sifra == sifra)
				{
					Artikli.Remove(a);
					return;
				}
			}
			Console.WriteLine("Sifra ne postoji :(");
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
			//TODO napraviti petlje kod unosa, te kada korisnik unese sifru ili naziv
			//koji vec postoje samo da se trazi ponovni unos
			Console.Write("Unesite sifru: ");
			string s = Console.ReadLine();
			foreach (Artikal a in Artikli)
			{
				if (a.sifra == s)
				{
					Console.WriteLine("Sifra vec postoji!");
					return;
				}
			}
			Console.Write("Unesite naziv: ");
			string n = Console.ReadLine();
			foreach (Artikal a in Artikli)
			{
				if (a.naziv == n)
				{
					Console.WriteLine("Naziv vec postoji!");
					return;
				}
			}
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

	
	class Racun
	{
		public string Rbr;                
		public List<int> Kolicina = new List<int>();   
		public List<Artikal> Art = new List<Artikal>();
		
		public decimal Total()
		{
			return 0;
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
