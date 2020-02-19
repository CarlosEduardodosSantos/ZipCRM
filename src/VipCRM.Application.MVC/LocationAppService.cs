using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using VipCRM.Application.MVC.Interface;
using VipCRM.Application.MVC.ViewModels;

namespace VipCRM.Application.MVC
{
    public class LocationAppService : ILocationAppService
    {
        public RootObject ReverseGeocode(double latitude, double longitude)
        {
            var queryString = $"https://nominatim.openstreetmap.org/reverse?format=jsonv2&lat={latitude.ToString().Replace(",", ".")}&lon={longitude.ToString().Replace(",", ".")}";

            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");

            var jsonData = webClient.DownloadData(queryString);

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
            var rootObject = ser.ReadObject(new MemoryStream(jsonData));
            return (RootObject)rootObject;
        }
    }
}