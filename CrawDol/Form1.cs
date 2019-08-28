using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawDol
{
    public partial class Form1 : Form
    {

        private string Dolar
        {
            get { return getDolar();}
        }

        private string Euro
        {
            get { return getEuro(); }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private string getDolar()
        {
            WebClient wCliente = new WebClient();
            wCliente.Headers.Add("User-Agent:Other");
            wCliente.Encoding = System.Text.Encoding.UTF8;

            var data = wCliente.DownloadString("https://economia.uol.com.br/");

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(data);

            var ret = document.DocumentNode.SelectNodes("//div[@class='hidden-xs']/h3[@class='tituloGrafico']/a[@class='subtituloGrafico subtituloGraficoValor']");
            var retorno = ret[0].InnerText;

            return retorno;
        }

        private string getEuro()
        {
            WebClient wCliente = new WebClient();
            wCliente.Headers.Add("User-Agent:Other");
            wCliente.Encoding = System.Text.Encoding.UTF8;

            var data = wCliente.DownloadString("https://economia.uol.com.br/");

            HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(data);

            var ret = document.DocumentNode.SelectNodes("//div[@class='hidden-xs']/h3[@class='tituloGrafico']/a[@class='subtituloGrafico subtituloGraficoValor']");
            var retorno = ret[1].InnerText;

            return retorno;
        }

        private decimal Conversao()
        {

            var input = Convert.ToDecimal(txtInput.Text.Replace("R$", ""), new CultureInfo("pt-BR"));

            if (cbEscolha.Text == "Euro")
            {
                var moeda = Convert.ToDecimal(this.Euro.Replace("R$", ""), new CultureInfo("pt-BR"));

                return moeda* input;
            }

            else
            {
                var moeda = Convert.ToDecimal(this.Dolar.Replace("R$", ""), new CultureInfo("pt-BR"));

                return moeda * input;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string objFinal = Conversao().ToString();
            txtOutput.Text = objFinal;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbEscolha.Items.Add("Dolar");
            cbEscolha.Items.Add("Euro");
            txtDolar.Text = this.Dolar;
            txtEuro.Text = this.Euro;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
