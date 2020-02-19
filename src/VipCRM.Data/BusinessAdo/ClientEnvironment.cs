using System.Data;

namespace VipCRM.Data.BusinessAdo
{
    public class ClientEnvironment
    {
        // Fields
        private IDbConnection _connection;
        private DataBaseType _dbType;
        private IDbTransaction _transaction;

        // Methods
        public ClientEnvironment(DataBaseType tipo)
        {
            this.dbType = tipo;
        }

        // Properties
        public IDbConnection connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
            }
        }

        public DataBaseType dbType
        {
            get
            {
                return this._dbType;
            }
            set
            {
                this._dbType = value;
            }
        }

        public IDbTransaction transaction
        {
            get
            {
                return this._transaction;
            }
            set
            {
                this._transaction = value;
            }
        }

    }
}