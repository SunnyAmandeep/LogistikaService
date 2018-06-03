using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Logistika.Service.Common.Ioc;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;


namespace Logistika.Service.Common.IoC
{
   
    public class ComponetRegistrator
    {
        private const string _dataAccessInterfacePattern = "Logistika.Service.*.DataAccessInterface";
        private const string _businessInterfacePattern = "Logistika.Service.*.BusinessComponentInterface"; 


        private IWindsorContainer container;

        public ComponetRegistrator(IWindsorContainer container)
        {
            this.container = container;
        }

        public void Register()
        {
            var assemblies = GetBusinessInterfaceAssemblies();
            foreach (var interfaceAssembly in assemblies)
            {
                
                Assembly businessImplementation = GetBusinessImplementation(interfaceAssembly);
                RegisterTypes(interfaceAssembly, businessImplementation);

                Assembly dataInterface = GetDataInterface(interfaceAssembly);

                Assembly dataImplementation = GetDataImplementation(interfaceAssembly);
                RegisterTypes(dataInterface, dataImplementation);
            }
        }

        private Assembly GetBusinessInterface(Assembly interfaceAssembly)
        {
            return GetAssembly(interfaceAssembly, ".BusinessComponentInterface");
        }

        private Assembly GetBusinessImplementation(Assembly interfaceAssembly)
        {
            return GetAssembly(interfaceAssembly, ".BusinessComponent");
        }

        private Assembly GetDataInterface(Assembly interfaceAssembly)
        {
            return GetAssembly(interfaceAssembly, ".DataAccessInterface");
        }

        private Assembly GetDataImplementation(Assembly interfaceAssembly)
        {
            return GetAssembly(interfaceAssembly, ".DataAccess");
        }

        private Assembly[] GetBusinessInterfaceAssemblies()
        {
            return GetAssemlies(_businessInterfacePattern);
        }
                

        private Assembly GetAssembly(Assembly interfaces, string shortName)
        {
            string name = interfaces.GetName().Name.Replace(".BusinessComponentInterface", shortName);
            return AssemblyHelper.GetAvailableAssemblies().Where(a => a.GetName().Name == name).First();

        }

        private Assembly[] GetAssemlies(string match)
        {

            return AssemblyHelper.GetAvailableAssemblies().Where(a => Regex.IsMatch(a.GetName().Name, match)).ToArray();
        }

        private void RegisterTypes(Assembly interfaces, Assembly implementation)
        {
            RegisterTypes(interfaces, implementation, false);
        }

        private void RegisterTypes(Assembly interfaces, Assembly implementation, bool registerComponents)
        {
            var lst = (from t in interfaces.GetTypes()
                       where t.IsInterface
                       select t).ToList();

            foreach (var t in lst)
            {
                Type impl = implementation.GetTypes().Where(type => type.GetInterfaces().Where(x => x == t).Count() > 0).First();
                container.Register(Component.For(t).ImplementedBy(impl).LifeStyle.Transient);
                if (registerComponents)
                {
                    container.AddComponent(impl.Name, impl);
                }
            }
        }

    }
}
