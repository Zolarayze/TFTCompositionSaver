using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TFT_CompositionSaver.DAL;
using TFT_CompositionSaver.Enums;
using TFT_CompositionSaver.Helper;
using TFT_CompositionSaver.Models;
using TFT_CompositionSaver.Models.API;
using TFT_CompositionSaver.Models.API.ChampionData;
using TFT_CompositionSaver.Models.API.ItemData;
using TFT_CompositionSaver.Properties;
using TFT_CompositionSaver.Views.Forms;

namespace TFT_CompositionSaver.Views.UserControls
{
    public partial class ChampRow : UserControl
    {
        private readonly FlowLayoutPanel flp;
        private bool champChosen;
        private PictureBox currentItem;

        private int chosenChampId;
        private string[] chosenTraits;
        private Dictionary<PictureBox, int> chosenItems;
        private bool swapChecked;
        private int starAmount = 1;

        private ActionMode actionMode;

        private const string plusTag = "PlusImg";
        private const string champTag = "ChampImg";
        private const string itemTag = "ItemImg";

        public ChampRow(FlowLayoutPanel flp)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            chosenItems = new Dictionary<PictureBox, int>();
            this.flp = flp;

            this.actionMode = ActionMode.Edit;

            this.pbxSwap.Image = Utility.SetAlpha((Bitmap)this.pbxSwap.Image, 128);
            this.pbxSwap.Parent = this.pbxChamp;
            this.pbxSwap.Location = new Point(this.pbxChamp.Width - this.pbxSwap.Width - 2, 0);

            this.btnDown.Visible = false;
            if (this.flp.Controls.Count <= 1)
            {
                this.btnUp.Visible = false;
                return;
            }

            int index = this.flp.Controls.Count - 2;
            ((ChampRow)flp.Controls[index]).SetButtonVisibility(index, index + 1);
        }

        public ChampRow(FlowLayoutPanel flp, ChampionSet champSet, int index, int lastIndex)
        {
            InitializeComponent();
            chosenItems = new Dictionary<PictureBox, int>();
            this.flp = flp;
            Item[] items = ApiData.Instance().GetItems();
            Champion[] champs = ApiData.Instance().GetChampions();
            Trait[] traits = ApiData.Instance().GetTraits();
            Champion champ = champs.FirstOrDefault(c => c.id == champSet.championId);
            Image champImg = champ?.image;
            this.pbxChamp.Image = champImg ?? Resources.Plus_white;
            this.pbxChamp.Tag = champImg != null ? champTag : plusTag;

            this.swapChecked = champSet.swapChecked;

            this.starAmount = champSet.stars;
            this.SetStarImage(this.starAmount);

            if (champ != null)
            {
                Image trait1 = traits.FirstOrDefault(t => champ.traits.FirstOrDefault() == t.name)?.image;
                Image trait3 = traits.FirstOrDefault(t => champ.traits.ElementAtOrDefault(1) == t.name)?.image;
                Image trait2 = traits.FirstOrDefault(t => champ.traits.LastOrDefault() == t.name)?.image;

                if (trait2 == trait3)
                {
                    trait3 = null;
                }

                this.pbxTrait1.Image = trait1 ?? Resources.missing;
                this.pbxTrait2.Image = trait2 ?? Resources.missing;
                this.pbxTrait3.Image = trait3;

                this.pbxSwap.Visible = true;
                if (!swapChecked)
                {
                    this.pbxSwap.Image = Utility.SetAlpha((Bitmap)this.pbxSwap.Image, 128);
                }
            }
            else
            {
                this.pbxTrait1.Visible = false;
                this.pbxTrait2.Visible = false;
                this.pbxTrait3.Visible = false;
                this.pbxSwap.Visible = false;
                this.pbxSwap.Image = Utility.SetAlpha((Bitmap)this.pbxSwap.Image, 128);
            }

            for (int i = 0; i < champSet.itemIds.Count; i++)
            {
                PictureBox pbx = (PictureBox)this.flpItemContainer.Controls.Find("pbxItem" + (i + 1), false).FirstOrDefault();
                if (pbx != null)
                {
                    Image itemImg = items.FirstOrDefault(item => champSet.itemIds[i] == item.id)?.image;

                    if (itemImg != null)
                    {
                        pbx.Image = itemImg;
                        pbx.Tag = itemTag;
                        chosenItems[pbx] = champSet.itemIds[i];
                    }
                    else
                    {
                        pbx.Image = Resources.Plus_white_small;
                        pbx.Tag = plusTag;
                    }
                }
            }

            if (!this.pbxChamp.Tag.ToString().Equals(plusTag))
            {
                champChosen = true;
                chosenChampId = champSet.championId;
            }

            this.btnUp.Visible = index != 0;
            this.btnDown.Visible = index != lastIndex;

            this.pbxSwap.Parent = this.pbxChamp;
            this.pbxSwap.Location = new Point(this.pbxChamp.Width - this.pbxSwap.Width - 2, 0);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ChampRow cr = (ChampRow)((Button)sender).Parent;

            int index = this.flp.Controls.GetChildIndex(cr);
            if (index > 0)
            {
                ((ChampRow)flp.Controls[index - 1]).SetButtonVisibility(index - 1, this.flp.Controls.Count - 3);
            }

            if (index < flp.Controls.Count - 2)
            {
                ((ChampRow)flp.Controls[index + 1]).SetButtonVisibility(index, this.flp.Controls.Count - 3);
            }


            flp.Controls.Remove(this);

            this.Dispose(true);
        }

