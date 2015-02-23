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
            ((System.ComponentModel.ISupportInitialize)(this.displayGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(902, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 90);
            this.button1.TabIndex = 0;
            this.button1.Text = "Execute";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.Location = new System.Drawing.Point(37, 73);
            this.QueryTextBox.Multiline = true;
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.Size = new System.Drawing.Size(838, 140);
            this.QueryTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Query";
            // 
            // displayGridView
            // 
            this.displayGridView.AllowUserToOrderColumns = true;
            this.displayGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.displayGridView.Location = new System.Drawing.Point(37, 278);
            this.displayGridView.Name = "displayGridView";
            this.displayGridView.RowTemplate.Height = 24;
            this.displayGridView.Size = new System.Drawing.Size(838, 344);
            this.displayGridView.TabIndex = 3;
            // 
            // QueryBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 658);
            this.Controls.Add(this.displayGridView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.QueryTextBox);
            this.Controls.Add(this.button1);
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
    }
}

