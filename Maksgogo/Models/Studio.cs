using System;
using System.Collections.Generic;

namespace Maksgogo
{
    public partial class Studio
    {
        public Studio()
        {
            Films = new HashSet<Film>();
        }

        public int IdStudio { get; set; }
        public string? StudioName { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}
