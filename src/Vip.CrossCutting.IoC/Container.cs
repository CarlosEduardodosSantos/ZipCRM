
using Ninject;

namespace Vip.CrossCutting.IoC
{
    public class Container
    {
        public Container()
        {
           
        }

        public StandardKernel GetModule()
        {
            return new StandardKernel(new NinjectModulo());
        }
    }
}