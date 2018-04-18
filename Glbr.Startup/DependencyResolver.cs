using Glbr.BusinessLogic.Services;
using Glbr.Domain.Contracts.Repositories;
using Glbr.Domain.Contracts.Services;
using Glbr.Domain.Entities;
using Glbr.Infra.DataContexts;
using Glbr.Infra.Repositories;
using Unity;
using Unity.Lifetime;

namespace Glbr.Startup
{
    public class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<GlbrDataContext, GlbrDataContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());

            container.RegisterType<User, User>(new HierarchicalLifetimeManager());
        }
    }
}
