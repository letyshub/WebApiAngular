using FluentNHibernate.Mapping;
using WebApiAngular.Domain;

namespace WebApiAngular.Data.Map
{
    public class ClientMap : ClassMap<Contact>
    {
        public ClientMap()
        {
            Table("waa_tb_clients");

            Id(x => x.Id)
              .Column("cli_id")
              .GeneratedBy.Guid();

            Map(x => x.Firstname)
              .Column("cli_firstname")
              .CustomType(typeof(string))
              .Length(255)
              .Not.Nullable();

            Map(x => x.Secondname)
              .Column("cli_secondname")
              .CustomType(typeof(string))
              .Length(255)
              .Not.Nullable();

            Map(x => x.Email)
              .Column("cli_email")
              .CustomType(typeof(string))
              .Length(255)
              .Not.Nullable();

            Map(x => x.Phone)
              .Column("cli_phone")
              .CustomType(typeof(string))
              .Length(255)
              .Not.Nullable();
        }
    }
}
