﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deezer.Entities
{
    public class Album
    {
        public int Id { get; set; }
        public string  Title { get; set; }
        public string CoverBig { get; set; }
        //public Artist Artist { get; set; }
        public List<Track> Tracks { get; set; }

        public Album()
        {
            Tracks = new List<Track>();
        }
    }
}
