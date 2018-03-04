using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace MetroSim
{
    public partial class MainGUI : Form
    {

        private Model model;
        private Nastaveni nastaveni;

        public MainGUI()
        {
            InitializeComponent();
        }

        private void MainGUI_load(object sender, EventArgs e)
        {
            nastaveni = new Nastaveni();
            model = new Model(this);
            model.init();
            vyplnDropdownyANastaveni();
        }

        private void vyplnDropdownyANastaveni()
        {
            foreach(KeyValuePair<string, Stanice> k in model.getSeznamStanic().stanice)
            {
                cZacatek.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
                cKonec.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
            }
            cZacatek.SelectedIndex = 0;
            cKonec.SelectedIndex = 0;

            foreach(KeyValuePair<string, string> k in model.getSeznamStanic().pismenaLinek)
            {
                cLinky.Items.Add(k.Key);
                NastaveniLinky nl = new NastaveniLinky(k.Key, (int) nPocetSouprav.Value, (float) nRychlost.Value, (int) nKapacita.Value);
                nastaveni.pridejNastaveniLinky(nl);
            }

            cLinky.SelectedIndex = 0;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            nastaveni.pocatecniStanice = model.getSeznamStanic().stanice[(string)((CustomCBItem)cZacatek.SelectedItem).Value];
            nastaveni.konecnaStanice = model.getSeznamStanic().stanice[(string)((CustomCBItem)cKonec.SelectedItem).Value];

            /*nahradit model.nactiNastaveni
            if (zacatek != null && konec != null)
            {
                model.pridejHlavnihoPasazera(zacatek, konec, 0);
            }
            else
            {
                Console.WriteLine("chyba");
            }
            */
            model.spawniSoupravy();
            Thread vypocetTh = new Thread(model.spocitej);
            vypocetTh.Start();
            lVysledek.Visible = false;
            pLoading.Visible = true;
        }

        delegate void BoolArgReturningVoidDelegate(bool visible);

        private void setLoadingVisibility(bool visible) //hack pro ruzna vlakna
        {
            if (pLoading.InvokeRequired)
            {
                BoolArgReturningVoidDelegate d = new BoolArgReturningVoidDelegate(setLoadingVisibility);
                Invoke(d, new object[] { visible });
            }
            else
            {
                pLoading.Visible = visible;
            }
        }

        delegate void StringArgReturningVoidDelegate(string vysledek);

        private void showVysledek(string vysledek) //hack pro ruzna vlakna
        {
            if (lVysledek.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(showVysledek);
                Invoke(d, new object[] { vysledek });
            }
            else
            {
                lVysledek.Visible = true;
                lVysledek.Text = vysledek;
            }
        }

        public void finished(int vysledek)
        {
            Console.WriteLine("konec " + vysledek);
            setLoadingVisibility(false);
            showVysledek(vysledek.ToString());
        }

        private void cLinky_SelectedIndexChanged(object sender, EventArgs e)
        {
            //nacte ulozene nastaveni linky
            NastaveniLinky nl = nastaveni.nastaveniLinek[(string) cLinky.SelectedItem];
            nKapacita.Value = nl.kapacitaSouprav;
            nPocetSouprav.Value = nl.pocetSouprav;
            nRychlost.Value = (decimal) nl.rychlostSouprav;
        }

        private void nastaveni_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown nd = (NumericUpDown)sender;
            NastaveniLinky nl = nastaveni.nastaveniLinek[(string) cLinky.SelectedItem];
            switch (nd.Name)
            {
                case "nRychlost":
                    nl.rychlostSouprav = (float) nd.Value;
                    break;
                case "nPocetSouprav":
                    nl.pocetSouprav = (int) nd.Value;
                    break;
                case "nKapacita":
                    nl.kapacitaSouprav = (int)nd.Value;
                    break;
            }
        }
    }
}
