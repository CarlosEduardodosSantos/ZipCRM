using System;

namespace VipCRM.Data.BusinessAdo
{
    public abstract class BusinessObject
    {
        // Fields
        private ClientEnvironment _clientEnvironment;
        private bool _initializing;
        private bool _isModified;
        private bool _isProxy;
        private bool _isRecorded;

        // Methods
        protected BusinessObject()
        {
        }

        public void BeginInit()
        {
            this._initializing = true;
        }

        public void Delete(ClientEnvironment clientEnvironment)
        {
            this.GetDAL().Delete(this, clientEnvironment);
        }

        public void EndInit()
        {
            this._initializing = false;
        }

        public abstract DALObject GetDAL();
        public abstract string GetKeyString();
        public abstract string GetObjectDescription();
        public void Save(ClientEnvironment clientEnvironment)
        {
            this.GetDAL().Save(this, clientEnvironment);
        }

        protected void SetIfChanged(ref bool field, bool value)
        {
            if (field != value)
            {
                this.isModified = true;
                field = value;
            }
        }

        protected void SetIfChanged(ref DateTime field, DateTime value)
        {
            if (field != value)
            {
                this.isModified = true;
                field = value;
            }
        }

        protected void SetIfChanged(ref decimal field, decimal value)
        {
            if (field != value)
            {
                this.isModified = true;
                field = value;
            }
        }

        protected void SetIfChanged(ref double field, double value)
        {
            if (field != value)
            {
                this.isModified = true;
                field = value;
            }
        }

        protected void SetIfChanged(ref int field, int value)
        {
            if (field != value)
            {
                this.isModified = true;
                field = value;
            }
        }

        protected void SetIfChanged(ref string field, string value)
        {
            if (field != value)
            {
                this.isModified = true;
                field = value;
            }
        }

        public abstract void SetKey(string keyString);

        // Properties
        public bool areAggregatesModified
        {
            get
            {
                return this.GetDAL().AreAggregatesModified(this);
            }
        }

        public ClientEnvironment clientEnvironment
        {
            get
            {
                return this._clientEnvironment;
            }
            set
            {
                this._clientEnvironment = value;
            }
        }

        public bool initializing
        {
            get
            {
                return this._initializing;
            }
        }

        public bool isModified
        {
            get
            {
                return this._isModified;
            }
            set
            {
                this._isModified = value;
            }
        }

        public bool isProxy
        {
            get
            {
                return this._isProxy;
            }
            set
            {
                this._isProxy = value;
            }
        }

        public bool isRecorded
        {
            get
            {
                return this._isRecorded;
            }
            set
            {
                this._isRecorded = value;
            }
        }

        public string keyString
        {
            get
            {
                return this.GetKeyString();
            }
        }

        public string keyStringCrypt
        {
            get
            {
                return EncryptUtils.Crypt("key=" + this.GetKeyString() + " " + DateTime.Now.ToShortDateString(), 3);
            }
        }
    }
}