using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace MetroSim
{
    public partial class MainGUI : Form
    {

        private Model model;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_load(object sender, EventArgs e)
        {
            model = new Model(this);
            model.init();
            vyplnDropdown();
        }

        private void vyplnDropdown()
        {
            foreach(KeyValuePair<string, Stanice> k in model.getSeznamStanic())
            {
                cZacatek.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
                cKonec.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
            }
            cZacatek.SelectedIndex = 0;
            cKonec.SelectedIndex = 0;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Stanice zacatek = model.getSeznamStanic()[(string)((CustomCBItem)cZacatek.SelectedItem).Value];
            Stanice konec = model.getSeznamStanic()[(string)((CustomCBItem)cKonec.SelectedItem).Value];

            if (zacatek != null && konec != null)
            {
                model.pridejHlavnihoPasazera(zacatek, konec, 0);
            }
            else
            {
                Console.WriteLine("chyba");
            }
            Thread vypocetTh = new Thread(model.spocitej);
            vypocetTh.Start();
        }

        public void finished(int vysledek)
        {
            Console.WriteLine("konec " + vysledek);
        }
    }
}
