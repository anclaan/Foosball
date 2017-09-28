using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class Tabela
    {
        public virtual int Id { get; set; }
        public virtual Druzyna Druzyna { get; set; }
        public virtual int Punkty { get; set; }
        public virtual int MeczeRozegrane { get; set; }
        public virtual int MeczeWygrane { get; set; }
        public virtual int MeczePrzegrane { get; set; }
        public virtual int MeczeZremisowane { get; set; }
        public virtual int BramkiStrzelone { get; set; }
        public virtual int BramkiStracone { get; set; }
    }
}