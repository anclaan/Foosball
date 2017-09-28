using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class MeczMap : ClassMap<Mecz>
    {
        public MeczMap()
        {
            Id(x => x.Id);
            References(x => x.Druzyna1, "Druzyna1");
            References(x => x.Druzyna2, "Druzyna2");
            Map(x => x.Druzyna1Bramki);
            Map(x => x.Druzyna2Bramki);
            Map(x => x.Data);

            Table("Mecz");
        }
    }
}