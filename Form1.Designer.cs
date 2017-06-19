namespace JournalRefLookup
{
    partial class frmJournalLookup
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
            this.lblSngLookup = new System.Windows.Forms.Label();
            this.txtSngLookup = new System.Windows.Forms.TextBox();
            this.btnSngLookup = new System.Windows.Forms.Button();
            this.btnBulk = new System.Windows.Forms.Button();
            this.txtSngResult = new System.Windows.Forms.TextBox();
            this.btnSngClear = new System.Windows.Forms.Button();
            this.lblInsert = new System.Windows.Forms.Label();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnInsertClear = new System.Windows.Forms.Button();
            this.txtInsert = new System.Windows.Forms.TextBox();
            this.lblRemove = new System.Windows.Forms.Label();
            this.txtRemove = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnRemoveClear = new System.Windows.Forms.Button();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblSngLookup
            // 
            this.lblSngLookup.AutoSize = true;
            this.lblSngLookup.Location = new System.Drawing.Point(28, 48);
            this.lblSngLookup.Name = "lblSngLookup";
            this.lblSngLookup.Size = new System.Drawing.Size(76, 13);
            this.lblSngLookup.TabIndex = 0;
            this.lblSngLookup.Text = "PMID Lookup:";
            // 
            // txtSngLookup
            // 
            this.txtSngLookup.Location = new System.Drawing.Point(126, 48);
            this.txtSngLookup.Name = "txtSngLookup";
            this.txtSngLookup.Size = new System.Drawing.Size(170, 20);
            this.txtSngLookup.TabIndex = 1;
            this.txtSngLookup.TextChanged += new System.EventHandler(this.txtSngLookup_TextChanged);
            // 
            // btnSngLookup
            // 
            this.btnSngLookup.Enabled = false;
            this.btnSngLookup.Location = new System.Drawing.Point(349, 48);
            this.btnSngLookup.Name = "btnSngLookup";
            this.btnSngLookup.Size = new System.Drawing.Size(170, 23);
            this.btnSngLookup.TabIndex = 2;
            this.btnSngLookup.Text = "Lookup";
            this.btnSngLookup.UseVisualStyleBackColor = true;
            this.btnSngLookup.Click += new System.EventHandler(this.btnSngLookup_Click);
            // 
            // btnBulk
            // 
            this.btnBulk.Location = new System.Drawing.Point(349, 181);
            this.btnBulk.Name = "btnBulk";
            this.btnBulk.Size = new System.Drawing.Size(170, 57);
            this.btnBulk.TabIndex = 3;
            this.btnBulk.Text = "Bulk Lookup";
            this.btnBulk.UseVisualStyleBackColor = true;
            this.btnBulk.Click += new System.EventHandler(this.btnBulk_Click);
            // 
            // txtSngResult
            // 
            this.txtSngResult.Enabled = false;
            this.txtSngResult.Location = new System.Drawing.Point(126, 130);
            this.txtSngResult.Name = "txtSngResult";
            this.txtSngResult.Size = new System.Drawing.Size(607, 20);
            this.txtSngResult.TabIndex = 4;
            // 
            // btnSngClear
            // 
            this.btnSngClear.Location = new System.Drawing.Point(563, 48);
            this.btnSngClear.Name = "btnSngClear";
            this.btnSngClear.Size = new System.Drawing.Size(170, 23);
            this.btnSngClear.TabIndex = 5;
            this.btnSngClear.Text = "Clear";
            this.btnSngClear.UseVisualStyleBackColor = true;
            this.btnSngClear.Click += new System.EventHandler(this.btnSngClear_Click);
            // 
            // lblInsert
            // 
            this.lblInsert.AutoSize = true;
            this.lblInsert.Location = new System.Drawing.Point(28, 307);
            this.lblInsert.Name = "lblInsert";
            this.lblInsert.Size = new System.Drawing.Size(63, 13);
            this.lblInsert.TabIndex = 6;
            this.lblInsert.Text = "Insert PMID";
            // 
            // btnInsert
            // 
            this.btnInsert.Enabled = false;
            this.btnInsert.Location = new System.Drawing.Point(349, 303);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(170, 23);
            this.btnInsert.TabIndex = 7;
            this.btnInsert.Text = "Insert";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnInsertClear
            // 
            this.btnInsertClear.Location = new System.Drawing.Point(563, 303);
            this.btnInsertClear.Name = "btnInsertClear";
            this.btnInsertClear.Size = new System.Drawing.Size(170, 23);
            this.btnInsertClear.TabIndex = 8;
            this.btnInsertClear.Text = "Clear";
            this.btnInsertClear.UseVisualStyleBackColor = true;
            this.btnInsertClear.Click += new System.EventHandler(this.btnInsertClear_Click);
            // 
            // txtInsert
            // 
            this.txtInsert.Location = new System.Drawing.Point(126, 300);
            this.txtInsert.Name = "txtInsert";
            this.txtInsert.Size = new System.Drawing.Size(170, 20);
            this.txtInsert.TabIndex = 9;
            this.txtInsert.TextChanged += new System.EventHandler(this.txtInsert_TextChanged);
            // 
            // lblRemove
            // 
            this.lblRemove.AutoSize = true;
            this.lblRemove.Location = new System.Drawing.Point(28, 384);
            this.lblRemove.Name = "lblRemove";
            this.lblRemove.Size = new System.Drawing.Size(77, 13);
            this.lblRemove.TabIndex = 10;
            this.lblRemove.Text = "Remove PMID";
            // 
            // txtRemove
            // 
            this.txtRemove.Location = new System.Drawing.Point(126, 376);
            this.txtRemove.Name = "txtRemove";
            this.txtRemove.Size = new System.Drawing.Size(170, 20);
            this.txtRemove.TabIndex = 11;
            this.txtRemove.TextChanged += new System.EventHandler(this.txtRemove_TextChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Enabled = false;
            this.btnRemove.Location = new System.Drawing.Point(349, 372);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(170, 23);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnRemoveClear
            // 
            this.btnRemoveClear.Location = new System.Drawing.Point(563, 373);
            this.btnRemoveClear.Name = "btnRemoveClear";
            this.btnRemoveClear.Size = new System.Drawing.Size(170, 23);
            this.btnRemoveClear.TabIndex = 13;
            this.btnRemoveClear.Text = "Clear";
            this.btnRemoveClear.UseVisualStyleBackColor = true;
            this.btnRemoveClear.Click += new System.EventHandler(this.btnRemoveClear_Click);
            // 
            // txtTitle
            // 
            this.txtTitle.Enabled = false;
            this.txtTitle.Location = new System.Drawing.Point(126, 88);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(607, 20);
            this.txtTitle.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Journal Name";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Journal Info";
            // 
            // frmJournalLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 486);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnRemoveClear);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtRemove);
            this.Controls.Add(this.lblRemove);
            this.Controls.Add(this.txtInsert);
            this.Controls.Add(this.btnInsertClear);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.lblInsert);
            this.Controls.Add(this.btnSngClear);
            this.Controls.Add(this.txtSngResult);
            this.Controls.Add(this.btnBulk);
            this.Controls.Add(this.btnSngLookup);
            this.Controls.Add(this.txtSngLookup);
            this.Controls.Add(this.lblSngLookup);
            this.Name = "frmJournalLookup";
            this.Text = "Journal Reference Lookup";
            this.Load += new System.EventHandler(this.frmJournalLookup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSngLookup;
        private System.Windows.Forms.TextBox txtSngLookup;
        private System.Windows.Forms.Button btnSngLookup;
        private System.Windows.Forms.Button btnBulk;
        private System.Windows.Forms.TextBox txtSngResult;
        private System.Windows.Forms.Button btnSngClear;
        private System.Windows.Forms.Label lblInsert;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnInsertClear;
        private System.Windows.Forms.TextBox txtInsert;
        private System.Windows.Forms.Label lblRemove;
        private System.Windows.Forms.TextBox txtRemove;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnRemoveClear;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

