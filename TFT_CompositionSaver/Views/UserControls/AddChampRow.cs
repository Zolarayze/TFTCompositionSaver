using System;
using System.Windows.Forms;
using TFT_CompositionSaver.Enums;

namespace TFT_CompositionSaver.Views.UserControls
{
    public partial class AddChampRow : UserControl
    {
        public event EventHandler AddedChampRow;

        public AddChampRow()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.AddedChampRow?.Invoke(this, EventArgs.Empty);
        }

        public void ActionModeSwitched(object sender, EventArgs e)
        {
            ActionMode actionMode = (ActionMode)sender;
            bool val = actionMode == ActionMode.Edit;
            this.Visible = val;
        }
    }
}
