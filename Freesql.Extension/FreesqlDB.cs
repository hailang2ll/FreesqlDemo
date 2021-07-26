namespace Freesql.Service
{
    public class FreesqlDB
    {

        public static IFreeSql Fsql = null;
        public static IFreeSql CreateInstance(string sqlstr = "")
        {
            if (Fsql == null)
            {
                if (string.IsNullOrWhiteSpace(sqlstr))
                {
                    sqlstr = System.Configuration.ConfigurationManager.AppSettings["Database.ConnectionString"];
                }
                Fsql = new FreeSql.FreeSqlBuilder()
                  .UseConnectionString(FreeSql.DataType.SqlServer, sqlstr)
                  .Build();
            }
            return Fsql;
        }


    }




}
