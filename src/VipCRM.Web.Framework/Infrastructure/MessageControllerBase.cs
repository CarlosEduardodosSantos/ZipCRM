using System.Web.Mvc;
using VipCRM.Web.Framework.UI.toastr;

namespace VipCRM.Web.Framework.Infrastructure
{
    public abstract class MessageControllerBase : Controller
    {
        public MessageControllerBase()
        {
            Toastr = new Toastr();
        }
        public Toastr Toastr { get; set; }

        public ToastMessage AddToastMessage(string title, string message, ToastType toastType)
        {
            return Toastr.AddToastMessage(title, message, toastType);
        }
    }
}