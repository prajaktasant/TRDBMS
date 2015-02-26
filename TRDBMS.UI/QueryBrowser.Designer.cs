namespace TRDBMS.UI
{
    partial class QueryBrowser
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
            this.button1 = new System.Windows.Forms.Button();
            this.QueryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.displayGridView = new System.Windows.Forms.DataGridView();
            this.lblErrorQuery = new System.Windows.Forms.Label();
            this.lblErrorText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.displayGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Location = new System.Drawing.Point(909, 94);

            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 73);
            this.button1.TabIndex = 0;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.Location = new System.Drawing.Point(28, 59);
            this.QueryTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.QueryTextBox.Multiline = true;
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(630, 114);
            this.QueryTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Query";
            // 
            // displayGridView
            // 
            this.displayGridView.AllowUserToAddRows = false;
            this.displayGridView.AllowUserToDeleteRows = false;
            this.displayGridView.AllowUserToOrderColumns = true;
            this.displayGridView.BackgroundColor = System.Drawing.SystemColors.ScrollBar;
            this.displayGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.displayGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayGridView.Location = new System.Drawing.Point(37, 316);
            this.displayGridView.Name = "displayGridView";
            this.displayGridView.RowTemplate.Height = 24;
            this.displayGridView.Size = new System.Drawing.Size(1002, 381);
            this.displayGridView.TabIndex = 3;
            // 
            // lblErrorQuery
            // 
            this.lblErrorQuery.AutoSize = true;
            this.lblErrorQuery.Location = new System.Drawing.Point(37, 231);
            this.lblErrorQuery.Name = "lblErrorQuery";
            this.lblErrorQuery.Size = new System.Drawing.Size(36, 17);
            this.lblErrorQuery.TabIndex = 4;
            this.lblErrorQuery.Text = "Test";
            // 
            // lblErrorText
            // 
            this.lblErrorText.AutoSize = true;
            this.lblErrorText.ForeColor = System.Drawing.Color.Red;
            this.lblErrorText.Location = new System.Drawing.Point(40, 255);
            this.lblErrorText.Name = "lblErrorText";
            this.lblErrorText.Size = new System.Drawing.Size(46, 17);
            this.lblErrorText.TabIndex = 5;
            this.lblErrorText.Text = "label2";
            // 
            // QueryBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 742);
            this.Controls.Add(this.lblErrorText);
            this.Controls.Add(this.lblErrorQuery);
            this.Controls.Add(this.displayGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QueryTextBox);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "QueryBrowser";
            this.Text = "QueryBrowser";
            ((System.ComponentModel.ISupportInitialize)(this.displayGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox QueryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView displayGridView;
        private System.Windows.Forms.Label lblErrorQuery;
        private System.Windows.Forms.Label lblErrorText;
    }
}

