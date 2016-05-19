using System;
using System.Diagnostics;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Paiwei
{
    /// <summary>
    /// Main window
    /// </summary>
	public partial class Form1 : Form
	{
		string namefile;
		string pdffile;

		List<List<string>> NameData;		

        public PDFCreationParameter settings = new PDFCreationParameter();

        string ConfigureXML = "paiwei.config";

		public Form1()
		{
			InitializeComponent();
            LoadPaiweiConfigFile();

		}

        /// <summary>
        /// Load configuration file paiwei.config
        /// </summary>
        public void LoadPaiweiConfigFile()
        {
            ExeConfigurationFileMap exeMap = new ExeConfigurationFileMap();
            exeMap.ExeConfigFilename = this.ConfigureXML;

            Configuration exeConfig = ConfigurationManager.OpenMappedExeConfiguration(
                                        exeMap, ConfigurationUserLevel.None);

            FontConfigureSectionGroup group =  (FontConfigureSectionGroup)exeConfig.GetSection("FontConfigure");

            //setup font dropdown menus using the setup from configuration.
            this.numChineseMain.Value = group.ChineseFonts.MainSize;
            this.numChineseSide.Value = group.ChineseFonts.SideSize;

            this.numEnglishMain.Value = group.EnglishFonts.MainSize;
            this.numEnglishSide.Value = group.EnglishFonts.SideSize;

            this.numVietMain.Value = group.VietnameseFonts.MainSize;
            this.numVietSide.Value = group.VietnameseFonts.SideSize;

            //setup dropdown lists
            for (int i = 0; i < group.ChineseFonts.Count; i++)
            {
                FontSection sect = group.ChineseFonts[i];
                this.combChineseFontName.Items.Add(sect);
            }
            this.combChineseFontName.SelectedIndex = 0;

            for (int i = 0; i < group.EnglishFonts.Count; i++)
            {
                FontSection sect = group.EnglishFonts[i];
                this.combEnglishFontName.Items.Add(sect);
            }
            this.combEnglishFontName.SelectedIndex = 0;

            for (int i = 0; i < group.VietnameseFonts.Count; i++)
            {
                FontSection sect = group.VietnameseFonts[i];
                this.combVietFontName.Items.Add(sect);
            }
            this.combVietFontName.SelectedIndex = 0;
        }

        /// <summary>
        /// display an open file dialog to load excel file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNameFile_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.RestoreDirectory = true;
			dlg.Filter = "Txt Files|*.txt";
			DialogResult result = dlg.ShowDialog();

			if (result == DialogResult.OK)
			{
				string namefile1 = dlg.FileName;
				this.namefile = namefile1;
				this.LoadNames(namefile1);
			}
		}

        /// <summary>
        /// load data from excel file.
        /// </summary>
        /// <param name="filename"></param>
		private void LoadNames(string filename)
		{
			this.namefile = filename;
			XLSReader reader = new XLSReader(filename);

			try
			{
				NameData = reader.GetData();

				//add data into listview.
				AddDataIntoList(NameData);
				
				this.btnGeneratePDF.Enabled = true;

				this.label8.Text = new FileInfo(filename).Name;
			}
			catch (Exception excep)
			{
				MessageBox.Show("Exception: " + excep.Message);
			}
		}

		/// <summary>
		/// Add data into listview
		/// </summary>
		/// <param name="data"></param>
		public void AddDataIntoList(List<List<string>> data)
		{
			int count = 1;
			this.lstNames.Items.Clear();
			foreach (List<string> row in data)
			{
				ListViewItem item = new ListViewItem(count.ToString());
				foreach (string field in row)
				{
					item.SubItems.Add(field);
				}
				this.lstNames.Items.Add(item);
				count++;
			}
		}

		/// <summary>
		/// set column headers to No., Column1, Column2...
		/// </summary>
		/// <param name="col"></param>
		public void SetColumnHeaders(int col)
		{
			this.lstNames.Columns.Clear();
			ColumnHeader headerno = new ColumnHeader();
			headerno.Text = "No.";
			this.lstNames.Columns.Add(headerno);

			for (int c = 0; c < col; c++)
			{
				ColumnHeader header = new ColumnHeader();
				header.Text = "Column " + c.ToString();
				this.lstNames.Columns.Add(header);
			}
		}

		/// <summary>
		/// Create pdf 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button1_Click_1(object sender, EventArgs e)
		{
			//get text rectangel
			try
			{
				this.settings.MargionTop = float.Parse(this.txtMarginTop.Text);
                this.settings.MarginLeft = float.Parse(this.txtMarginLeft.Text);
			}
			catch (Exception ex1)
			{
				MessageBox.Show("Input in Margin Text Box is wrong: " + ex1.Message);
				return;
			}

			//get name range
			try
			{
				if (this.rbPrintAll.Checked)
				{
                    this.settings.StartNo = 0;
                    this.settings.EndNo = this.lstNames.Items.Count - 1;
				}
				else
				{
                    this.settings.StartNo = Int32.Parse(this.txtStartNo.Text) - 1;
                    this.settings.EndNo = Int32.Parse(this.txtEndNo.Text) - 1;
				}
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Input in Name Range is wrong: " + ex2.Message);
				return;
			}

			try
			{
                FontSection fs = this.combChineseFontName.Items[this.combChineseFontName.SelectedIndex] as FontSection;

                FontParameter chinese = new FontParameter(
                                        fs.Name,
                                        fs.File,
                                        this.numChineseMain.Value,
                                        this.numChineseSide.Value,
                                        TextType.Chinese
                                            );
                this.settings.ChineseFontParameter = chinese;

                fs = this.combEnglishFontName.Items[this.combEnglishFontName.SelectedIndex] as FontSection;
                FontParameter english = new FontParameter(
                                        fs.Name,
                                        fs.File,
                                        this.numEnglishMain.Value,
                                        this.numEnglishSide.Value,
                                        TextType.English
                                            );
                this.settings.EnglishFontParameter = english;

                fs = this.combVietFontName.Items[this.combVietFontName.SelectedIndex] as FontSection;
                FontParameter vietnamese = new FontParameter(
                        fs.Name,
                        fs.File,
                        this.numVietMain.Value,
                        this.numVietSide.Value,
                        TextType.Vietnamese
                            );
                this.settings.VietnameseFontParameter = vietnamese;
			}
			catch (Exception ex2)
			{
				MessageBox.Show("Input in Font Size is wrong: " + ex2.Message);
				return;
			}

            this.settings.IsPrintNameBox = this.cbPrintNameBox.Checked;

            PrintType type = (PrintType)this.cbType.SelectedItem;
            TypeValues value = type.Value;
            if (value == TypeValues.ChangeSheng)
            {
                this.settings.Printer = new ChangShengYuanQingPrinter();
                this.settings.Type = TypeValues.ChangeSheng;
                this.settings.ImageFile = Path.Combine(Directory.GetCurrentDirectory(), "changsheng.jpg");
            }
            else if (value == TypeValues.WangSheng)
            {
                this.settings.Printer = new WangShengPrinter();
                this.settings.Type = TypeValues.WangSheng;
                this.settings.ImageFile = Path.Combine(Directory.GetCurrentDirectory(), "wangshen.jpg");
            }
            else if (value == TypeValues.YuanQing)
            {
                this.settings.Printer = new YuanQingPrinter();
                this.settings.Type = TypeValues.YuanQing;
                this.settings.ImageFile = Path.Combine(Directory.GetCurrentDirectory(), "yuanqing.jpg");
            }
            else if (value == TypeValues.ZhuXian)
            {
                this.settings.Printer = new ZhuXianPrinter();
                this.settings.Type = TypeValues.ZhuXian;
                this.settings.ImageFile = Path.Combine(Directory.GetCurrentDirectory(), "zhuxian.jpg");
            }
            else
            {
                MessageBox.Show("Incorrect TypeValue selected :" + this.cbType.SelectedValue.ToString());
            }

            //extra line space
            this.settings.ExtraLineSpaceMain = (float)((int)this.numExtraLineSpace.Value * 2.54f / 96);
            this.settings.ExtraLineSpaceSide = (float)((int)this.numExtraSpaceSide.Value * 2.54f / 96);

			//generate pdf.
			this.GeneratePDF(this.settings);

			if (this.cbOpenPDF.Checked)
			{
				this.ViewPDF();
			}
			this.btnViewPDF.Enabled = true;
		}

        /// <summary>
        /// Generate pdf according to the excel file.
        /// </summary>
        /// <param name="parameter"></param>
        public void GeneratePDF(PDFCreationParameter parameter)
		{
			this.Cursor = Cursors.WaitCursor;

            // get string name of pdf file
            string pdffilename = GetPDFFileName();
			if (String.IsNullOrEmpty(pdffilename))
			{
				MessageBox.Show("Please load name file first");
				return;
			}

			this.pdffile = pdffilename;

			//initialise pdf writer and document.
            parameter.Printer.Initial(pdffilename);
            parameter.PrintData(NameData);
            parameter.Printer.FinalisePDF();
			this.Cursor = Cursors.Arrow;
		}

        /// <summary>
        /// return pdf file name. PDF File is genearted in the same directory of excel file.
        /// </summary>
        /// <returns></returns>
		public string GetPDFFileName()
		{
			if (File.Exists(this.namefile))
			{
				FileInfo namefileinfo = new FileInfo(this.namefile);
				string pdffilename = Path.Combine(namefileinfo.Directory.ToString(), namefileinfo.Name.Replace(namefileinfo.Extension, ".pdf"));
				return pdffilename;
			}

			return "";
		}

		/// <summary>
		/// View pdf
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnViewPDF_Click(object sender, EventArgs e)
		{
			this.ViewPDF();
		}

        /// <summary>
        /// View pdf
        /// </summary>
		public void ViewPDF()
		{
			if (File.Exists(this.pdffile))
			{
				Process p = new Process();
				p.StartInfo = new ProcessStartInfo(this.pdffile);
				p.Start();
				p.WaitForExit();
			}
		}

		/// <summary>
		/// display corresponding image in picturebox, it will also set corresponding 
		/// parameters in PDFParameter.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cbType_SelectedIndexChanged(object sender, EventArgs e)
		{
            PrintType type = (PrintType)this.cbType.SelectedItem;
            TypeValues value = type.Value;
            if (value == TypeValues.ChangeSheng)
            {
                this.SetImage("changsheng.jpg");
            }
            else if (value == TypeValues.WangSheng)
            {
                this.SetImage("wangshen.jpg");
            }
            else if (value == TypeValues.YuanQing)
            {
                this.SetImage("yuanqing.jpg");
            }
            else if (value == TypeValues.ZhuXian)
            {
                this.SetImage("zhuxian.jpg");
            }
            else
            {
                MessageBox.Show("Incorrect TypeValue selected :" + this.cbType.SelectedValue.ToString());
            }
		}

		/// <summary>
		/// Load Image and display it in proper ratio.
		/// </summary>
		/// <param name="filename"></param>
		public void SetImage(string filename)
		{
			if (File.Exists(filename))
			{
				this.picImage.SizeMode = PictureBoxSizeMode.StretchImage;
				this.picImage.Load(filename);

				//Initialise the position boxes and image related parameter
			}
			else
			{
				MessageBox.Show("File " + filename + " does not exist." + Directory.GetCurrentDirectory());
			}
		}

        /// <summary>
        /// load even handler for this form. it will do data binding for document type combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void Form1_Load(object sender, EventArgs e)
		{

			this.cbType.DataSource = Prints.TypeList;
			this.cbType.DisplayMember = "Name";
		}

        /// <summary>
        /// Change print range.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void rbPrintRange_CheckedChanged(object sender, EventArgs e)
		{
			if (this.NameData == null && rbPrintRange.Checked)
			{
				MessageBox.Show("Please input name data.");
				this.rbPrintAll.Checked = true;
				return;
			}

			if (rbPrintRange.Checked)
			{
				this.txtStartNo.Enabled = true;
				this.txtEndNo.Enabled = true;
				this.txtStartNo.Text = "1";
				if (this.NameData != null)
				{
					this.txtEndNo.Text = this.NameData.Count.ToString();
				}
				else
				{
					this.txtEndNo.Text = "1";
				}
			}
		}

        /// <summary>
        /// print all 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void rbPrintAll_CheckedChanged(object sender, EventArgs e)
		{
			this.txtStartNo.Enabled = false;
			this.txtEndNo.Enabled = false;
		}
	}
}
