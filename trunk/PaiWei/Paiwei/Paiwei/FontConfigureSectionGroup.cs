using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Paiwei
{
    public class FontConfigureSectionGroup : ConfigurationSection
    {
        /// <summary>
        /// Setup for Chinese font section.
        /// </summary>
        [ConfigurationCollection(typeof(FontSection))]
        [ConfigurationProperty("Chinese", IsRequired = false)]
        public FontSectionCollection ChineseFonts
        {
            get { return this["Chinese"] as FontSectionCollection; }
        }

        /// <summary>
        /// Setup for english font section
        /// </summary>
        [ConfigurationCollection(typeof(FontSection))]
        [ConfigurationProperty("English", IsRequired = false)]
        public FontSectionCollection EnglishFonts
        {
            get { return this["English"] as FontSectionCollection; }
        }

        /// <summary>
        /// setup for vietnamese font section
        /// </summary>
        [ConfigurationCollection(typeof(FontSection))]
        [ConfigurationProperty("Vietnamese", IsRequired = false)]
        public FontSectionCollection VietnameseFonts
        {
            get { return this["Vietnamese"] as FontSectionCollection; }
        }
    }
}
