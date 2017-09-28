using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class TabelaMap : ClassMap<Tabela>
    {
        public TabelaMap()
        {
            Id(x => x.Id);
            References(x => x.Druzyna, "Druzyna");
            Map(x => x.Punkty);
            Map(x => x.MeczeRozegrane);
            Map(x => x.MeczeWygrane);
            Map(x => x.MeczePrzegrane);
            Map(x => x.MeczeZremisowane);
            Map(x => x.BramkiStrzelone);
            Map(x => x.BramkiStracone);

            Table("Tabela");
        }
       
    }
}