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
        public int Id { get; set; }
        public string Parents { get; set; }
        public string Title { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
    }
}