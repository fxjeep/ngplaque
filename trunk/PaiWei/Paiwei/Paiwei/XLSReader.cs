using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Paiwei
{
	public class XLSReader
	{
		public string FileName { get; set; }
		public XLSReader(string filename)
		{
			this.FileName = filename;
			
		}

		/// <summary>
		/// Read data from xls file.
		/// </summary>
		/// <param name="sheetname"></param>
		public List<List<string>> GetData()
		{
		    var file = File.OpenText(FileName);

			List<List<string>> list = new List<List<string>>();
		    
			while (true)
			{
			    string line = file.ReadLine();
			    if (String.IsNullOrEmpty(line))
			        break;
			    string[] fields = line.Split(new string[] {"\t"}, StringSplitOptions.RemoveEmptyEntries);
				List<string> onerow = new List<string>();
				for (int i = 0; i < fields.Length; i++)
				{
					onerow.Add(fields[i]);
				}
				list.Add(onerow);
			}
			return list;
		}
	}
}
