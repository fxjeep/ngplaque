namespace Paiwei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lstNames = new System.Windows.Forms.ListView();
            this.No = new System.Windows.Forms.ColumnHeader();
            this.Names = new System.Windows.Forms.ColumnHeader();
            this.btnNameFile = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbPrintNameBox = new System.Windows.Forms.CheckBox();
            this.numEnglishSide = new System.Windows.Forms.NumericUpDown();
            this.numChineseSide = new System.Windows.Forms.NumericUpDown();
            this.numVietSide = new System.Windows.Forms.NumericUpDown();
            this.numVietMain = new System.Windows.Forms.NumericUpDown();
            this.numEnglishMain = new System.Windows.Forms.NumericUpDown();
            this.numChineseMain = new System.Windows.Forms.NumericUpDown();
            this.combVietFontName = new System.Windows.Forms.ComboBox();
            this.combEnglishFontName = new System.Windows.Forms.ComboBox();
            this.combChineseFontName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.gb = new System.Windows.Forms.GroupBox();
            this.txtMarginTop = new System.Windows.Forms.TextBox();
            this.txtMarginLeft = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbpdf = new System.Windows.Forms.GroupBox();
            this.cbOpenPDF = new System.Windows.Forms.CheckBox();
            this.btnViewPDF = new System.Windows.Forms.Button();
            this.btnGeneratePDF = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtEndNo = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStartNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rbPrintRange = new System.Windows.Forms.RadioButton();
            this.rbPrintAll = new System.Windows.Forms.RadioButton();
            this.numExtraLineSpace = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.numExtraSpaceSide = new System.Windows.Forms.NumericUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnglishSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChineseSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVietSide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVietMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnglishMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChineseMain)).BeginInit();
            this.gb.SuspendLayout();
            this.gbpdf.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraLineSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraSpaceSide)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbType);
            this.groupBox1.Controls.Add(this.picImage);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 476);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "1.Choose Image";
            // 
            // cbType
            // 
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Location = new System.Drawing.Point(7, 20);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(187, 21);
            this.cbType.TabIndex = 1;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(7, 47);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(187, 388);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lstNames);
            this.groupBox2.Controls.Add(this.btnNameFile);
            this.groupBox2.Location = new System.Drawing.Point(220, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(261, 476);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "2. Choose Name List";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(7, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(248, 21);
            this.label8.TabIndex = 2;
            // 
            // lstNames
            // 
            this.lstNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.No,
            this.Names});
            this.lstNames.FullRowSelect = true;
            this.lstNames.GridLines = true;
            this.lstNames.Location = new System.Drawing.Point(6, 40);
            this.lstNames.Name = "lstNames";
            this.lstNames.Size = new System.Drawing.Size(249, 400);
            this.lstNames.TabIndex = 1;
            this.lstNames.UseCompatibleStateImageBehavior = false;
            this.lstNames.View = System.Windows.Forms.View.Details;
            // 
            // No
            // 
            this.No.Text = "No";
            this.No.Width = 38;
            // 
            // Names
            // 
            this.Names.Width = 135;
            // 
            // btnNameFile
            // 
            this.btnNameFile.Location = new System.Drawing.Point(6, 446);
            this.btnNameFile.Name = "btnNameFile";
            this.btnNameFile.Size = new System.Drawing.Size(188, 23);
            this.btnNameFile.TabIndex = 0;
            this.btnNameFile.Text = "Choose Name List";
            this.btnNameFile.UseVisualStyleBackColor = true;
            this.btnNameFile.Click += new System.EventHandler(this.btnNameFile_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.gb);
            this.groupBox3.Controls.Add(this.gbpdf);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Location = new System.Drawing.Point(487, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 476);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "PDF";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numExtraSpaceSide);
            this.groupBox4.Controls.Add(this.numExtraLineSpace);
            this.groupBox4.Controls.Add(this.cbPrintNameBox);
            this.groupBox4.Controls.Add(this.numEnglishSide);
            this.groupBox4.Controls.Add(this.numChineseSide);
            this.groupBox4.Controls.Add(this.numVietSide);
            this.groupBox4.Controls.Add(this.numVietMain);
            this.groupBox4.Controls.Add(this.numEnglishMain);
            this.groupBox4.Controls.Add(this.numChineseMain);
            this.groupBox4.Controls.Add(this.combVietFontName);
            this.groupBox4.Controls.Add(this.combEnglishFontName);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.combChineseFontName);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Location = new System.Drawing.Point(8, 187);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(365, 227);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Font Settings";
            // 
            // cbPrintNameBox
            // 
            this.cbPrintNameBox.AutoSize = true;
            this.cbPrintNameBox.Location = new System.Drawing.Point(12, 204);
            this.cbPrintNameBox.Name = "cbPrintNameBox";
            this.cbPrintNameBox.Size = new System.Drawing.Size(310, 17);
            this.cbPrintNameBox.TabIndex = 6;
            this.cbPrintNameBox.Text = "Print box around name print rectangles. This is to test set up.";
            this.cbPrintNameBox.UseVisualStyleBackColor = true;
            // 
            // numEnglishSide
            // 
            this.numEnglishSide.DecimalPlaces = 1;
            this.numEnglishSide.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numEnglishSide.Location = new System.Drawing.Point(309, 76);
            this.numEnglishSide.Name = "numEnglishSide";
            this.numEnglishSide.Size = new System.Drawing.Size(48, 20);
            this.numEnglishSide.TabIndex = 5;
            // 
            // numChineseSide
            // 
            this.numChineseSide.DecimalPlaces = 1;
            this.numChineseSide.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numChineseSide.Location = new System.Drawing.Point(309, 37);
            this.numChineseSide.Name = "numChineseSide";
            this.numChineseSide.Size = new System.Drawing.Size(48, 20);
            this.numChineseSide.TabIndex = 5;
            // 
            // numVietSide
            // 
            this.numVietSide.DecimalPlaces = 1;
            this.numVietSide.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVietSide.Location = new System.Drawing.Point(309, 109);
            this.numVietSide.Name = "numVietSide";
            this.numVietSide.Size = new System.Drawing.Size(48, 20);
            this.numVietSide.TabIndex = 5;
            // 
            // numVietMain
            // 
            this.numVietMain.DecimalPlaces = 1;
            this.numVietMain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVietMain.Location = new System.Drawing.Point(251, 109);
            this.numVietMain.Name = "numVietMain";
            this.numVietMain.Size = new System.Drawing.Size(48, 20);
            this.numVietMain.TabIndex = 5;
            // 
            // numEnglishMain
            // 
            this.numEnglishMain.DecimalPlaces = 1;
            this.numEnglishMain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numEnglishMain.Location = new System.Drawing.Point(251, 76);
            this.numEnglishMain.Name = "numEnglishMain";
            this.numEnglishMain.Size = new System.Drawing.Size(48, 20);
            this.numEnglishMain.TabIndex = 5;
            // 
            // numChineseMain
            // 
            this.numChineseMain.DecimalPlaces = 1;
            this.numChineseMain.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numChineseMain.Location = new System.Drawing.Point(251, 37);
            this.numChineseMain.Name = "numChineseMain";
            this.numChineseMain.Size = new System.Drawing.Size(48, 20);
            this.numChineseMain.TabIndex = 5;
            // 
            // combVietFontName
            // 
            this.combVietFontName.DisplayMember = "Name";
            this.combVietFontName.FormattingEnabled = true;
            this.combVietFontName.Location = new System.Drawing.Point(143, 109);
            this.combVietFontName.Name = "combVietFontName";
            this.combVietFontName.Size = new System.Drawing.Size(100, 21);
            this.combVietFontName.TabIndex = 4;
            this.combVietFontName.ValueMember = "File";
            // 
            // combEnglishFontName
            // 
            this.combEnglishFontName.DisplayMember = "Name";
            this.combEnglishFontName.FormattingEnabled = true;
            this.combEnglishFontName.Location = new System.Drawing.Point(143, 76);
            this.combEnglishFontName.Name = "combEnglishFontName";
            this.combEnglishFontName.Size = new System.Drawing.Size(100, 21);
            this.combEnglishFontName.TabIndex = 4;
            this.combEnglishFontName.ValueMember = "File";
            // 
            // combChineseFontName
            // 
            this.combChineseFontName.DisplayMember = "Name";
            this.combChineseFontName.FormattingEnabled = true;
            this.combChineseFontName.Location = new System.Drawing.Point(143, 37);
            this.combChineseFontName.Name = "combChineseFontName";
            this.combChineseFontName.Size = new System.Drawing.Size(100, 21);
            this.combChineseFontName.TabIndex = 4;
            this.combChineseFontName.ValueMember = "File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Vietnamese Font Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "English Font Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(309, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "SideSize";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(249, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "MainSize";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Chinese Font Name";
            // 
            // gb
            // 
            this.gb.Controls.Add(this.txtMarginTop);
            this.gb.Controls.Add(this.txtMarginLeft);
            this.gb.Controls.Add(this.label3);
            this.gb.Controls.Add(this.label1);
            this.gb.Location = new System.Drawing.Point(7, 122);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(365, 59);
            this.gb.TabIndex = 4;
            this.gb.TabStop = false;
            this.gb.Text = "Margin";
            // 
            // txtMarginTop
            // 
            this.txtMarginTop.Location = new System.Drawing.Point(247, 21);
            this.txtMarginTop.Name = "txtMarginTop";
            this.txtMarginTop.Size = new System.Drawing.Size(100, 20);
            this.txtMarginTop.TabIndex = 2;
            this.txtMarginTop.Text = "0.5";
            // 
            // txtMarginLeft
            // 
            this.txtMarginLeft.Location = new System.Drawing.Point(62, 21);
            this.txtMarginLeft.Name = "txtMarginLeft";
            this.txtMarginLeft.Size = new System.Drawing.Size(100, 20);
            this.txtMarginLeft.TabIndex = 2;
            this.txtMarginLeft.Text = "0.5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(203, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Top:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Left:";
            // 
            // gbpdf
            // 
            this.gbpdf.Controls.Add(this.cbOpenPDF);
            this.gbpdf.Controls.Add(this.btnViewPDF);
            this.gbpdf.Controls.Add(this.btnGeneratePDF);
            this.gbpdf.Location = new System.Drawing.Point(8, 420);
            this.gbpdf.Name = "gbpdf";
            this.gbpdf.Size = new System.Drawing.Size(365, 49);
            this.gbpdf.TabIndex = 3;
            this.gbpdf.TabStop = false;
            this.gbpdf.Text = "Output PDF";
            // 
            // cbOpenPDF
            // 
            this.cbOpenPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOpenPDF.AutoSize = true;
            this.cbOpenPDF.Location = new System.Drawing.Point(12, 20);
            this.cbOpenPDF.Name = "cbOpenPDF";
            this.cbOpenPDF.Size = new System.Drawing.Size(141, 17);
            this.cbOpenPDF.TabIndex = 5;
            this.cbOpenPDF.Text = "Open PDF Automatically";
            this.cbOpenPDF.UseVisualStyleBackColor = true;
            // 
            // btnViewPDF
            // 
            this.btnViewPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewPDF.Enabled = false;
            this.btnViewPDF.Location = new System.Drawing.Point(174, 20);
            this.btnViewPDF.Name = "btnViewPDF";
            this.btnViewPDF.Size = new System.Drawing.Size(74, 23);
            this.btnViewPDF.TabIndex = 3;
            this.btnViewPDF.Text = "View PDF";
            this.btnViewPDF.UseVisualStyleBackColor = true;
            this.btnViewPDF.Click += new System.EventHandler(this.btnViewPDF_Click);
            // 
            // btnGeneratePDF
            // 
            this.btnGeneratePDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGeneratePDF.Enabled = false;
            this.btnGeneratePDF.Location = new System.Drawing.Point(254, 20);
            this.btnGeneratePDF.Name = "btnGeneratePDF";
            this.btnGeneratePDF.Size = new System.Drawing.Size(97, 23);
            this.btnGeneratePDF.TabIndex = 2;
            this.btnGeneratePDF.Text = "Generate PDF";
            this.btnGeneratePDF.UseVisualStyleBackColor = true;
            this.btnGeneratePDF.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtEndNo);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txtStartNo);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.rbPrintRange);
            this.groupBox5.Controls.Add(this.rbPrintAll);
            this.groupBox5.Location = new System.Drawing.Point(6, 20);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(366, 95);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Name Range";
            // 
            // txtEndNo
            // 
            this.txtEndNo.Enabled = false;
            this.txtEndNo.Location = new System.Drawing.Point(250, 63);
            this.txtEndNo.Name = "txtEndNo";
            this.txtEndNo.Size = new System.Drawing.Size(100, 20);
            this.txtEndNo.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "End No.";
            // 
            // txtStartNo
            // 
            this.txtStartNo.Enabled = false;
            this.txtStartNo.Location = new System.Drawing.Point(62, 63);
            this.txtStartNo.Name = "txtStartNo";
            this.txtStartNo.Size = new System.Drawing.Size(100, 20);
            this.txtStartNo.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Start No.";
            // 
            // rbPrintRange
            // 
            this.rbPrintRange.AutoSize = true;
            this.rbPrintRange.Location = new System.Drawing.Point(8, 43);
            this.rbPrintRange.Name = "rbPrintRange";
            this.rbPrintRange.Size = new System.Drawing.Size(128, 17);
            this.rbPrintRange.TabIndex = 0;
            this.rbPrintRange.Text = "Print Names in Range";
            this.rbPrintRange.UseVisualStyleBackColor = true;
            this.rbPrintRange.CheckedChanged += new System.EventHandler(this.rbPrintRange_CheckedChanged);
            // 
            // rbPrintAll
            // 
            this.rbPrintAll.AutoSize = true;
            this.rbPrintAll.Checked = true;
            this.rbPrintAll.Location = new System.Drawing.Point(7, 20);
            this.rbPrintAll.Name = "rbPrintAll";
            this.rbPrintAll.Size = new System.Drawing.Size(96, 17);
            this.rbPrintAll.TabIndex = 0;
            this.rbPrintAll.TabStop = true;
            this.rbPrintAll.Text = "Print All Names";
            this.rbPrintAll.UseVisualStyleBackColor = true;
            this.rbPrintAll.CheckedChanged += new System.EventHandler(this.rbPrintAll_CheckedChanged);
            // 
            // numExtraLineSpace
            // 
            this.numExtraLineSpace.Location = new System.Drawing.Point(143, 139);
            this.numExtraLineSpace.Name = "numExtraLineSpace";
            this.numExtraLineSpace.Size = new System.Drawing.Size(120, 20);
            this.numExtraLineSpace.TabIndex = 7;
            this.numExtraLineSpace.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 143);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Main Spacing Extra";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(11, 169);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Side Spacing Extra";
            // 
            // numExtraSpaceSide
            // 
            this.numExtraSpaceSide.Location = new System.Drawing.Point(143, 165);
            this.numExtraSpaceSide.Name = "numExtraSpaceSide";
            this.numExtraSpaceSide.Size = new System.Drawing.Size(120, 20);
            this.numExtraSpaceSide.TabIndex = 7;
            this.numExtraSpaceSide.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 494);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "PaiWei PDF Creator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEnglishSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChineseSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVietSide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numVietMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnglishMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChineseMain)).EndInit();
            this.gb.ResumeLayout(false);
            this.gb.PerformLayout();
            this.gbpdf.ResumeLayout(false);
            this.gbpdf.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraLineSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numExtraSpaceSide)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnNameFile;
		private System.Windows.Forms.ListView lstNames;
		private System.Windows.Forms.ColumnHeader Names;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ColumnHeader No;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton rbPrintRange;
		private System.Windows.Forms.RadioButton rbPrintAll;
		private System.Windows.Forms.TextBox txtEndNo;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtStartNo;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox gbpdf;
		private System.Windows.Forms.Button btnViewPDF;
		private System.Windows.Forms.Button btnGeneratePDF;
		private System.Windows.Forms.CheckBox cbOpenPDF;
		private System.Windows.Forms.ComboBox cbType;
		private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox gb;
		private System.Windows.Forms.TextBox txtMarginLeft;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtMarginTop;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox combChineseFontName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox combVietFontName;
        private System.Windows.Forms.ComboBox combEnglishFontName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numEnglishSide;
        private System.Windows.Forms.NumericUpDown numChineseSide;
        private System.Windows.Forms.NumericUpDown numVietSide;
        private System.Windows.Forms.NumericUpDown numVietMain;
        private System.Windows.Forms.NumericUpDown numEnglishMain;
        private System.Windows.Forms.NumericUpDown numChineseMain;
        private System.Windows.Forms.CheckBox cbPrintNameBox;
        private System.Windows.Forms.NumericUpDown numExtraLineSpace;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numExtraSpaceSide;
        private System.Windows.Forms.Label label12;
	}
}

