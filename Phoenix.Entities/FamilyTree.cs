﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Entities
{
    public class FamilyTree
    {
        public int Id { get; set; }
        public int seqId { get; set;  }
        public string Parents { get; set; }
        public string Spouses { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public bool Deceased { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime FirstRegisteredDate { get; set; }

    }
}
