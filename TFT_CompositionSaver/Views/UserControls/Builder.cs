using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using TFT_CompositionSaver.Controllers;
using TFT_CompositionSaver.Controllers.Interfaces;
using TFT_CompositionSaver.Enums;
using TFT_CompositionSaver.Helper;
using Help = TFT_CompositionSaver.Views.Forms.Help;

namespace TFT_CompositionSaver.Views.UserControls
{
    public partial class Builder : UserControl, IBuilderView
    {
        private BuilderController controller;
        private int count;

        public Builder()
        {
            InitializeComponent();
            this.lblName.Text = "";
            this.flpContainer.AllowDrop = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            this.count++;
            string compName = "New composition #" + this.count;
            while (this.lbxComps.Items.Contains(compName))
            {
                count++;
                compName = "New composition #" + count;
            }

            this.lbxComps.Items.Add(compName);
            this.lbxComps.SetSelected(lbxComps.Items.Count-1,true);
            this.SetEnabledAll(true);
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            DialogResult result =MessageBox.Show("Do you really want to remove composition \"" + this.lbxComps.SelectedItem + "\" ?",
                    "Remove composition", MessageBoxButtons.YesNo);

            if (result != DialogResult.Yes)
            {
                return;
            }

            int index = this.lbxComps.SelectedIndex;

            this.controller.RemoveComposition(index);
            this.lbxComps.Items.Remove(this.lbxComps.SelectedItem);
            if (this.lbxComps.Items.Count == 0)
            {
                this.tbxCompName.Text = string.Empty;
                this.lblName.Text = string.Empty;
                this.SetEnabledAll(false);
                this.btnSave.Visible = false;
                return;
            }

            if (index >= this.lbxComps.Items.Count)
            {
                this.lbxComps.SetSelected(lbxComps.Items.Count - 1, true);
            }
            else
            {
                this.lbxComps.SetSelected(index, true);
            }
            
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            this.lblSave.Visible = true;
            this.controller.SetModelData();
            this.lblSave.Text = "Successfully saved!";
            this.WaitAndRemoveSaveText();
        }

        private async void WaitAndRemoveSaveText()
        {
            await Task.Delay(3000);
            this.lblSave.Visible = false;
            this.lblSave.Text = "Saving...";
        }

        private void tbxCompName_KeyUp(object sender, KeyEventArgs e)
        {
            this.lbxComps.Items[this.lbxComps.SelectedIndex] = this.tbxCompName.Text;
            this.lblName.Text = this.lbxComps.Text;
            this.controller.SetNewName(this.lbxComps.SelectedIndex, this.tbxCompName.Text);
        }

        private void lbxComps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbxComps.SelectedIndex == -1 || (string)this.lbxComps.Items[this.lbxComps.SelectedIndex] == this.tbxCompName.Text)
            {
                return;
            }

            this.tbxCompName.Text = this.lbxComps.Text;
            this.lblName.Text = this.lbxComps.Text;
            this.SetEnabledAll(true);
            this.btnSave.Visible = this.controller.GetCurrentActionMode() == ActionMode.Edit;
            this.controller.AddChampionSets(this.lbxComps.SelectedIndex, this.lbxComps.Text);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.SuspendDrawing();
            
            this.SetVisibleReadEdit(ActionMode.Edit);
            this.controller.ShowReadEdit(ActionMode.Edit);

            this.ResumeDrawing(true);
            this.ResumeLayout(true);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.SuspendDrawing();

            this.SetVisibleReadEdit(ActionMode.Read);
            this.controller.ShowReadEdit(ActionMode.Read);

            this.ResumeDrawing(true);
            this.ResumeLayout(true);
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Help help = new Help();
            help.ShowDialog();
        }

        public void SetController(BuilderController controller)
        {
            this.controller = controller;
        }

        private void SetEnabledAll(bool val)
        {
            this.btnRemove.Enabled = val;
            this.tbxCompName.Enabled = val;
            this.btnSave.Enabled = val;
        }

        public void SetVisibleReadEdit(ActionMode actionMode)
        {
            // val = true = edit
            // val = false = read
            bool val = actionMode == ActionMode.Edit;

            this.btnRead.Visible = val;
            this.btnEdit.Visible = !val;
            this.btnAdd.Visible = val;
            this.btnRemove.Visible = val;
            this.btnSave.Visible = this.lbxComps.SelectedItems.Count != 0 && val;
            
            this.tbxCompName.Visible = val;
            this.lblName.Visible = !val;
            if (val)
            {
                this.lbxComps.Height = this.Height - 40;
            }
            else
            {
                this.lbxComps.Height = this.Height - 4;
            }
        }

        public ListBox CompositionList
        {
            get { return this.lbxComps; }
            set {
                this.lbxComps.Items.Clear();
                this.lbxComps.Items.AddRange(value.Items);
                count = this.lbxComps.Items.Count;
            }
        }

        public FlowLayoutPanel FlpTemplate
        {
            get { return this.flpContainer; }
        }

        
    }
}
