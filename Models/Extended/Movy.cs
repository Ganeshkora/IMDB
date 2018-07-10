using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMDB.com.Models.Extended
{
    public class Movy
    {
    }
    public class MoviesMetadata
    {
        [Required (AllowEmptyStrings =false,ErrorMessage ="Please Enter Movie Name")]
        public string MovieName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please Enter Movie Name")]
        public string Cast { get; set; }
    }
}