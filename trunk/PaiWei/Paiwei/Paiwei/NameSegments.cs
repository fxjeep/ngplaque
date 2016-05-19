using System;
using System.Collections.Generic;
using System.Text;

namespace Paiwei
{
    /// <summary>
    /// NameSegments contains a list of strings. Chinese will be split by each character.
    /// English and Vietanmese will be split by space.
    /// </summary>
    public class NameSegments
    {
        int chfrom = Convert.ToInt32("4e00", 16);    //range for chinese in unicode（0x4e00～0x9fff）convert to int（chfrom～chend）
        int chend = Convert.ToInt32("9fff", 16);

        public List<Segment> namelists = new List<Segment>();

        /// <summary>
        /// Number of segments.
        /// </summary>
        public int Length
        {
            get { return namelists.Count; }
        }

        /// <summary>
        /// Return a string with all fields
        /// </summary>
        public string FullText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Segment seg in this.namelists)
                {
                    sb.Append(seg.Text + " ");
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// The type of first segement.
        /// </summary>
        public TextType FirstType
        {
            get
            {
                if (this.namelists.Count > 0)
                {
                    return this.namelists[0].Type;
                }
                throw new ArgumentException("namelist is not initialised");
            }
        }

        /// <summary>
        /// The type of segements. If a segement has different types of texts, it is called mixed type.
        /// </summary>
        public TextType Type
        {
            get
            {
                if (namelists.Count == 0)
                {
                    return TextType.NoText;
                }
                else
                {
                    TextType type = this.FirstType;
                    for (int i = 1; i < this.namelists.Count; i++)
                    {
                        if (this.namelists[i].Type != type)
                        {
                            return TextType.Mixed;
                        }
                    }
                    return type;
                }
            }
        }

        public bool IsSegmentsSameType
        {
            get
            {
                int i = 0;
                TextType type = TextType.English; //this initialise is useless,
                foreach (Segment seg in namelists)
                {
                    if (i == 0)
                    {
                        type = seg.Type;
                    }
                    else
                    {
                        if ( type != seg.Type)
                        {
                            return false;
                        }
                    }
                    i++;
                }
                return true;
            }
        }

        public NameSegments(string name)
        {
            name = name.Trim();
            StringBuilder sb = new StringBuilder();
            TextType sbtype = TextType.English;
            for (int i = 0; i < name.Length; i++)
            {
                int code = Char.ConvertToUtf32(name, i);
                string chara = name.Substring(i, 1);
                if (code >= chfrom && code <= chend)
                {
                    sbtype = TextType.Chinese;
                    if (sb.Length > 0)
                    {
                        Segment seg = new Segment(sb.ToString(), sbtype);
                        namelists.Add(seg);
                        sb.Remove(0, sb.Length);
                    }

                    //it is chinese, we add a new string.
                    Segment seg1 = new Segment(chara, TextType.Chinese);
                    namelists.Add(seg1);
                }
                else
                {
                    sbtype = TextType.English;
                    //english or ventimese
                    if (chara.Equals(" "))
                    {
                        Segment seg1 = new Segment(sb.ToString(), sbtype);
                        namelists.Add(seg1);
                        sb.Remove(0, sb.Length);
                    }
                    else
                    {
                        sbtype = TextType.English;
                        sb.Append(chara);
                    }
                }
            }
            if (sb.Length > 0)
            {
                Segment seg1 = new Segment(sb.ToString(), sbtype);
                namelists.Add(seg1);
                sb.Remove(0, sb.Length);
            }
        }
    }

    /// <summary>
    /// A Segement is a line of text to print. It also contains a property for what text it is,
    /// chinese, english or vietneness. This is used to choose font.
    /// Font size are pre-configured in configuration. It is changable in GUI.
    /// </summary>
    public class Segment
    {
        public TextType Type { get; set; }
        public string Text { get; set; }

        public Segment(string text, TextType type)
        {
            this.Text = text;
            this.Type = type;
        }
    }

    public enum TextType
    {
        English,
        Chinese,
        Vietnamese,
        Mixed,
        NoText
    }
}
