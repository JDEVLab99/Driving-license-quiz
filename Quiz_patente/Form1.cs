using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Windows.Forms;
using System.IO;

namespace Quiz_patente
{
    public partial class Form1 : Form
    {
        Quiz quiz = new Quiz();
        char[] risposte = new char[40];
        int i = 0;
        int timeLeft = 60;
        int minuti = 29;
        int errori = 0;

        public Form1()
        {
            InitializeComponent();
            textBox1.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            i = 0;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button6.Enabled = false;
            timer1.Enabled = false;
            label4.Enabled = false;
            quiz.carica();
        }

        //FUNZIONE TIMER 
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (minuti > 0)
            {
                label1.Text = minuti + "    :";
                if (timeLeft > 0)
                {
                    timeLeft = timeLeft - 1;
                    timeLabel.Text = timeLeft.ToString();
                }
                else
                {
                    timer1.Stop();
                    timeLeft = 60;
                    minuti--;
                    timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("HAI FINITO IL TEMPO!");
                timeLabel.Text = "TEMPO SCADUTO!";
            }
        }

        //VERO
        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = quiz.get_domanda(i).get_testo();
            risposte[i] = 'V';
            carica_immagine();
            progressBar1.Value = i;
            label4.Text = (i+1).ToString();
            if(i<40)
                i++;
        }

        //FALSO
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = quiz.get_domanda(i).get_testo();
            risposte[i] = 'F';
            carica_immagine();
            progressBar1.Value = i;
            label4.Text = (i+1).ToString();
            if(i<40)
                i++;
        }

        //AVANTI
        private void button4_Click(object sender, EventArgs e)
        {
            if(i<40)
                i++;
            progressBar1.Value = i;
            textBox1.Text = quiz.get_domanda(i).get_testo();
            label4.Text = (i+1).ToString();
            carica_immagine();
        }

        //INDIETRO
        private void button3_Click(object sender, EventArgs e)
        {
            if(i>=0)
                i--;
            progressBar1.Value = i;
            textBox1.Text = quiz.get_domanda(i).get_testo();
            label4.Text = (i+1).ToString();
            carica_immagine();
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        //INIZIA
        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = quiz.get_domanda(0).get_testo();
            label4.Enabled = true;
            label4.Text = (i+1).ToString();
            button5.Enabled = false;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button6.Enabled = true;
            timer1.Enabled = true;
            timer1.Start();
        }
        //FINE
        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            controlla_errori();
            MessageBox.Show("NUMERO ERRORI: " + errori);
            if (errori >= 5)
                MessageBox.Show("NON PASSATO");
            else
                MessageBox.Show("PASSATO");
            
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
        }

        private void controlla_errori()
        {
            int i = 0;
            StreamWriter file = new StreamWriter("Report.csv");
            while (i < 40)
            {
                if (quiz.get_domanda(i).get_risposta() == risposte[i])
                    file.WriteLine(quiz.get_domanda(i).get_testo() + ';' + quiz.get_domanda(i).get_risposta() + ';' + "GIUSTA;");
                else if (risposte[i] == ' ')
                {
                    file.WriteLine(quiz.get_domanda(i).get_testo() + ';' + quiz.get_domanda(i).get_risposta() + ';' + "NON DATA;");
                    errori++;
                }
                else
                {
                    file.WriteLine(quiz.get_domanda(i).get_testo() + ';' + quiz.get_domanda(i).get_risposta() + ';' + "SBAGLIATA;");
                    errori++;
                }
                i++;
            }
            file.Close();
        }

        private void carica_immagine()
        {
            if(quiz.get_domanda(i).get_url() != " ")
                pictureBox1.ImageLocation = quiz.get_domanda(i).get_url();
        }
    }
}
