using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class Uzytkownik
    {
        public virtual int Id { get; set; }
        [Required (ErrorMessage = "Podaj login")]
        public virtual string Login { get; set; }
        [Required (ErrorMessage ="Pole hasło")]
        public virtual string Password { get; set; }
        public virtual string Email { get; set; }
        public virtual string Rola { get; set; }
                                           //  public virtual int Rola { get; internal set; }
    }
}