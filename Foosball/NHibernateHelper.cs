using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Foosball.Models;
using Microsoft.AspNet.Identity;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;

namespace Foosball
{
    public class NHibernateHelper

    {

        //public static ISession OpenSession()
        //{
        //    var cfg = new StoreConfiguration();
        //    ISessionFactory sessionFactory = Fluently.Configure()
        //        .Database(MsSqlConfiguration.MsSql2012
        //            .ConnectionString(@"Data Source=L0121\SQLEXPRESS;Initial Catalog=FoosballDataBase;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        //                        .ShowSql()
        //        )
        //        .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Zawodnik>(cfg).Convections))
        //        .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Mecz>(cfg)))
        //       .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Druzyna>(cfg)))
        //       .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Tabela>(cfg)))


        //        .Mappings(m =>
        //                   m.FluentMappings
        //                        .AddFromAssemblyOf<Zawodnik>()).Mappings(m =>
        //                   m.FluentMappings
        //                        .AddFromAssemblyOf<Druzyna>()).Mappings(m =>
        //                   m.FluentMappings
        //                        .AddFromAssemblyOf<Tabela>())
        //        .ExposeConfiguration(cfg => new SchemaExport(cfg)
        //                                            .Create(false, false))

        //        .BuildSessionFactory();
        //    return sessionFactory.OpenSession();

        //}



        //public static ISession OpenSession()
        //{
        //    var cfg = new StoreConfiguration();
        //    ISessionFactory sessionFactory = Fluently.Configure()
        //        .Database(MsSqlConfiguration.MsSql2012
        //        .ConnectionString(@"Data Source=L0121\SQLEXPRESS;database=FoosballDataBase;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False").ShowSql())
        //        .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Zawodnik>(cfg)
        //        .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Mecz>(cfg)

        //        .Override<Zawodnik>(map => { map.IgnoreProperty(x => x.IN); })
        //        .IgnoreBase<DruzynaMap>



        //        )).Mappings(m =>
        //                  m.FluentMappings
        //                        .AddFromAssemblyOf<Zawodnik>())
        //            .Mappings(m =>
        //                  m.FluentMappings
        //                        .AddFromAssemblyOf<Druzyna>())
        //            .Mappings(m =>
        //                  m.FluentMappings
        //                        .AddFromAssemblyOf<Mecz>())
        //        //.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Tabela>())
        //        //.Mappings(m => m.FluentMappings.AddFromAssemblyOf<Ksiazka>())
        //        //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
        //        .BuildSessionFactory();
        //    return sessionFactory.OpenSession();
        //}


        public static ISession OpenSession()

        {


            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                  .ConnectionString(@"Data Source=L0121\SQLEXPRESS;Initial Catalog=FoosballDataBase;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                              .ShowSql()
                )
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Zawodnik>())
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Tabela>())
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Uzytkownik>())

                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                .Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();

        }


    }
}