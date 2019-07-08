using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFT_CompositionSaver.DAL;
using TFT_CompositionSaver.Models.API.ChampionData;
using TFT_CompositionSaver.Views.UserControls;

namespace TFT_CompositionSaver.Views.Forms
{
    public partial class ChampPicker : Form
    {
        private Champion[] champions;
        private SelectionBox selectedChampion;

        public event EventHandler Confirmed;

        public ChampPicker()
        {
            InitializeComponent();
            champions = ApiData.Instance().GetChampions();

            for (int i = 0; i < this.tclClasses.TabCount; i++)
            {
                this.TabpageSelected(this.tclClasses.TabPages[i]);
            }

            for (int i = 0; i < this.tclOrigins.TabCount; i++)
            {
                this.TabpageSelected(this.tclOrigins.TabPages[i]);
            }
        }

        private void TabpageSelected(TabPage tabPage)
        {
            string selectedTab = tabPage.Text;
            Champion[] chosenChamps = champions.Where(x => x.traits.Contains(selectedTab)).ToArray();
            FlowLayoutPanel selectedFLP = (FlowLayoutPanel)tabPage.Controls[0];

            for (int i = 0; i < chosenChamps.Length; i++)
            {
                SelectionBox sBox = new SelectionBox(chosenChamps[i]);
                
                sBox.Clicked += selectedFlp_Click;
                sBox.DoubleClicked += selectedFlp_DoubleClick;
                selectedFLP.Controls.Add(sBox);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selectedChampion == null)
            {
                return;
            }
            this.Confirmed?.Invoke(selectedChampion.GetChampion(), e);
            this.Close();
        }

        private void selectedFlp_DoubleClick(object sender, EventArgs e)
        {
            selectedFlp_Click(sender, e);
            btnConfirm_Click(sender, e);
        }

        private void selectedFlp_Click(object sender, EventArgs e)
        {
            selectedChampion?.SetPanelColor(Color.Transparent);
            selectedChampion = (SelectionBox)sender;
            selectedChampion.SetPanelColor(Color.SteelBlue);
        }
    }
}
