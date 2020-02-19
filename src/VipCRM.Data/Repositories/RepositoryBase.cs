using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VipCRM.Data.Repositories
{
    public abstract  class RepositoryBase
    {
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["VipCRMConnection"].ConnectionString);
            }
        }
        public abstract string GetSelectBasic();
        public abstract string GetUpdateBasic();
        public abstract string GetInsertBasic();
    }
}