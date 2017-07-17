namespace Mustava.DbManager
{
    partial class DbSyncForm
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
            this.grpDb1 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.cbeServer1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtUsername1 = new DevExpress.XtraEditors.TextEdit();
            this.txtPassword1 = new DevExpress.XtraEditors.TextEdit();
            this.cbeSchema1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.hlcConnect1 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.hlcConnect2 = new DevExpress.XtraEditors.HyperlinkLabelControl();
            this.cbeSchema2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPassword2 = new DevExpress.XtraEditors.TextEdit();
            this.txtUsername2 = new DevExpress.XtraEditors.TextEdit();
            this.cbeServer2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpDb1)).BeginInit();
            this.grpDb1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeServer1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeSchema1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeSchema2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeServer2.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDb1
            // 
            this.grpDb1.Controls.Add(this.hlcConnect1);
            this.grpDb1.Controls.Add(this.cbeSchema1);
            this.grpDb1.Controls.Add(this.txtPassword1);
            this.grpDb1.Controls.Add(this.txtUsername1);
            this.grpDb1.Controls.Add(this.cbeServer1);
            this.grpDb1.Controls.Add(this.labelControl4);
            this.grpDb1.Controls.Add(this.labelControl3);
            this.grpDb1.Controls.Add(this.labelControl2);
            this.grpDb1.Controls.Add(this.labelControl1);
            this.grpDb1.Location = new System.Drawing.Point(12, 12);
            this.grpDb1.Name = "grpDb1";
            this.grpDb1.Size = new System.Drawing.Size(268, 158);
            this.grpDb1.TabIndex = 0;
            this.grpDb1.Text = "DB #1";
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(11, 30);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Server:";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(11, 124);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(41, 13);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Schema:";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(11, 52);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(26, 13);
            this.labelControl3.TabIndex = 2;
            this.labelControl3.Text = "User:";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(11, 75);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(26, 13);
            this.labelControl4.TabIndex = 3;
            this.labelControl4.Text = "Pass:";
            // 
            // cbeServer1
            // 
            this.cbeServer1.Location = new System.Drawing.Point(64, 27);
            this.cbeServer1.Name = "cbeServer1";
            this.cbeServer1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeServer1.Size = new System.Drawing.Size(191, 20);
            this.cbeServer1.TabIndex = 4;
            // 
            // txtUsername1
            // 
            this.txtUsername1.Location = new System.Drawing.Point(64, 49);
            this.txtUsername1.Name = "txtUsername1";
            this.txtUsername1.Size = new System.Drawing.Size(191, 20);
            this.txtUsername1.TabIndex = 6;
            // 
            // txtPassword1
            // 
            this.txtPassword1.Location = new System.Drawing.Point(64, 72);
            this.txtPassword1.Name = "txtPassword1";
            this.txtPassword1.Size = new System.Drawing.Size(191, 20);
            this.txtPassword1.TabIndex = 7;
            // 
            // cbeSchema1
            // 
            this.cbeSchema1.Location = new System.Drawing.Point(64, 121);
            this.cbeSchema1.Name = "cbeSchema1";
            this.cbeSchema1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeSchema1.Size = new System.Drawing.Size(191, 20);
            this.cbeSchema1.TabIndex = 8;
            // 
            // hlcConnect1
            // 
            this.hlcConnect1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlcConnect1.Location = new System.Drawing.Point(215, 98);
            this.hlcConnect1.Name = "hlcConnect1";
            this.hlcConnect1.Size = new System.Drawing.Size(40, 13);
            this.hlcConnect1.TabIndex = 9;
            this.hlcConnect1.Text = "Connect";
            this.hlcConnect1.Click += new System.EventHandler(this.hlcConnect1_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.hlcConnect2);
            this.groupControl1.Controls.Add(this.cbeSchema2);
            this.groupControl1.Controls.Add(this.txtPassword2);
            this.groupControl1.Controls.Add(this.txtUsername2);
            this.groupControl1.Controls.Add(this.cbeServer2);
            this.groupControl1.Controls.Add(this.labelControl5);
            this.groupControl1.Controls.Add(this.labelControl6);
            this.groupControl1.Controls.Add(this.labelControl7);
            this.groupControl1.Controls.Add(this.labelControl8);
            this.groupControl1.Location = new System.Drawing.Point(286, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(268, 158);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "DB #1";
            // 
            // hlcConnect2
            // 
            this.hlcConnect2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.hlcConnect2.Location = new System.Drawing.Point(215, 98);
            this.hlcConnect2.Name = "hlcConnect2";
            this.hlcConnect2.Size = new System.Drawing.Size(40, 13);
            this.hlcConnect2.TabIndex = 9;
            this.hlcConnect2.Text = "Connect";
            // 
            // cbeSchema2
            // 
            this.cbeSchema2.Location = new System.Drawing.Point(64, 121);
            this.cbeSchema2.Name = "cbeSchema2";
            this.cbeSchema2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeSchema2.Size = new System.Drawing.Size(191, 20);
            this.cbeSchema2.TabIndex = 8;
            // 
            // txtPassword2
            // 
            this.txtPassword2.Location = new System.Drawing.Point(64, 72);
            this.txtPassword2.Name = "txtPassword2";
            this.txtPassword2.Size = new System.Drawing.Size(191, 20);
            this.txtPassword2.TabIndex = 7;
            // 
            // txtUsername2
            // 
            this.txtUsername2.Location = new System.Drawing.Point(64, 49);
            this.txtUsername2.Name = "txtUsername2";
            this.txtUsername2.Size = new System.Drawing.Size(191, 20);
            this.txtUsername2.TabIndex = 6;
            // 
            // cbeServer2
            // 
            this.cbeServer2.Location = new System.Drawing.Point(64, 27);
            this.cbeServer2.Name = "cbeServer2";
            this.cbeServer2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbeServer2.Size = new System.Drawing.Size(191, 20);
            this.cbeServer2.TabIndex = 4;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(11, 75);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(26, 13);
            this.labelControl5.TabIndex = 3;
            this.labelControl5.Text = "Pass:";
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(11, 52);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(26, 13);
            this.labelControl6.TabIndex = 2;
            this.labelControl6.Text = "User:";
            // 
            // labelControl7
            // 
            this.labelControl7.Location = new System.Drawing.Point(11, 124);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(41, 13);
            this.labelControl7.TabIndex = 1;
            this.labelControl7.Text = "Schema:";
            // 
            // labelControl8
            // 
            this.labelControl8.Location = new System.Drawing.Point(11, 30);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(36, 13);
            this.labelControl8.TabIndex = 0;
            this.labelControl8.Text = "Server:";
            // 
            // DbSyncForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 632);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.grpDb1);
            this.Name = "DbSyncForm";
            this.Text = "DB Sync";
            ((System.ComponentModel.ISupportInitialize)(this.grpDb1)).EndInit();
            this.grpDb1.ResumeLayout(false);
            this.grpDb1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeServer1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeSchema1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbeSchema2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbeServer2.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpDb1;
        private DevExpress.XtraEditors.ComboBoxEdit cbeServer1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtPassword1;
        private DevExpress.XtraEditors.TextEdit txtUsername1;
        private DevExpress.XtraEditors.ComboBoxEdit cbeSchema1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlcConnect1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.HyperlinkLabelControl hlcConnect2;
        private DevExpress.XtraEditors.ComboBoxEdit cbeSchema2;
        private DevExpress.XtraEditors.TextEdit txtPassword2;
        private DevExpress.XtraEditors.TextEdit txtUsername2;
        private DevExpress.XtraEditors.ComboBoxEdit cbeServer2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl8;
    }
}

