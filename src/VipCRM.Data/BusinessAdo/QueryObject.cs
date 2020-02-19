using System;
using System.Data;

namespace VipCRM.Data.BusinessAdo
{
    public abstract class QueryObject
    {
        // Fields
        private string _alias;
        private int _maxRecords;
        private string _prefix;
        private bool _useMaxRecords;

        // Methods
        public QueryObject(string alias)
        {
            this.SetAlias(alias);
        }

        public abstract void CreateParameters(IDbCommand command);
        public virtual void ModifySQL(SQLUtils handler)
        {
            if (this.useMaxRecords)
            {
                handler.handleSQLAppendSelect(string.Format("top {0} ", this.maxRecords), "_TOP");
            }
        }

        private void SetAlias(string value)
        {
            if ((value.IndexOf(" ") != -1) || (value.IndexOf(".") != -1))
            {
                throw new EAliasError("Valor inv\x00e1lido para propriedade Alias.");
            }
            this._alias = value;
            if (this._alias.Trim().Length > 0)
            {
                this._prefix = this._alias + ".";
            }
            else
            {
                this._prefix = string.Empty;
            }
        }

        public abstract void SetKey(BusinessObject businessObject);
        public abstract void SetKey(string keyString);

        // Properties
        public string alias
        {
            get
            {
                return this._alias;
            }
        }

        public int maxRecords
        {
            get
            {
                return this._maxRecords;
            }
            set
            {
                this._maxRecords = value;
            }
        }

        public string prefix
        {
            get
            {
                return this._prefix;
            }
        }

        public bool useMaxRecords
        {
            get
            {
                return this._useMaxRecords;
            }
            set
            {
                this._useMaxRecords = value;
            }
        }
    }
    internal class EAliasError : SystemException
    {
        // Methods
        public EAliasError(string msg)
            : base(msg)
        {
        }
    }
}