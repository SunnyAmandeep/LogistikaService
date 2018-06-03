using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Logistika.Service.Common.BusinessComponentInterface.Logger;
using Logistika.Service.Common.BusinessComponentInterface.User;
using Logistika.Service.Common.ExceptionHelper;
using Logistika.Service.Common.IoC;
using Logistika.Service.Common.Log;
using Logistika.Service.Providers;
using Logistika.Service.Providers.Filter;
using Logistika.Service.Providers.Handler;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup("KFISWebApiStartup", typeof(Logistika.Service.Startup))]

namespace Logistika.Service
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }

        IUserBusinessComponent _authenticationBusinessComponent = null;
        private readonly IWindsorContainer _container;

        public Startup()
        {
            this._container = new WindsorContainer();
            //  GlobalConfiguration.Configuration.SuppressDefaultHostAuthentication();
        }


        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            //AreaRegistration.RegisterAllAreas();
            // throw new Exception("Test Start up check");
            WebApiConfig.Register(config);
            this.RegisterDependencyResolver(config);
            ConfigureOAuth(app);
            var logger = (ILoggerBusinessComponent)_container.Resolve(typeof(ILoggerBusinessComponent));
            _authenticationBusinessComponent = (IUserBusinessComponent)_container.Resolve(typeof(IUserBusinessComponent));
            //config.MessageHandlers.Add(new CorsHandler(_authenticationBusinessComponent));
            config.MessageHandlers.Add(new ApiLogHandler(logger));
            //CorsPolicies = GetCorsPoliciesfromConfiguration(); 
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
            config.Filters.Add(new ValidateModelAttribute());
            //config.Filters.Add(new CheckModelForNullAttribute());
            config.Filters.Add(new CheckContextErrorAttribute());
            config.Filters.Add(new GlobalExceptionFilterAttribute(logger));
            config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new System.Web.Http.AuthorizeAttribute());
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));
            //var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            //jsonFormatter.UseDataContractJsonSerializer = true;
            //jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            ExceptionHelper.Log = logger.LogSystemError;
            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.UseXmlSerializer = true;
            //new Uri(Application.StartupPath + "\\log4net.config")
            log4net.Config.XmlConfigurator.Configure();

        }

        private void RegisterDependencyResolver(HttpConfiguration config)
        {
            config.DependencyResolver = new WindsorDependencyResolver(this._container.Kernel);
            ComponetRegistrator registrator = new ComponetRegistrator(_container);
            this._container.Install(FromAssembly.This());
            registrator.Register();
            this._container.Kernel.Resolver.AddSubResolver(new LoggerResolver());
            this._container.Register(Component.For<IAppLogger>()
                                   .ImplementedBy<AppLogger>()
                                   .LifeStyle.Transient);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();
            _authenticationBusinessComponent = (IUserBusinessComponent)_container.Resolve(typeof(IUserBusinessComponent));
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(3),
                Provider = new SimpleAuthorizationServerProvider(_authenticationBusinessComponent),
                RefreshTokenProvider = new SimpleRefreshTokenProvider(_authenticationBusinessComponent),

            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}