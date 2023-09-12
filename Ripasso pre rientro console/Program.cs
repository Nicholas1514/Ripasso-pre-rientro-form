using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Ripasso_pre_rientro_console
{
    internal class Program
    {
        Fconsole f = new Fconsole();
        public static string nfile = @"Zappa.csv";
        static void Main(string[] args)
        {
            string path = @"Zappa.csv";
            string sep = ";";
            int l = 124;
            int scelta = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Menù");
                Console.WriteLine("1 - Aggiunta campo");
                Console.WriteLine("2 - Conta campi ");
                Console.WriteLine("3 - Lunghezza massima record");
                Console.WriteLine("4 - Spazi univoci record");
                Console.WriteLine("5 - Aggiunta in coda");
                Console.WriteLine("6 - Visualizza");
                Console.WriteLine("7 - Ricerca");
                Console.WriteLine("8 - Modifica");
                Console.WriteLine("9 - Cancellazione logica");
                Console.WriteLine("Inserisci la scelta");
                scelta = int.Parse(Console.ReadLine());
                switch (scelta)
                {
                    case 1:
                        string nomefile = @"Zappa.csv";
                        Aggiuntacampo(nomefile);
                        break;
                    case 2:
                        string nfile = @"Zappa.csv";
                        int contacampi = Contacampi(nfile);
                        break;
                    case 3:
                        int lung = Lunghmax();
                        Console.WriteLine("La lunghezza massima è: " + lung);
                        break;
                    case 4:
                        Spazi();
                        break;
                    case 5:
                        Console.WriteLine("Inserisci campo 1");
                        string c1 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 2");
                        string c2 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 3");
                        string c3 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 4");
                        string c4 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 5");
                        string c5 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 6");
                        string c6 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 7");
                        string c7 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 8");
                        string c8 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 9");
                        string c9 = Console.ReadLine();

                        Campi c = sp(c1, c2, c3, c4, c5, c6, c7, c8, c9);
                        Console.WriteLine(c.comune + c.provincia + c.regione + c.nome + c.anno + c.data + c.identificatore + c.longitudine + c.latitudine);
                        break;
                    case 6:
                        Console.WriteLine("Visualizza");
                        break;
                    case 7:
                        Console.WriteLine("Inserisci un elemento da ricercare");
                        string cerca = Console.ReadLine();
                        Ricerca(cerca, path);
                        break;
                    case 8:
                        Console.WriteLine("Inserisci campo per la modifica");
                        string mod = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 1 da modificare");
                        string ca1 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 2 da modificare");
                        string ca2 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 3 da modificare");
                        string ca3 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 4 da modificare");
                        string ca4 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 5 da modificare");
                        string ca5 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 6 da modificare");
                        string ca6 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 7 da modificare");
                        string ca7 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 8 da modificare");
                        string ca8 = Console.ReadLine();
                        Console.WriteLine("Inserisci campo 9 da modificare");
                        string ca9 = Console.ReadLine();
                        Modifica(mod, ca1, ca2, ca3, ca4, ca5, ca6, ca7, ca8, ca9, path,sep, l);
                        break;
                    case 9:
                        int ncampi = Contacampi(path);
                        Console.WriteLine("Inserisci l'elemento da cancellare");
                        string cancellato = Console.ReadLine();
                        CancellazioneLogica(cancellato, ncampi);
                        break;
                }


            } while (scelta != 0);
        }
        public struct Campi
        {
            public string comune;
            public string provincia;
            public string regione;
            public string nome;
            public int anno;
            public string data;
            public float identificatore;
            public string longitudine;
            public string latitudine;
        }

        public Campi c;
       
        public int ncampi;
        public int recordLenght = 124;
        public string sep = ";";
        public static int Contacampi( string nfile,char sep = ';',int ncampi = 0)
        {

            string[] linea = File.ReadAllLines(nfile);

            ncampi = linea[0].Split(sep).Length;

            return ncampi;
        }


        public static string Record(Campi c, int lunghrecord, string sp = ";")
        {

            return (c.comune + sp + c.provincia + sp + c.regione + sp + c.nome + sp + c.anno + sp + c.data + sp + c.identificatore + sp + c.longitudine + sp + c.latitudine).PadRight(lunghrecord - 4) + "##\r\n";

        }

        public static void AggProd(string riga, string nomefile)
        {

            var oStream = new FileStream(nomefile, FileMode.Append, FileAccess.Write, FileShare.Read);
            BinaryWriter writer = new BinaryWriter(oStream);
            char[] linea = riga.ToCharArray();
            writer.Write(linea);
            writer.Close();
            oStream.Close();
        }
        //valori dei campi settati per l'aggiunta
        public static Campi sp(string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, char sep = ';')
        {
            Campi c;
            c.comune = c1;
            c.provincia = c2;
            c.regione = c3;
            c.nome = c4;
            c.anno = int.Parse(c5);
            c.data = c6;
            c.identificatore = float.Parse(c7);
            c.longitudine = c8;
            c.latitudine = c9;
            return c;
        }

        public static int Ricerca(string nome, string nfile)
        {

            int riga = 0;
            using (StreamReader sr = File.OpenText(nfile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] dati = s.Split(';');

                    if (dati[0] == nome)
                    {
                        sr.Close();
                        return riga;
                    }
                    if (dati[1] == nome)
                    {
                        sr.Close();
                        return riga;
                    }
                    if (dati[4] == nome)
                    {
                        sr.Close();
                        return riga;
                    }

                    riga++;
                }
                sr.Close();
            }
            return -1;
        }

        public static void Aggiuntacampo(string nfile)
        {
            Random r = new Random();
            string[] arr = new string[1000];
            int riga = 0;
            using (StreamReader sr = new StreamReader(nfile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    if (riga == 0)
                    {
                        arr[riga] = s + ";miovalore;canc logica";
                    }
                    else
                    {
                        string x = (r.Next(10, 21)).ToString();
                        arr[riga] = s + ";" + x + ";" + "0";
                    }
                    riga++;

                }
            }
            using (StreamWriter sw = new StreamWriter(nfile))
            {
                riga = 0;
                while (arr[riga] != null)

                {
                    sw.WriteLine(arr[riga]);
                    riga++;
                }
            }
        }

        public static int Lunghmax()
        {
            int ncampi = Contacampi(nfile);
            int lungmax = 0;

            using (StreamReader sr = new StreamReader(nfile))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] dati = s.Split(';');
                    for (int i = 0; i < ncampi; i++)
                    {
                        if (dati[i].Length > lungmax)
                        {
                            lungmax = dati[i].Length;
                        }
                    }


                }
            }
            return lungmax;
        }

        public static void Spazi()
        {
            string[] linee = File.ReadAllLines(nfile);
            using (StreamWriter sw = new StreamWriter(nfile))
            {
                for (int i = 0; i < linee.Length; i++)
                {
                    linee[i] = linee[i].PadRight(120);
                    sw.WriteLine(linee[i]);

                }


            }

        }


        public static bool CancellazioneLogica(string cancellato, int ncampi)
        {
            string path = @"Zappa.csv";
            ncampi = Contacampi(path);
            bool canc = false;
            string[] linee = File.ReadAllLines(nfile);
            using (StreamWriter sw = new StreamWriter(nfile))
            {
                for (int i = 0; i < linee.Length; i++)
                {
                    string[] campi = linee[i].Split(';');

                    if (cancellato == campi[i])
                    {
                        campi[ncampi - 1] = "1";
                        linee[i] = String.Join(";", campi);
                        canc = true;

                        break;
                    }



                }
                for (int i = 0; i < linee.Length; i++)
                    sw.WriteLine(linee[i]);

            }

            return canc;

        }

        public static Campi ModProd(string prodottostringa, string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string sp)
        {

            Campi c;
            string[] div = prodottostringa.Split(sp[0]);
            c.comune = c1;
            c.provincia = c2;
            c.regione = c3;
            c.nome = c4;
            c.anno = int.Parse(c5);
            c.data = c6;
            c.identificatore = float.Parse(c7);
            c.longitudine = c8;
            c.latitudine = c9;
            return c;
        }

        public static void Modifica(string ricerca, string c1, string c2, string c3, string c4, string c5, string c6, string c7, string c8, string c9, string nfile, string sep, int l)
        {
            Campi c;
            string line;
            byte[] br;
            var file = new FileStream(nfile, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(file);
            BinaryWriter writer = new BinaryWriter(file);
            file.Seek(0, SeekOrigin.Begin);
            while (file.Position < file.Length)
            {
                br = reader.ReadBytes(l);
                line = Encoding.ASCII.GetString(br, 0, br.Length);
                string[] div = line.Split(sep[0]);
                if (div[0] == ricerca)
                {
                    c = ModProd(line, c1, c2, c3, c4, c5, c6, c7, c8, c9, sep);
                    line = Record(c, l);
                    file.Seek(-l, SeekOrigin.Current);
                    char[] linea = line.ToCharArray();
                    writer.Write(linea);
                }
            }
            reader.Close();
            writer.Close();
            file.Close();
        }
    }
}