        private void pbxChamp_Click(object sender, EventArgs e)
        {
            if (this.actionMode == ActionMode.Read)
            {
                return;
            }
            ChampPicker champPicker = new ChampPicker();
            champPicker.Confirmed += SetChamp;
            champPicker.ShowDialog();
        }

        private void pbxItem_Click(object sender, EventArgs e)
        {
            if (this.actionMode == ActionMode.Read)
            {
                return;
            }
            currentItem = (PictureBox)sender;

            MouseEventArgs mb = (MouseEventArgs)e;
            if (mb.Button == MouseButtons.Left)
            {
                ItemPicker itemPicker = new ItemPicker();
                itemPicker.Confirmed += SetItem;
                itemPicker.ShowDialog();
            }
            else if (mb.Button == MouseButtons.Right && (string)currentItem.Tag != plusTag)
            {
                currentItem.Image = Resources.Plus_white_small;
                currentItem.Tag = plusTag;

                if (chosenItems.TryGetValue(currentItem, out int val))
                {
                    chosenItems.Remove(currentItem);
                }
            }
        }

        private void SetChamp(object sender, EventArgs e)
        {
            Champion champ = (Champion)sender;
            this.pbxChamp.Image = champ.image;
            this.pbxChamp.Tag = champTag;
            champChosen = true;
            chosenChampId = champ.id;

            Trait[] traits = ApiData.Instance().GetTraits();
            Image trait1 = traits.FirstOrDefault(t => champ.traits.FirstOrDefault() == t.name)?.image;
            Image trait3 = traits.FirstOrDefault(t => champ.traits.ElementAtOrDefault(1) == t.name)?.image;
            Image trait2 = traits.FirstOrDefault(t => champ.traits.LastOrDefault() == t.name)?.image;

            if (trait2 == trait3)
            {
                trait3 = null;
            }
            this.pbxTrait1.Image = trait1 ?? Resources.missing;
            this.pbxTrait2.Image = trait2 ?? Resources.missing;
            this.pbxTrait3.Image = trait3;

            this.pbxSwap.Visible = true;

            chosenTraits = champ.traits;
        }

        private void SetItem(object sender, EventArgs e)
        {
            Item item = (Item)sender;
            currentItem.Image = item.image;
            currentItem.Tag = itemTag;
            if (this.chosenItems.TryGetValue(currentItem, out int val))
            {
                if (val == item.id)
                {
                    return;
                }

                chosenItems[currentItem] = item.id;
                return;
            }
            chosenItems.Add(currentItem, item.id);
        }

        public void ActionModeSwitched(object sender, EventArgs e)
        {
            ActionMode actionMode = (ActionMode)sender;
            this.SetActionMode(actionMode);
        }

