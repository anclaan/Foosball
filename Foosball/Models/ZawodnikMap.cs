using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class ZawodnikMap : ClassMap<Zawodnik>
    {
        public ZawodnikMap()
        {
            Id(x => x.Id);
            Map(x => x.Imie);
            Map(x => x.Nazwisko);
            References(x => x.IdUzytkownik, "IdUzytkownik");

            //Table("Zawodnik");
        }
    }
}