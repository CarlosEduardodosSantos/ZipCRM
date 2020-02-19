using System.Security.Principal;
using System.Web.Security;

namespace VipCRM.Web.Infrastructure
{
    public class UserIdentity : IIdentity, IPrincipal
    {
        private readonly FormsAuthenticationTicket _ticket;
        public UserIdentity(FormsAuthenticationTicket ticket)
        {
            _ticket = ticket;
        }
        public string AuthenticationType
        {
            get { return "User"; }
        }

        public bool IsAuthenticated
        {
            get { return true; }
        }

        public string Name
        {
            get { return _ticket.Name; }
        }
        public string UsuarioId
        {
            get { return _ticket.UserData; }
        }

        public IIdentity Identity
        {
            get { return this; }
        }

        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(role);
        }
    }
}