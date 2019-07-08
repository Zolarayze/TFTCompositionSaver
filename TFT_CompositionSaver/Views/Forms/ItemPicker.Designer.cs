namespace TFT_CompositionSaver.Views.Forms
{
    partial class ItemPicker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemPicker));
            this.btnConfirm = new System.Windows.Forms.Button();
            this.tclItems = new System.Windows.Forms.TabControl();
            this.Base = new System.Windows.Forms.TabPage();
            this.flpBase = new System.Windows.Forms.FlowLayoutPanel();
            this.Combined = new System.Windows.Forms.TabPage();
            this.flpCombined = new System.Windows.Forms.FlowLayoutPanel();
            this.tclItems.SuspendLayout();
            this.Base.SuspendLayout();
            this.Combined.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfirm
            // 
            this.btnConfirm.BackColor = System.Drawing.SystemColors.Control;
            this.btnConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.ForeColor = System.Drawing.Color.Black;
            this.btnConfirm.Location = new System.Drawing.Point(736, 441);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(114, 36);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = false;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // tclItems
            // 
            this.tclItems.Controls.Add(this.Base);
            this.tclItems.Controls.Add(this.Combined);
            this.tclItems.Location = new System.Drawing.Point(11, 9);
            this.tclItems.Name = "tclItems";
            this.tclItems.SelectedIndex = 0;
            this.tclItems.Size = new System.Drawing.Size(840, 426);
            this.tclItems.TabIndex = 2;
            // 
            // Base
            // 
            this.Base.BackColor = System.Drawing.Color.White;
            this.Base.Controls.Add(this.flpBase);
            this.Base.Location = new System.Drawing.Point(4, 22);
            this.Base.Name = "Base";
            this.Base.Padding = new System.Windows.Forms.Padding(3);
            this.Base.Size = new System.Drawing.Size(832, 400);
            this.Base.TabIndex = 0;
            this.Base.Text = "Base";
            // 
            // flpBase
            // 
            this.flpBase.AutoScroll = true;
            this.flpBase.BackColor = System.Drawing.Color.White;
            this.flpBase.ForeColor = System.Drawing.Color.Black;
            this.flpBase.Location = new System.Drawing.Point(6, 6);
            this.flpBase.Name = "flpBase";
            this.flpBase.Size = new System.Drawing.Size(820, 388);
            this.flpBase.TabIndex = 2;
            // 
            // Combined
            // 
            this.Combined.BackColor = System.Drawing.Color.White;
            this.Combined.Controls.Add(this.flpCombined);
            this.Combined.Location = new System.Drawing.Point(4, 22);
            this.Combined.Name = "Combined";
            this.Combined.Padding = new System.Windows.Forms.Padding(3);
            this.Combined.Size = new System.Drawing.Size(832, 400);
            this.Combined.TabIndex = 1;
            this.Combined.Text = "Combined";
            // 
            // flpCombined
            // 
            this.flpCombined.AutoScroll = true;
            this.flpCombined.BackColor = System.Drawing.Color.White;
            this.flpCombined.ForeColor = System.Drawing.Color.Black;
            this.flpCombined.Location = new System.Drawing.Point(6, 6);
            this.flpCombined.Name = "flpCombined";
            this.flpCombined.Size = new System.Drawing.Size(820, 388);
            this.flpCombined.TabIndex = 3;
            // 
            // ItemPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(15)))), ((int)(((byte)(23)))));
            this.ClientSize = new System.Drawing.Size(863, 487);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.tclItems);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ItemPicker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ItemPicker";
            this.tclItems.ResumeLayout(false);
            this.Base.ResumeLayout(false);
            this.Combined.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.TabControl tclItems;
        private System.Windows.Forms.TabPage Base;
        private System.Windows.Forms.TabPage Combined;
        private System.Windows.Forms.FlowLayoutPanel flpBase;
        private System.Windows.Forms.FlowLayoutPanel flpCombined;
    }
}