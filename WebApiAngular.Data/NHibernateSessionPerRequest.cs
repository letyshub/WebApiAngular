using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;
using System.Web;

namespace WebApiAngular.Data
{
    public class NHibernateSessionPerRequest : IHttpModule
    {
        private static readonly ISessionFactory _sessionFactory;

        static NHibernateSessionPerRequest()
        {
            _sessionFactory = CreateSessionFactory();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard
                    .ConnectionString(c => c
                        .FromConnectionStringWithKey("MySqlConnection")))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(c =>
                {
                    BuildSchema(c);
                    c.Properties[NHibernate.Cfg.Environment.CurrentSessionContextClass] = "web";
                })
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration cfg)
        {
            // uncomment this code if you want create database
            //new SchemaExport(cfg)
            //     .Create(false, true);


            new SchemaUpdate(cfg);
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose() { }

        public static ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = _sessionFactory.OpenSession();

            session.BeginTransaction();

            CurrentSessionContext.Bind(session);
        }

        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);

            if (session == null) return;

            try
            {
                session.Transaction.Commit();
            }
            catch (Exception ex)
            {
                session.Transaction.Rollback();
            }
            finally
            {
                session.Close();
                session.Dispose();
            }
        }
    }
}
