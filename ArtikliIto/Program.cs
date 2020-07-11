using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArtikliIto
{
	//TODO treba napraviti snimanje artikala :)
	//TODO treba uzeti nas stari imenik i dodati snimanje i citanje iz fajlova :) 
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


			if (File.Exists("artikli.txt"))
			{
				foreach(string art in File.ReadLines("artikli.txt"))
				{  //  0    1    2   3 4
					//007;Plazam;49;34;56

					string[] polja = art.Split(';');
					Artikli.Add(new Artikal(polja[0], polja[1], decimal.Parse(polja[2]), int.Parse(polja[3]),
											int.Parse(polja[4])));
				}
			}

			if (File.Exists("racuni.txt"))
			{
				foreach(string rac in File.ReadLines("racuni.txt"))
				{
					string[] polja = rac.Split(';');
					Racun r = new Racun();
					r.Rbr = polja[0];
					List<string> poljaKaoLista = polja.ToList();
					poljaKaoLista.RemoveAt(0);

					Artikal zaRacun = null;
					for(int indeks = 0; indeks < poljaKaoLista.Count; indeks++)
					{
						if (indeks%2 == 0)
						{
							foreach(Artikal a in Artikli)
							{
								if (a.sifra == poljaKaoLista[indeks])
								{
									zaRacun = a;
									break;
								}
							}
						}else
						{
							r.ArtikliKolicine.Add(zaRacun, int.Parse(poljaKaoLista[indeks]));
						}
					}
					Racuni.Add(r);

				}
			}


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
						Izmena();
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
						Snimanje();
						break;
				}

			} while (unos != "7");

			Console.ReadKey();
		}

		static void Snimanje()
		{
			if (File.Exists("artikli.txt"))
			{
				File.Delete("artikli.txt");
			}
			foreach(Artikal a in Artikli)
			{
				File.AppendAllText("artikli.txt", $"{a.sifra};{a.naziv};{a.ucena};{a.marzaProc};{a.kolicina}" + Environment.NewLine);
			}
		}

		static void Izmena()
		{
			Console.Write("Unesite sifru: ");
			string sifra = Console.ReadLine();
			foreach (Artikal a in Artikli)
			{
				if (a.sifra == sifra)
				{
					Console.Write("Unesite novi naziv ili nista da preskocite: ");
					string unos = Console.ReadLine();
					if (unos != "")
					{
						a.naziv = unos;
					}

					Console.Write("Unesite novu ulaznu cenu ili nista da preskocite: ");
					unos = Console.ReadLine();
					if (unos != "")
					{
						a.ucena = decimal.Parse(unos);
					}

					Console.Write("Unesite novu marzu ili nista da preskocite: ");
					unos = Console.ReadLine();
					if (unos != "")
					{
						a.marzaProc = int.Parse(unos);
					}

					Console.Write("Unesite novu kolicinu ili nista da preskocite: ");
					unos = Console.ReadLine();
					if (unos != "")
					{
						a.kolicina = int.Parse(unos);
					}

					return;
				}
			}
			Console.WriteLine("Sifra ne postoji :(");
		}

		static void PrikazRacuna()
		{
			Console.WriteLine("=============================================================");
			foreach (Racun r in Racuni)
			{
				Console.WriteLine($"Racun: {r.Rbr}.");
				Console.WriteLine("---------------------");
				/*for(int indeks = 0; indeks < r.Art.Count; indeks++)
				{
					Console.WriteLine($"|{r.Art[indeks].sifra}-{r.Art[indeks].naziv} | {r.Art[indeks].DajIzlaznuCenu()} | {r.Kolicina[indeks]} | {r.Kolicina[indeks] * r.Art[indeks].DajIzlaznuCenu()}|");
				}*/

				foreach(Artikal a in r.ArtikliKolicine.Keys)
				{
					Console.WriteLine($"|{a.sifra}-{a.naziv} | {a.DajIzlaznuCenu()} | {r.ArtikliKolicine[a]} | {r.ArtikliKolicine[a] * a.DajIzlaznuCenu()}|");
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
				bool PrikaziGresku = true;
				foreach (Artikal a in Artikli)
				{
					if (a.sifra == sifra)
					{
						//if (r.Art.Contains(a))
						//{
						//	Console.WriteLine("Sadrzi!");
						//}

						//Ovo je resenje za dve liste 
						/*int indeksDuplikata = -1;


						for (int indeks = 0; indeks < r.Art.Count; indeks++)
						{
							if (r.Art[indeks] == a)
							{
								indeksDuplikata = indeks;
								break;
							}
						}

						if (indeksDuplikata == -1)
						{
							r.Art.Add(a);
						}*/ 

						int kolicina;
						do
						{
							Console.Write($"Unesite kolicinu (na stanju: {a.kolicina}): ");
							kolicina = int.Parse(Console.ReadLine());
							if (kolicina <= a.kolicina && kolicina > 0)
							{
								break;
							}
							Console.WriteLine("Losa kolicina :/");

						} while (true);

						if (r.ArtikliKolicine.ContainsKey(a))
						{
							r.ArtikliKolicine[a] += kolicina;
						} else
						{
							r.ArtikliKolicine.Add(a, kolicina);
						}

						/* Ovo je za dve liste
						if (indeksDuplikata == -1)
						{
							r.Kolicina.Add(kolicina);
						} else
						{
							r.Kolicina[indeksDuplikata] += kolicina;
						}*/

						a.kolicina -= kolicina;
						//a.kolicina -= r.Kolicina[r.Kolicina.Count - 1];
						Console.Write("Nastavite unos?(d/n): ");
						u = Console.ReadKey().KeyChar;
						Console.WriteLine();
						PrikaziGresku = false;
					}
				}
				if (PrikaziGresku)
				{
					Console.WriteLine("Sifre nema :(");
				}
			} while (u != 'n');
			r.Rbr = (Racuni.Count + 1).ToString();
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
			string s;
			bool NadjenDuplikat;
			do
			{
				NadjenDuplikat = false;
				Console.Write("Unesite sifru: ");
				s = Console.ReadLine();

				foreach (Artikal a in Artikli)
				{
					if (a.sifra == s)
					{
						Console.WriteLine("Sifra vec postoji!");
						NadjenDuplikat = true;
					}
				}

			} while (NadjenDuplikat);

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

	
	
}
