﻿using System;

namespace VipCRM.Data.QueryObject
{
    public class OcorrenciaQueryObject : BusinessAdo.QueryObject
    {
        public OcorrenciaQueryObject()
            :base(String.Empty)
        {
            
        }
        public override void CreateParameters(System.Data.IDbCommand command)
        {
            throw new System.NotImplementedException();
        }

        public override void SetKey(BusinessAdo.BusinessObject businessObject)
        {
            throw new System.NotImplementedException();
        }

        public override void SetKey(string keyString)
        {
            throw new System.NotImplementedException();
        }
    }
}