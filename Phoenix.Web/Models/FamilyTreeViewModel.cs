using Phoenix.Web.Infrastructure.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Phoenix.Web.Models
{
    [Bind(Exclude = "UniqueKey")]
    public class FamilyTreeViewModel
    {
        // lower case required for the receiving ajax call
        public int id { get; set; }
        public string parents { get; set; }
        public string title { get; set; }
        public string label { get; set; }
        public string description { get; set; }
    }
}