using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Paiwei
{
	/// <summary>
	/// The class that loads DocType element in App.config
	/// </summary>
	public class DocTypeConfigureSetction : ConfigurationSection
	{
		#region Fields
		private static ConfigurationPropertyCollection s_properties;
		private static ConfigurationProperty s_propTypes;
		#endregion

		public DocTypeConfigureSetction()
		{
			s_propTypes = new ConfigurationProperty(
					"Type",
					typeof(DocTypeCollection),
					null,
					ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsDefaultCollection
					);
			s_properties = new ConfigurationPropertyCollection();
			s_properties.Add(s_propTypes);
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return s_properties;
			}
		}

		public DocTypeCollection Types
		{
			get { return (DocTypeCollection)base[s_propTypes]; }
		}
	}

	/// <summary>
	/// The class that loads Type element in App.config
	/// </summary>
	public class DocTypeElement : ConfigurationElement
	{
		#region Fields
		private static ConfigurationPropertyCollection s_properties;
		private static ConfigurationProperty s_propName;
		private static ConfigurationProperty s_propImage;
		#endregion

		public DocTypeElement()
		{
			s_propName = new ConfigurationProperty(
				"Name",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsRequired
				);

			s_propImage = new ConfigurationProperty(
				"Image",
				typeof(string),
				null,
				ConfigurationPropertyOptions.IsRequired
				);

			s_properties = new ConfigurationPropertyCollection();

			s_properties.Add(s_propName);
			s_properties.Add(s_propImage);
		}

		public string Name
		{
			get { return base[s_propName] as string; }
			set { base[s_propName] = value; }
		}

		public string Image
		{
			get { return base[s_propImage] as string; }
			set { base[s_propImage] = value; }
		}

		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return s_properties;
			}
		}

		public override string ToString()
		{
			return Name;
		}
	}

	/// <summary>
	/// Collection of DocType elements
	/// </summary>
	public class DocTypeCollection : ConfigurationElementCollection
	{
		/*public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}*/

		protected override string ElementName 
		{	
			get { 
				return "Type"; 
			} 
		}

		public DocTypeElement this[int index]
		{
			get
			{
				return (DocTypeElement)base.BaseGet(index);
			}
			set
			{
				if (base.BaseGet(index) != null)
				{
					base.BaseRemoveAt(index);
				}
				base.BaseAdd(index, value);
			}
		}

		protected override ConfigurationElement CreateNewElement()
		{
			return new DocTypeElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as DocTypeElement).Name;
		}
	}	
}
