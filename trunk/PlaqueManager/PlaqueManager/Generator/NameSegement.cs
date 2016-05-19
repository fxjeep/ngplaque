using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlaqueManager.Generator
{
    public class NameSegement
    {
        public List<TextSegement> NameSegements = new List<TextSegement>();

        public bool HasEnglish = false;

        public NameSegement(string name)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < name.Length; i++)
            {
                //get a character
                int code = Char.ConvertToUtf32(name, i);
                string chara = name.Substring(i, 1);

                if (code >= Configure.ChineseFrom && code <= Configure.ChineseEnd)
                {
                    if ( sb.Length > 0 )
                    {
                        TextSegement seg1 = new TextSegement();
                        seg1.Type = EnumSegementType.English;
                        seg1.Text = sb.ToString();
                        NameSegements.Add(seg1);
                        sb.Clear();
                    }

                    //if all texts are chinese, we should create a list of names, 
                    //1 character each name
                    TextSegement seg = new TextSegement();
                    seg.Type = EnumSegementType.Chinese;
                    seg.Text = chara;
                    NameSegements.Add(seg);
                }
                else
                {
                    //if it is english, we split english by space
                    HasEnglish = true;

                    if (chara.Equals(" ") || code == 12288)
                    {
                        TextSegement seg = new TextSegement();
                        seg.Type = EnumSegementType.English;
                        seg.Text = sb.ToString();
                        NameSegements.Add(seg);
                        sb.Clear();
                    }
                    else
                    {
                        sb.Append(chara);
                    }
                }
            }

            if (sb.Length > 0)
            {
                TextSegement seg = new TextSegement();
                seg.Type = EnumSegementType.English;
                seg.Text = sb.ToString();
                NameSegements.Add(seg);
                sb.Clear();
            }
        }

        public string MainString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TextSegement seg in this.NameSegements)
            {
                sb.Append(seg.Text + "\n");
            }
            return sb.ToString();
        }

        public string SideString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (TextSegement seg in this.NameSegements)
            {
                sb.Append(seg.Text + " ");
            }
            return sb.ToString();
        }
    }

    public class TextSegement
    {
        public string Text;
        public EnumSegementType Type;
    }

    public enum EnumSegementType
    {
        Chinese,
        English
    }
}
