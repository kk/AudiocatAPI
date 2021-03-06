﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Audiocat.Models
{
    public class AudioItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Timestamp { get; set; }
        public string File { get; set; }
        public string Author { get; set; }
    }
}
