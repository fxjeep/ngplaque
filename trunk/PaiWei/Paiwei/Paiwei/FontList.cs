using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Paiwei
{
	public class FontListSectionGroup : ConfigurationSection
	{
		[ConfigurationCollection(typeof(FontSection))]
		[ConfigurationProperty("Font", IsRequired = true)]
		public FontSectionCollection Fonts
		{
			get { return this["Font"] as FontSectionCollection; }
		}
	}

	public class FontSection : ConfigurationElement
	{
		[ConfigurationProperty("Name", IsRequired = true)]
		public string Name
		{
			get { return this["Name"] as string; }
			set { this["Name"] = value; }
		}

		[ConfigurationProperty("File", IsRequired = true)]
		public string File
		{
			get { return this["File"] as string; }
		}

		public override string ToString()
		{
			return Name;
		}


	}

	public class FontSectionCollection : ConfigurationElementCollection
	{
		public FontSection this[int index]
		{
			get
			{
				return base.BaseGet(index) as FontSection;
			}
			set
			{
				if (base.BaseGet(index) != null)
					base.BaseRemoveAt(index);
				this.BaseAdd(index, value);
			}
		}

		protected override string ElementName { get { return "Font"; } }

		protected override ConfigurationElement CreateNewElement()
		{
			return new FontSection();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((FontSection)element).Name;
		}

        [ConfigurationProperty("MainSize", IsRequired = true)]
        public decimal MainSize
        {
            get { 
                decimal dd = (decimal)this["MainSize"];
                return dd;
            }
            set { this["MainSize"] = value; }
        }

        [ConfigurationProperty("SideSize", IsRequired = true)]
        public decimal SideSize
        {
            get
            {
                decimal dd = (decimal)this["SideSize"];
                return dd;
            }
            set { this["SideSize"] = value; }
        }
	}
}
