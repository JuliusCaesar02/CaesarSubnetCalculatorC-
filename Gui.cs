using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
namespace Analisi_IP_SM_C
{
	public partial class Gui : Form
	{
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Gui());
		}
		public Gui()
		{
			InitializeComponent();
			button1.Click += new EventHandler(this.buttonClicked);
		}

		void buttonClicked(object sender, EventArgs e)
		{
			Sub sub = new Sub();
			String auxString = null;
			String ip = textBox1.Text;
			sub.setIp(ip);
			if (sub.decon(sub.setIp(ip))[0] == -1)
			{
				textBox2.Text = ("È stata riscontrata un'eccezione! Verifica il formato dell'IP" + "\r\n");
				return;
			}
			else if (sub.decon(sub.setIp(ip))[0] == -2)
			{
				textBox2.Text = ("Errore! L'IP inserito non esiste" + "\n");
				return;
			}
			sub.setClasse();
			String sm = textBox3.Text;
			sub.setSm(sm);
			if (sub.decon(sub.setIp(sm))[0] == -1)
			{
				textBox2.Text = ("È stata riscontrata un'eccezione! Verifica il formato della SM" + "\r\n");
				return;
			}
			else if (sub.decon(sub.setIp(sm))[0] == -2)
			{
				textBox2.Text = ("Errore! La SM inserita non esiste" + "\r\n");
				return;
			}
			sub.setBinaryBit(sub.decon(sub.setIp(ip)));
			textBox2.Text = ("Indirizzo IP binario: " + sub.getBinaryBit(sub.decon((sub.setIp(ip)))) + "\r\n");

			sub.setBinaryBit(sub.decon((sub.setIp(sm))));
			textBox2.AppendText("Indirizzo SM binario: " + sub.getBinaryBit(sub.decon((sub.setIp(sm)))) + "\r\n");
			sub.decon(sub.setIp(ip));
			textBox2.AppendText("1- La classe è " + sub.setClasse() + "\r\n");
			if (sub.setClasse() == 'D' || sub.setClasse() == 'E')
			{
				textBox2.AppendText("Non è possibile eseguire ulteriori operazioni" + "\r\n");
				return;
			}
			if (sub.setClasse() == 'X')
			{
				textBox2.AppendText("L'indirizzo IP inserito non esiste" + "\r\n");
				return;
			}
			if (sub.setByteCrit() != 0)
			{
				textBox2.AppendText("2- Il byte critico è il " + (sub.setByteCrit()) + " (" + sub.decon(sm)[sub.setByteCrit() - 1] + ")" + "\r\n");
			}
			else textBox2.AppendText("2- Il byte critico non è presente" + "\r\n");

			textBox2.AppendText("3- Il CIDR è /" + sub.setCidr() + "\r\n");
			             
			textBox2.AppendText("4- I bit di rete sono " + sub.setNetBit() + "\r\n");

			sub.setSubBit();
			if (sub.setSubBit() < 0)
			{
				textBox2.AppendText("5- ERRORE! I bit di sotto rete sono negativi! " + sub.setSubBit() + "\r\n");
				return;
			}
			else textBox2.AppendText("5- I bit di sotto rete sono " + sub.setSubBit() + "\r\n");

			sub.setHostBit();
			if (sub.setHostBit() < 0)
			{
				textBox2.AppendText("6- ERRORE! I bit degli host sono negativi! " + sub.setHostBit() + "\r\n");
				return;                                                                   
			}
			else textBox2.AppendText("6- I bit degli host sono " + sub.setHostBit() + "\r\n");

			textBox2.AppendText("7- Ci possono essere " + sub.setHostESubPerNet(sub.setHostBit(), 2) + " host in ogni subnet (" + (sub.setHostESubPerNet(sub.setHostBit(), 0)) + ")" + "\r\n");

			textBox2.AppendText("8- Ci possono essere " + sub.setHostESubPerNet(sub.setSubBit(), 0) + " subnet nella rete" + "\r\n");

			textBox2.AppendText("9- Il magic number è " + sub.setMn() + "\r\n");

			sub.setNetIp();
			auxString = string.Join(".", sub.setNetIp());
			textBox2.AppendText("10- Indirizzo di rete " +auxString + "\r\n");

			sub.setBroadIp();
			auxString = string.Join(".", sub.setBroadIp());
			textBox2.AppendText("11- Indirizzo di broadcast " + auxString + "\r\n");

			textBox2.AppendText("12- Primo indirizzo host " + sub.setRange(sub.setNetIp(), 1) + "\r\n");
			textBox2.AppendText("      Ultimo indirizzo host " + sub.setRange(sub.setBroadIp(), -1) + "\r\n");

			textBox2.AppendText("13- L'IP fa parte della subnet " + sub.setSubNet() + "\r\n");

			textBox2.AppendText(sub.setBitMap());
		}
	}
}