        public void SetActionMode(ActionMode actionMode)
        {
            bool val = actionMode == ActionMode.Edit;
            this.actionMode = actionMode;

            this.btnDel.Visible = val;
            this.pnlArrows.Visible = val;
            this.pbxSwap.Visible = champChosen && (val || this.swapChecked);

            int xOffset = 50;
            this.pbxChamp.Location =
                new Point(val ? this.pbxChamp.Location.X + xOffset : this.pbxChamp.Location.X - xOffset,
                    this.pbxChamp.Location.Y);
            this.flpItemContainer.Location =
                new Point(
                    val
                        ? this.flpItemContainer.Location.X + xOffset
                        : this.flpItemContainer.Location.X - xOffset,
                    this.flpItemContainer.Location.Y);
            this.pbxTrait1.Location =
                new Point(val ? this.pbxTrait1.Location.X + xOffset : this.pbxTrait1.Location.X - xOffset,
                    this.pbxTrait1.Location.Y);
            this.pbxTrait2.Location =
                new Point(val ? this.pbxTrait2.Location.X + xOffset : this.pbxTrait2.Location.X - xOffset,
                    this.pbxTrait2.Location.Y);
            this.pbxTrait3.Location =
                new Point(val ? this.pbxTrait3.Location.X + xOffset : this.pbxTrait3.Location.X - xOffset,
                    this.pbxTrait3.Location.Y);
            this.pbxStar.Location =
                new Point(val ? this.pbxStar.Location.X + xOffset : this.pbxStar.Location.X - xOffset,
                    this.pbxStar.Location.Y);

            this.pbxChamp.Cursor = val ? Cursors.Hand : Cursors.Default;
            this.pbxSwap.Cursor = val ? Cursors.Hand : Cursors.Default;
            this.pbxStar.Cursor = val ? Cursors.Hand : Cursors.Default;

            if (this.pbxChamp.Tag.ToString().Equals(plusTag))
            {
                this.pbxChamp.Image = !val ? Resources.missing : Resources.Plus_white;
            }

            for (int i = 0; i < this.flpItemContainer.Controls.Count; i++)
            {
                PictureBox pbx = (PictureBox)this.flpItemContainer.Controls[i];

                if (!val && pbx.Tag.ToString().Equals(plusTag))
                {
                    pbx.Visible = false;
                }
                else if (val && !pbx.Visible)
                {
                    pbx.Visible = true;
                }

                pbx.Cursor = val ? Cursors.Hand : Cursors.Default;
            }
        }

        public int GetChampionId()
        {
            return this.chosenChampId;
        }

        public List<int> GetItemIds()
        {
            List<int> itemIds = this.chosenItems.Values.ToList();

            return itemIds;
        }

        public bool GetSwapChecked()
        {
            return this.swapChecked;
        }

        public int GetStarAmount()
        {
            return this.starAmount;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel parent = (FlowLayoutPanel)(this).Parent;
            int index = parent.Controls.GetChildIndex(this) - 1;
            parent.Controls.SetChildIndex(this, index);

            this.btnUp.Visible = index != 0;
            this.btnDown.Visible = index != parent.Controls.Count - 2;

            ((ChampRow)parent.Controls[index + 1]).SetButtonVisibility(index + 1, parent.Controls.Count - 2);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel parent = (FlowLayoutPanel)(this).Parent;
            int index = parent.Controls.GetChildIndex(this) + 1;
            parent.Controls.SetChildIndex(this, index);

            this.btnUp.Visible = index != 0;
            this.btnDown.Visible = index != parent.Controls.Count - 2;

            ((ChampRow)parent.Controls[index - 1]).SetButtonVisibility(index - 1, parent.Controls.Count - 2);
        }

        public void SetButtonVisibility(int index, int lastIndex)
        {
            this.btnUp.Visible = index != 0;
            this.btnDown.Visible = index != lastIndex;
        }

        private void pbxCross_Click(object sender, EventArgs e)
        {
            if (this.actionMode == ActionMode.Read)
            {
                return;
            }
            this.swapChecked = !this.swapChecked;
            PictureBox pbx = (PictureBox)sender;
            pbx.Image = Utility.SetAlpha((Bitmap)pbx.Image, swapChecked ? 510 : 128);
        }

        private void pbxStar_Click(object sender, EventArgs e)
        {
            if (this.actionMode == ActionMode.Read)
            {
                return;
            }
            this.starAmount = this.starAmount == 3 ? 1 : this.starAmount + 1;
            this.SetStarImage(this.starAmount);
        }

        private void SetStarImage(int amount)
        {
            switch (amount)
            {
                case 1:
                    this.pbxStar.Image = Resources._1star;
                    break;
                case 2:
                    this.pbxStar.Image = Resources._2star;
                    break;
                case 3:
                    this.pbxStar.Image = Resources._3star;
                    break;
                default:
                    this.pbxStar.Image = Resources._1star;
                    break;
            }
        }
    }
}
