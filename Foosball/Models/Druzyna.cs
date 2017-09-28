using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class Druzyna
    {
        public virtual int Id { get; set; }
        [Required(ErrorMessage = "Podaj nazweDruzyny")]
        public virtual string Nazwa { get; set; }
        public virtual Zawodnik Zawodnik1 { get; set; }
        public virtual Zawodnik Zawodnik2 { get; set; }
        public virtual string Logo { get; set; }
        public virtual IEnumerable<Tabela> Tabela { get; set; }

    }
}
