namespace VipCRM.Application.MVC.Interface
{
    public interface IAppServiceBase
    {
        void BeginTransaction();
        void Commit(); 
    }
}