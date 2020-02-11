using System;
using System.Collections.Generic;
using System.Text;

namespace KNews.Core.Entities
{
    public class ProfileRibbonPrefs
    {
        public long ProfileID { get; set; }

        public List<string> ExcludeTags { get; set; }
        public List<long> ExcludeAuthors { get; set; }
        public List<long> ExcludePosts { get; set; }
    }
}
