using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using SerhatYas.Business.Abstract;
using SerhatYas.Business.Concrete;
using SerhatYas.Core.Utilities.Interceptors;
using SerhatYas.DataAccess.Abstract;
using SerhatYas.DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Text;
namespace SerhatYas.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {


            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserProjectMapManager>().As<IUserProjectMapService>();
            builder.RegisterType<EfUserProjectMapDal>().As<IUserProjectMapDal>();

            builder.RegisterType<ProjectManager>().As<IProjectService>();
            builder.RegisterType<EfProjectDal>().As<IProjectDal>();

           

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                        .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                        {
                            Selector = new AspectInterceptorSelector()
                        }).SingleInstance();
        }
    }
}
