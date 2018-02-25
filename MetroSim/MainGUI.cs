using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            model = new Model();
            model.init();
            vyplnDropdown();
        }

        private void vyplnDropdown()
        {
            foreach(KeyValuePair<string, Stanice> k in model.getSeznamStanic())
            {
                cZacatek.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
                cZacatek.Items.Add(new CustomCBItem(k.Value.jmeno + " (" + k.Value.pismeno + ")", k.Key));
            }
            cZacatek.SelectedIndex = 0;
            cKonec.SelectedIndex = 0;
        }

        private void bStart_Click(object sender, EventArgs e)
        {
            Stanice zacatek;
            Stanice konec;
            model.getSeznamStanic().TryGetValue((string) cZacatek.SelectedValue, out zacatek);
            model.getSeznamStanic().TryGetValue((string)cZacatek.SelectedValue, out konec);

            if (zacatek != null && konec != null)
            {
                model.pridejHlavnihoPasazera(zacatek, konec, 0);
            }
            else
            {
                Console.WriteLine("chyba");
            }
            
        }
    }
}
