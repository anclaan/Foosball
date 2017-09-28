using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Foosball.Models
{
    public class UzytkownikMap : ClassMap<Uzytkownik>
    {
        public UzytkownikMap()
        {
            Id(x => x.Id);
            Map(x => x.Login);
            Map(x => x.Password);
            Map(x => x.Email);
            Map(x => x.Rola);
            Table("Uzytkownik");
        }
    }
}