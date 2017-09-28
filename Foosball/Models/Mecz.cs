using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class Mecz
    {
        public virtual int Id { get; set; }
        public virtual Druzyna Druzyna1 { get; set; }
        public virtual Druzyna Druzyna2 { get; set; }
        [Required(ErrorMessage = "Podaj bramki 1 druzyny")]
        public virtual int Druzyna1Bramki { get; set; }
        [Required(ErrorMessage = "Podaj bramki 2 druzyny")]
        public virtual int Druzyna2Bramki { get; set; }
        public virtual string Data { get; set; }


    }
}