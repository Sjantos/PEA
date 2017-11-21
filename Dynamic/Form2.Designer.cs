namespace Dynamic
{
    partial class MatrixView
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
            this.richTextBoxMatrix = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxMatrix
            // 
            this.richTextBoxMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMatrix.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxMatrix.Name = "richTextBoxMatrix";
            this.richTextBoxMatrix.ReadOnly = true;
            this.richTextBoxMatrix.Size = new System.Drawing.Size(1078, 721);
            this.richTextBoxMatrix.TabIndex = 0;
            this.richTextBoxMatrix.Text = "";
            // 
            // MatrixView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1078, 721);
            this.Controls.Add(this.richTextBoxMatrix);
            this.Name = "MatrixView";
            this.Text = "Matrix view";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxMatrix;
    }
}