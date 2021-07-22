using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Deezer.Entities
{
    public class Track
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Preview { get; set; }
        //public Album Album { get; set; }

        public Track() { }
    }
}