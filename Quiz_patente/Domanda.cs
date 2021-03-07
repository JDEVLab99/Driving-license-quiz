using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Quiz_patente
{
    class Domanda
    {
        private string testo;
        private string codice;
        private string url;
        private char risposta;

        public Domanda(string c, string r, string t, string u)
        {
            set_codice(c);
            set_risposta(r);
            set_testo(t);
            if (u != " ")
                set_url(u);
            else
                MessageBox.Show("vuota");
        }
        public void set_testo(string t)
        {
            if (t != " " && t.Length>0)
                testo = t;
        }

        public void set_codice(string c)
        {
            if (c != " " && c.Length > 0)
                codice = c;
        }

        public void set_risposta(string r)
        {
            r.ToCharArray();
            char ri = r[0];
            if (ri == 'V' || ri=='F')
               risposta=ri;
        }

        public void set_url(string u)
        {
            if (u != " " && u.Length > 0)
                url = u;
        }

        public string get_testo()
        {
            return testo;
        }

        public string get_codice()
        {
            return codice;
        }

        public char get_risposta()
        {
            return risposta;
        }

        public string get_url()
        {
            return url;
        }

    }
}
