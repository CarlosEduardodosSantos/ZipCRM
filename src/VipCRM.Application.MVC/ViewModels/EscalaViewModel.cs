using System;

namespace VipCRM.Application.MVC.ViewModels
{
    public class EscalaViewModel
    {
        public int EscalaID { get; set; }
        public DateTime DataEscala { get; set; }
        public string Data => DataEscala.ToString("yyyy-MM-dd");
        public string Func1 { get; set; }
        public string Func2 { get; set; }
        public string Func3 { get; set; }
        public string Func4 { get; set; }
        public string Func5 { get; set; }
    }
}