using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class DruzynaMap : ClassMap<Druzyna>
    {
        public DruzynaMap()
        {
            Id(x => x.Id);
            Map(x => x.Nazwa);
            References(x => x.Zawodnik1, "Zawodnik1");
            References(x => x.Zawodnik2, "Zawodnik2");
            HasMany(x => x.Tabela);
            Map(x => x.Logo);
            Table("Druzyna");
           
        }
    }
}