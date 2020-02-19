using System;
using System.Collections.Generic;

namespace VipCRM.Application.MVC.ViewModels
{
    public class RootObjeto
    {
        public int TotalPage { get; set; }
        public int Page { get; set; }
        public List<object> Results { get; set; }
    }
}