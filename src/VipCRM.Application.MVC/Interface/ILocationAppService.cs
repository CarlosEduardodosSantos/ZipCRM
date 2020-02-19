using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC.Interface
{
    public interface ILocationAppService
    {
        RootObject ReverseGeocode(double latitude, double longitude);

    }
}