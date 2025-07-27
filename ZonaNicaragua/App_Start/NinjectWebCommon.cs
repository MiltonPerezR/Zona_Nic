using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZonaNicaragua.Models;
using Ninject;
using Ninject.Web.Common;

namespace ZonaNicaragua.App_Start
{
    public class NinjectWebCommon
    {
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IUsuarioRepositorio<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind<AppDbContext>().ToSelf().InRequestScope();
        }


    }
}