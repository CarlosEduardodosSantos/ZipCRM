using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace VipCRM.Data.BusinessAdo
{
    public abstract class DALObject
    {
        // Methods
        protected DALObject()
        {
        }

        public bool AreAggregatesModified(BusinessObject businessObject)
        {
            foreach (PropertyInfo info in this.GetPersistentObjectType().GetProperties())
            {
                if ((info.PropertyType.BaseType == typeof(BusinessObject)) && ((BusinessObject)info.GetValue(businessObject, null)).isModified)
                {
                    return true;
                }
            }
            return false;
        }

        protected abstract void CreateInsertParameters(BusinessObject businessObject, IDbCommand cmd);
        private static IDataParameter CreateInternalParameter(IDbCommand cmd, string paramName, ParameterDirection paramDirection, DbType paramType, object paramValue)
        {
            IDataParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = paramName;
            parameter.DbType = paramType;
            parameter.Direction = paramDirection;
            if (paramDirection == ParameterDirection.Input)
            {
                parameter.Value = paramValue;
            }
            cmd.Parameters.Add(parameter);
            return parameter;
        }

        protected abstract void CreateKeyParameters(BusinessObject businessObject, IDbCommand cmd);
        public static void CreateParameter(IDbCommand cmd, string paramName, DbType paramType, object paramValue)
        {
            CreateInternalParameter(cmd, paramName, ParameterDirection.Input, paramType, paramValue);
        }

        public static void CreateReturnParameter(IDbCommand cmd, string paramName, DbType paramType)
        {
            CreateInternalParameter(cmd, paramName, ParameterDirection.Output, paramType, null);
        }

        protected abstract void CreateUpdateParameters(BusinessObject businessObject, IDbCommand cmd);
        protected abstract void DataReaderToPersistentObject(IDataReader dataReader, BusinessObject businessObject, string radical);
        public void Delete(BusinessObject businessObject, ClientEnvironment clientEnvironment)
        {
            if (businessObject.isRecorded)
            {
                IDbCommand cmd = clientEnvironment.connection.CreateCommand();
                try
                {
                    this.DoBeforeDelete(cmd, businessObject, clientEnvironment);
                    this.DoDelete(cmd, businessObject);
                    this.DoAfterDelete(cmd, businessObject, clientEnvironment);
                }
                catch
                {
                    throw;
                }
            }
        }

        protected virtual void DoAfterDelete(IDbCommand cmd, BusinessObject businessObject, ClientEnvironment clientEnvironment)
        {
        }

        protected virtual void DoAfterSave(IDbCommand cmd, BusinessObject businessObject, ClientEnvironment clientEnvironment)
        {
        }

        protected virtual void DoBeforeDelete(IDbCommand cmd, BusinessObject businessObject, ClientEnvironment clientEnvironment)
        {
        }

        protected virtual void DoBeforeSave(IDbCommand cmd, BusinessObject businessObject, ClientEnvironment clientEnvironment)
        {
        }

        protected void DoDelete(IDbCommand cmd, BusinessObject businessObject)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = this.GetDeleteStatement();
            this.CreateKeyParameters(businessObject, cmd);
            cmd.ExecuteNonQuery();
        }

        protected void DoInsert(IDbCommand cmd, BusinessObject businessObject)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = this.GetInsertStatement();
            this.CreateInsertParameters(businessObject, cmd);
            cmd.ExecuteNonQuery();
        }

        protected void DoUpdate(IDbCommand cmd, BusinessObject businessObject)
        {
            cmd.Parameters.Clear();
            cmd.CommandText = this.GetUpdateStatement();
            this.CreateUpdateParameters(businessObject, cmd);
            cmd.ExecuteNonQuery();
        }

        public bool Exists(string keyString, ClientEnvironment clientEnvironment)
        {
            return (this.Find(keyString, clientEnvironment, false) != null);
        }

        public BusinessObject Find(string keyString, ClientEnvironment clientEnvironment)
        {
            return this.Find(keyString, clientEnvironment, true);
        }

        private BusinessObject Find(string keyString, ClientEnvironment clientEnvironment, bool Throw)
        {
            QueryObject queryObject = (QueryObject)Activator.CreateInstance(this.GetCollectorType());
            queryObject.SetKey(keyString);
            ArrayList instances = this.GetInstances(queryObject, clientEnvironment);
            if (Throw)
            {
                if (instances.Count == 0)
                {
                    throw new Exception("Registro n\x00e3o encontrado");
                }
                if (instances.Count > 1)
                {
                    throw new Exception("Chave duplicada");
                }
            }
            if (instances.Count > 0)
            {
                return (BusinessObject)instances[0];
            }
            return null;
        }

        protected abstract Type GetCollectorType();
        public IDataReader GetDataReader(QueryObject queryObject, SQLUtils handler, ClientEnvironment clientEnvironment)
        {
            queryObject.ModifySQL(handler);
            IDbCommand command = clientEnvironment.connection.CreateCommand();
            command.Parameters.Clear();
            command.CommandText = handler.ToString();
            if (clientEnvironment.transaction != null)
            {
                command.Transaction = clientEnvironment.transaction;
            }
            queryObject.CreateParameters(command);
            return command.ExecuteReader();
        }

        protected abstract string GetDeleteStatement();
        protected abstract string GetEntityName();
        protected abstract string GetInsertStatement();
        public ArrayList GetInstances(QueryObject queryObject, ClientEnvironment clientEnvironment)
        {
            SQLUtils handler = new SQLUtils(this.GetSelectStatement(), clientEnvironment.dbType);
            IDataReader dataReader = this.GetDataReader(queryObject, handler, clientEnvironment);
            ArrayList list = new ArrayList();
            try
            {
                while (dataReader.Read())
                {
                    BusinessObject businessObject = (BusinessObject)Activator.CreateInstance(this.GetPersistentObjectType());
                    businessObject.clientEnvironment = clientEnvironment;
                    this.DataReaderToPersistentObject(dataReader, businessObject, string.Empty);
                    list.Add(businessObject);
                }
            }
            finally
            {
                dataReader.Close();
            }
            return list;
        }

        protected abstract Type GetPersistentObjectType();
        protected abstract string GetSelectStatement();
        protected abstract string GetUpdateStatement();
        public virtual void Save(BusinessObject businessObject, ClientEnvironment clientEnvironment)
        {
            IDbCommand cmd = clientEnvironment.connection.CreateCommand();
            if (businessObject.isModified)
            {
                bool flag = clientEnvironment.transaction != null;
                if (!flag)
                {
                    clientEnvironment.transaction = clientEnvironment.connection.BeginTransaction();
                }
                cmd.Transaction = clientEnvironment.transaction;
                try
                {
                    this.DoBeforeSave(cmd, businessObject, clientEnvironment);
                    if (businessObject.isRecorded)
                    {
                        this.DoUpdate(cmd, businessObject);
                    }
                    else
                    {
                        this.DoInsert(cmd, businessObject);
                    }
                    this.DoAfterSave(cmd, businessObject, clientEnvironment);
                    if (!flag)
                    {
                        cmd.Transaction.Commit();
                        clientEnvironment.transaction = null;
                    }
                }
                catch
                {
                    if (!flag)
                    {
                        cmd.Transaction.Rollback();
                        clientEnvironment.transaction = null;
                    }
                    throw;
                }
                businessObject.isRecorded = true;
                businessObject.isModified = false;
            }
        }
    }
}