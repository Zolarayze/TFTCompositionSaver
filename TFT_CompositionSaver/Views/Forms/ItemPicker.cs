using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFT_CompositionSaver.DAL;
using TFT_CompositionSaver.Models.API.ItemData;
using TFT_CompositionSaver.Views.UserControls;

namespace TFT_CompositionSaver.Views.Forms
{
    public partial class ItemPicker : Form
    {
        private Item[] items;
        private SelectionBox selectedItem;

        public event EventHandler Confirmed;

        public ItemPicker()
        {
            InitializeComponent();

            items = ApiData.Instance().GetItems();

            for (int i = 0; i < this.tclItems.TabCount; i++)
            {
                this.TabpageSelected(this.tclItems.TabPages[i]);
            }
        }

        private void TabpageSelected(TabPage tabPage)
        {
            string selectedTab = tabPage.Text;

            Item[] chosenItems = {};

            switch (selectedTab)
            {
                case "Base":
                    chosenItems = items.Where(x => x.from.Length == 0).ToArray();
                    break;
                case "Combined":
                    chosenItems = items.Where(x => x.from.Length > 0).ToArray();
                    break;
            }
            
            FlowLayoutPanel selectedFLP = (FlowLayoutPanel)tabPage.Controls[0];

            for (int i = 0; i < chosenItems.Length; i++)
            {
                SelectionBox sBox = new SelectionBox(chosenItems[i]);

                sBox.Clicked += selectedFlp_Click;
                sBox.DoubleClicked += selectedFlp_DoubleClick;
                selectedFLP.Controls.Add(sBox);
            }
        }

        private void selectedFlp_DoubleClick(object sender, EventArgs e)
        {
            selectedFlp_Click(sender, e);
            btnConfirm_Click(sender, e);
        }

        private void selectedFlp_Click(object sender, EventArgs e)
        {
            selectedItem?.SetPanelColor(Color.Transparent);
            selectedItem = (SelectionBox)sender;
            selectedItem.SetPanelColor(Color.SteelBlue);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                return;
            }
            this.Confirmed?.Invoke(selectedItem.GetItem(), e);
            this.Close();
        }
    }
}
