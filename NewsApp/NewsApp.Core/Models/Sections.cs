using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.Core.Models
{
    public class Sections
    {
        Dictionary<string, string> _sections;

        public Sections()
        {
            _sections = new Dictionary<string, string>()
            {
                { "Main","search" },
                { "Sport","sport" },
                { "Politics","politics" },
                { "Technology","technology" },
                { "Business","business" },
            };

        }

        public List<string> GetSectionNames()
        {
            return _sections.Select((sectionItem) => sectionItem.Key).ToList();
        }

        public List<string> GetSectionValues()
        {
            return _sections.Select((sectionItem) => sectionItem.Value).ToList();
        }

        public string GetSectionByKey(string key)
        {
            return _sections[key];
        }
    }
}
