namespace Dynamic
{
    partial class Form1
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
            this.buttonLoadFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.richTextBoxStatus = new System.Windows.Forms.RichTextBox();
            this.buttonShowMatrix = new System.Windows.Forms.Button();
            this.buttonDynamic = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxRepeatsInTest = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonTest = new System.Windows.Forms.Button();
            this.textBoxDimension = new System.Windows.Forms.TextBox();
            this.buttonLoadFromList = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonGeneticTest = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxPopulationSize = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxNumberOfGenerations = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxMutationChance = new System.Windows.Forms.TextBox();
            this.buttonGeneticAlgorithm = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.textBoxTotalIterations = new System.Windows.Forms.TextBox();
            this.textBoxTabuTime = new System.Windows.Forms.TextBox();
            this.buttonTabuTest = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonTabuSearch = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadFile
            // 
            this.buttonLoadFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLoadFile.Location = new System.Drawing.Point(3, 3);
            this.buttonLoadFile.Name = "buttonLoadFile";
            this.buttonLoadFile.Size = new System.Drawing.Size(93, 49);
            this.buttonLoadFile.TabIndex = 1;
            this.buttonLoadFile.Text = "Load file";
            this.buttonLoadFile.UseVisualStyleBackColor = true;
            this.buttonLoadFile.Click += new System.EventHandler(this.buttonLoadFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // richTextBoxStatus
            // 
            this.richTextBoxStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxStatus.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxStatus.Name = "richTextBoxStatus";
            this.richTextBoxStatus.ReadOnly = true;
            this.richTextBoxStatus.Size = new System.Drawing.Size(523, 555);
            this.richTextBoxStatus.TabIndex = 2;
            this.richTextBoxStatus.Text = "";
            // 
            // buttonShowMatrix
            // 
            this.buttonShowMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonShowMatrix.Location = new System.Drawing.Point(3, 113);
            this.buttonShowMatrix.Name = "buttonShowMatrix";
            this.buttonShowMatrix.Size = new System.Drawing.Size(93, 49);
            this.buttonShowMatrix.TabIndex = 3;
            this.buttonShowMatrix.Text = "Show matrix";
            this.buttonShowMatrix.UseVisualStyleBackColor = true;
            this.buttonShowMatrix.Click += new System.EventHandler(this.buttonShowMatrix_Click);
            // 
            // buttonDynamic
            // 
            this.buttonDynamic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonDynamic.Location = new System.Drawing.Point(3, 168);
            this.buttonDynamic.Name = "buttonDynamic";
            this.buttonDynamic.Size = new System.Drawing.Size(93, 49);
            this.buttonDynamic.TabIndex = 4;
            this.buttonDynamic.Text = "Dynamic";
            this.buttonDynamic.UseVisualStyleBackColor = true;
            this.buttonDynamic.Click += new System.EventHandler(this.buttonDynamic_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.richTextBoxStatus, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.listView1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1058, 561);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.buttonLoadFile, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxRepeatsInTest, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.buttonTest, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.textBoxDimension, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.buttonLoadFromList, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.buttonDynamic, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.buttonShowMatrix, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.buttonClear, 0, 9);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(743, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 10;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(99, 555);
            this.tableLayoutPanel2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of repeats in tests";
            // 
            // textBoxRepeatsInTest
            // 
            this.textBoxRepeatsInTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRepeatsInTest.Location = new System.Drawing.Point(3, 292);
            this.textBoxRepeatsInTest.Name = "textBoxRepeatsInTest";
            this.textBoxRepeatsInTest.Size = new System.Drawing.Size(93, 20);
            this.textBoxRepeatsInTest.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 351);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Matrix dimension";
            // 
            // buttonTest
            // 
            this.buttonTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTest.Location = new System.Drawing.Point(3, 443);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(93, 49);
            this.buttonTest.TabIndex = 7;
            this.buttonTest.Text = "Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // textBoxDimension
            // 
            this.textBoxDimension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDimension.Location = new System.Drawing.Point(3, 402);
            this.textBoxDimension.Name = "textBoxDimension";
            this.textBoxDimension.Size = new System.Drawing.Size(93, 20);
            this.textBoxDimension.TabIndex = 9;
            // 
            // buttonLoadFromList
            // 
            this.buttonLoadFromList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonLoadFromList.Location = new System.Drawing.Point(3, 58);
            this.buttonLoadFromList.Name = "buttonLoadFromList";
            this.buttonLoadFromList.Size = new System.Drawing.Size(93, 49);
            this.buttonLoadFromList.TabIndex = 10;
            this.buttonLoadFromList.Text = "Load from list";
            this.buttonLoadFromList.UseVisualStyleBackColor = true;
            this.buttonLoadFromList.Click += new System.EventHandler(this.buttonLoadFromList_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonClear.Location = new System.Drawing.Point(3, 498);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(93, 54);
            this.buttonClear.TabIndex = 11;
            this.buttonClear.Text = "Clear workspace";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(532, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(205, 555);
            this.listView1.TabIndex = 8;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.buttonGeneticTest, 0, 8);
            this.tableLayoutPanel6.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.textBoxPopulationSize, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label6, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.textBoxNumberOfGenerations, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel6.Controls.Add(this.textBoxMutationChance, 0, 5);
            this.tableLayoutPanel6.Controls.Add(this.buttonGeneticAlgorithm, 0, 6);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(953, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 10;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(102, 555);
            this.tableLayoutPanel6.TabIndex = 9;
            // 
            // buttonGeneticTest
            // 
            this.buttonGeneticTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeneticTest.Location = new System.Drawing.Point(3, 443);
            this.buttonGeneticTest.Name = "buttonGeneticTest";
            this.buttonGeneticTest.Size = new System.Drawing.Size(96, 49);
            this.buttonGeneticTest.TabIndex = 0;
            this.buttonGeneticTest.Text = "Test genetic";
            this.buttonGeneticTest.UseVisualStyleBackColor = true;
            this.buttonGeneticTest.Click += new System.EventHandler(this.buttonGeneticTest_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Population size";
            // 
            // textBoxPopulationSize
            // 
            this.textBoxPopulationSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPopulationSize.Location = new System.Drawing.Point(3, 58);
            this.textBoxPopulationSize.Name = "textBoxPopulationSize";
            this.textBoxPopulationSize.Size = new System.Drawing.Size(96, 20);
            this.textBoxPopulationSize.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(3, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 39);
            this.label6.TabIndex = 3;
            this.label6.Text = "Number of generations (iterations)";
            // 
            // textBoxNumberOfGenerations
            // 
            this.textBoxNumberOfGenerations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNumberOfGenerations.Location = new System.Drawing.Point(3, 168);
            this.textBoxNumberOfGenerations.Name = "textBoxNumberOfGenerations";
            this.textBoxNumberOfGenerations.Size = new System.Drawing.Size(96, 20);
            this.textBoxNumberOfGenerations.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(3, 262);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Mutation chance";
            // 
            // textBoxMutationChance
            // 
            this.textBoxMutationChance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMutationChance.Location = new System.Drawing.Point(3, 278);
            this.textBoxMutationChance.Name = "textBoxMutationChance";
            this.textBoxMutationChance.Size = new System.Drawing.Size(96, 20);
            this.textBoxMutationChance.TabIndex = 6;
            // 
            // buttonGeneticAlgorithm
            // 
            this.buttonGeneticAlgorithm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonGeneticAlgorithm.Location = new System.Drawing.Point(3, 333);
            this.buttonGeneticAlgorithm.Name = "buttonGeneticAlgorithm";
            this.buttonGeneticAlgorithm.Size = new System.Drawing.Size(96, 49);
            this.buttonGeneticAlgorithm.TabIndex = 7;
            this.buttonGeneticAlgorithm.Text = "Genetic algorithm";
            this.buttonGeneticAlgorithm.UseVisualStyleBackColor = true;
            this.buttonGeneticAlgorithm.Click += new System.EventHandler(this.buttonGeneticAlgorithm_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.textBoxTotalIterations, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.textBoxTabuTime, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.buttonTabuTest, 0, 8);
            this.tableLayoutPanel3.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.buttonTabuSearch, 0, 4);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(848, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 10;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(99, 555);
            this.tableLayoutPanel3.TabIndex = 7;
            // 
            // textBoxTotalIterations
            // 
            this.textBoxTotalIterations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTotalIterations.Location = new System.Drawing.Point(3, 58);
            this.textBoxTotalIterations.Name = "textBoxTotalIterations";
            this.textBoxTotalIterations.Size = new System.Drawing.Size(93, 20);
            this.textBoxTotalIterations.TabIndex = 1;
            // 
            // textBoxTabuTime
            // 
            this.textBoxTabuTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTabuTime.Location = new System.Drawing.Point(3, 168);
            this.textBoxTabuTime.Name = "textBoxTabuTime";
            this.textBoxTabuTime.Size = new System.Drawing.Size(93, 20);
            this.textBoxTabuTime.TabIndex = 1;
            // 
            // buttonTabuTest
            // 
            this.buttonTabuTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTabuTest.Location = new System.Drawing.Point(3, 443);
            this.buttonTabuTest.Name = "buttonTabuTest";
            this.buttonTabuTest.Size = new System.Drawing.Size(93, 49);
            this.buttonTabuTest.TabIndex = 4;
            this.buttonTabuTest.Text = "Tabu test";
            this.buttonTabuTest.UseVisualStyleBackColor = true;
            this.buttonTabuTest.Click += new System.EventHandler(this.buttonTabuTest_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tabu duration";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 26);
            this.label3.TabIndex = 0;
            this.label3.Text = "Number of iterations";
            // 
            // buttonTabuSearch
            // 
            this.buttonTabuSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonTabuSearch.Location = new System.Drawing.Point(3, 223);
            this.buttonTabuSearch.Name = "buttonTabuSearch";
            this.buttonTabuSearch.Size = new System.Drawing.Size(93, 49);
            this.buttonTabuSearch.TabIndex = 3;
            this.buttonTabuSearch.Text = "Tabu search";
            this.buttonTabuSearch.UseVisualStyleBackColor = true;
            this.buttonTabuSearch.Click += new System.EventHandler(this.buttonTabuSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "PEA Sebastian Jantos 225982";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonLoadFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RichTextBox richTextBoxStatus;
        private System.Windows.Forms.Button buttonShowMatrix;
        private System.Windows.Forms.Button buttonDynamic;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxRepeatsInTest;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDimension;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTotalIterations;
        private System.Windows.Forms.TextBox textBoxTabuTime;
        private System.Windows.Forms.Button buttonTabuSearch;
        private System.Windows.Forms.Button buttonTabuTest;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Button buttonLoadFromList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.Button buttonGeneticTest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxPopulationSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxNumberOfGenerations;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxMutationChance;
        private System.Windows.Forms.Button buttonGeneticAlgorithm;
        private System.Windows.Forms.Button buttonClear;
    }
}

