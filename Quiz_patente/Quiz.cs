using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Quiz_patente
{
    
    class Quiz
    {
        Domanda[] domande = new Domanda[41];
        const int n_domande = 40;
        List<int> indici = new List<int>();
        string filenamecsv = "Domande.csv";
        string filenamebin = "Domande.bin";
        string indicefile = "Indici.csv";
        int max = 0;
        int n_stringhe = 0;

        public void leggi_csv()
        {
            string line;
            int i = 0;
            string[] temp;
            
            StreamWriter file1 = new StreamWriter(indicefile);
            StreamReader file = new StreamReader(filenamecsv);
            while ((line = file.ReadLine()) != null)
            {
                if(line.Length > max)
                 max = line.Length;
                 
                temp = line.Split(';');
                if(i%10==0)
                    file1.WriteLine(temp[0] + ';' + i.ToString() + ';');
                i++;
            }
            n_stringhe = i;
            file1.Close();
            file.Close();
        }

        public void scrivi_bin(int size,char[] s)
        {
            FileStream bin_out = new FileStream(filenamebin, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter bin = new BinaryWriter(bin_out);
            StreamReader file = new StreamReader(filenamecsv);
            int i = 0;
            
            while (i<n_stringhe)
            {
                s = file.ReadLine().ToCharArray(); //leggo riga per riga dal file CSV e lo converto in formato caratteri
                bin.BaseStream.Seek(i * size, SeekOrigin.Begin);//mi posiziono e ogni tot byte scrivo
                bin.Write(s);
                i++;//incremento posizione
            }
            bin.Close();
            bin_out.Close();
            file.Close();
        }

        public void leggi_bin(int size)
        {
            FileStream f = new FileStream(filenamebin, FileMode.Open, FileAccess.Read);
            BinaryReader fin = new BinaryReader(f);
            
            int i = 0;
            int j = 0;
            int p = 0;

            while (j < n_domande)
            {
                char[] s = new char[size];
                p = indici.ElementAt(j);

                fin.BaseStream.Seek(p * size, SeekOrigin.Begin);

                i = 0;
                while (i < size)
                {
                    s[i] = fin.ReadChar();
                    i++;
                }
                string temp = new string(s);
                string[] str =  temp.Split(';');

                domande[j] = new Domanda(str[0],str[1],str[2],str[3]);
                j++;
            }
            fin.Close();
            f.Close();
        }

        public void carica_indici()
        {
            string line;
            string[] temp;
            int range = 0;
            int n1 = 0;
            int n2 = 0;
            Random rand = new Random();
            StreamReader file = new StreamReader(indicefile);
            while ((line = file.ReadLine()) != null)
            {
               temp = line.Split(';');
               Int32.TryParse(temp[1], out range);
               do
               {
                 n1 = rand.Next(range, range + 10);
                 n2 = rand.Next(range, range + 10);
               } while (n1 == n2);

               indici.Add(n1);
               indici.Add(n2);
            }
            indici.Sort();
        }
         
        public void carica()
        {
            leggi_csv();
            char[] stringa = new char[max];
            scrivi_bin(max,stringa);//scrivo il file binario 
            carica_indici();
            leggi_bin(max); //lettura domande da file e set dei valori per ogni domanda con split
        }

        public Domanda get_domanda(int pos)
        {
            return domande[pos];
        }
    }
}
