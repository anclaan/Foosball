using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class Zawodnik
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Podaj imie")]
        public virtual string Imie { get; set; }
        [Required(ErrorMessage = "Podaj nazwisko")]
        public virtual string Nazwisko { get; set; }
       // public virtual int IdUzytkownik { get; set; }
        public virtual string IN { get { return Imie + " " + Nazwisko; } }
        public virtual Uzytkownik IdUzytkownik { get; set; }
        //public virtual IList<Druzyna> Druzyna { get; set; }
    }
}