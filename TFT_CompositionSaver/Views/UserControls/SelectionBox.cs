using System;
using System.Drawing;
using System.Windows.Forms;
using TFT_CompositionSaver.Models.API.ChampionData;
using TFT_CompositionSaver.Models.API.ItemData;

namespace TFT_CompositionSaver.Views.UserControls
{
    public partial class SelectionBox : UserControl
    {
        private bool isSelected;
        private Champion champ;
        private Item item;
        public event EventHandler Clicked;
        public event EventHandler DoubleClicked;

        public SelectionBox(Champion champ)
        {
            InitializeComponent();
            this.champ = champ;
            this.pbxImage.Image = champ.image;
        }

        public SelectionBox(Item item)
        {
            InitializeComponent();
            this.item = item;
            this.pbxImage.Image = item.image;
        }

        private void pbxImage_Click(object sender, EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }

        private void pbxImage_DoubleClick(object sender, EventArgs e)
        {
            DoubleClicked?.Invoke(this, e);
        }

        public void SetPanelColor(Color color)
        {
            this.pnlBack.BackColor = color;
        }

        public Champion GetChampion()
        {
            return this.champ;
        }

        public Item GetItem()
        {
            return this.item;
        }
    }
}
