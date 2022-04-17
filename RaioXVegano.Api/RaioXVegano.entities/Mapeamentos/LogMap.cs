using FluentNHibernate.Mapping;

namespace RaioXVegano.entities.Mapeamentos
{
    public class LogMap : ClassMap<Log>
    {
        public LogMap() 
        {
            Table("tb_log");
            Id(l => l.Id, "id").GeneratedBy.Identity();
            Map(l => l.ChaveUsuarioLogado, "usuario").Not.Nullable();
            Map(l => l.Data, "data").Not.Nullable();
            Map(l => l.Parametro1, "parametro_1").Not.Nullable();
            Map(l => l.Parametro2, "parametro_2").Not.Nullable();
        }
    }
}
