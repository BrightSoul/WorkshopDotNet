﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using WorkshopDotNet.Web.Models;
using IdentityManager.Configuration;
using IdentityManager;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentityManager.AspNetIdentity;
using Microsoft.Owin.Security.OAuth;

namespace WorkshopDotNet.Web
{
    public partial class Startup
    {
        // Per ulteriori informazioni sulla configurazione dell'autenticazione, visitare http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configurare il contesto di database, la gestione utenti e la gestione accessi in modo da usare un'unica istanza per richiesta
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            #region cookie authentication
            // Consentire all'applicazione di utilizzare un cookie per memorizzare informazioni relative all'utente connesso
            // e per memorizzare temporaneamente le informazioni relative a un utente che accede tramite un provider di accesso di terze parti
            // Configurare il cookie di accesso
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Consente all'applicazione di convalidare l'indicatore di sicurezza quando l'utente esegue l'accesso.
                    // Questa funzionalità di sicurezza è utile quando si cambia una password o si aggiungono i dati di un account di accesso esterno all'account personale.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)),
                    OnApplyRedirect = ctx =>
                    {
                        var response = ctx.Response;
                        if (!IsApiResponse(ctx.Response))
                        {
                            response.Redirect(ctx.RedirectUri);
                        }
                    }
                },
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Consente all'applicazione di memorizzare temporaneamente le informazioni dell'utente durante la verifica del secondo fattore nel processo di autenticazione a due fattori.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Consente all'applicazione di memorizzare il secondo fattore di verifica dell'accesso, ad esempio il numero di telefono o l'indirizzo e-mail.
            // Una volta selezionata questa opzione, il secondo passaggio di verifica durante la procedura di accesso viene memorizzato sul dispositivo usato per accedere.
            // È simile all'opzione RememberMe disponibile durante l'accesso.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            #endregion

            #region external login
            // Rimuovere il commento dalle seguenti righe per abilitare l'accesso con provider di accesso di terze parti
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            #endregion


            #region Bearer token
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Token"),
                Provider = new ApplicationOAuthProvider("self"),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(oAuthOptions);
            #endregion

            #region IdentityManagerConfiguration
            var factory = new IdentityManagerServiceFactory();

            // In questo specifico esempio, usiamo Entity Framework 
            // e perciò registriamo le classi UserStore<TUser> e 
            // RoleStore<TRole> dal namespace Microsoft.AspNet.Identity.EntityFramework
            factory.IdentityManagerService = new Registration<IIdentityManagerService>(
              resolver =>
              {
                  var userManager =
        new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                  var roleManager =
        new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
                  return new AspNetIdentityManagerService<
        IdentityUser, string, IdentityRole, string>(userManager, roleManager);
              });

            // Creiamo l'oggetto di configurazione
            var managerOptions = new IdentityManagerOptions
            {
                // Consentiamo l'accesso solo dalla macchina locale
                SecurityConfiguration = new LocalhostSecurityConfiguration
                {
                    RequireSsl = false
                },

                // Potremmo decidere di fare a meno dell'interfaccia grafica, 
                // se volessimo sfruttare la Web API sottostante, esposta 
                // su /identitymanager/api
                DisableUserInterface = false,

                //Indichiamo la factory creata in precedenza
                Factory = factory
            };

            // Infine, registriamo il middleware indicando il percorso
            // da cui desideriamo accedere al pannello di gestione
            app.Map("/identitymanager", map =>
            {
                map.UseIdentityManager(managerOptions);
            });

            #endregion

        }
        private static bool IsApiResponse(IOwinResponse response)
        {
            var responseHeader = response.Headers;

            if (responseHeader == null)
                return false;

            return (responseHeader["Suppress-Redirect"] == "True");
        }
    }
}