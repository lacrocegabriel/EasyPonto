using Dev.Business.Core.Notifications;
using Dev.Business.Models.Funcionarios;
using Dev.Business.Models.Funcionarios.Services;
using Dev.Business.Models.Pontos;
using Dev.Business.Models.Pontos.Services;
using Dev.Infra.Data.Context;
using Dev.Infra.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System.Reflection;
using System.Web.Mvc;

namespace Dev.EasyPonto
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void InitializeContainer(Container container)
        {

            container.Register<EasyPontoDbContext>(Lifestyle.Scoped);
            container.Register<IPontoRepository, PontoRepository>(Lifestyle.Scoped);
            container.Register<IPontoService, PontoService>(Lifestyle.Scoped);
            container.Register<IFuncionarioRepository, FuncionarioRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<IFuncionarioService, FuncionarioService>(Lifestyle.Scoped);
            container.Register<INotificador, Notificador>(Lifestyle.Scoped);

            container.RegisterSingleton(() => AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));
            
        }

    }
}