using System;
using System.Configuration;
using System.IdentityModel.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Web.Hosting;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Sustainsys.Saml2;
using Sustainsys.Saml2.Configuration;
using Sustainsys.Saml2.Owin;
using Sustainsys.Saml2.WebSso;

[assembly: OwinStartup(typeof(OktaSamlSample.Startup))]

namespace OktaSamlSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                AuthenticationMode = AuthenticationMode.Active
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseSaml2Authentication(CreateSaml2Options());
        }

        private static Saml2AuthenticationOptions CreateSaml2Options()
        {
            var applicationBaseUri = new Uri(ConfigurationManager.AppSettings["ApplicationBaseUri"]);
            var saml2BaseUri = new Uri(applicationBaseUri, "saml2");
            var identityProviderIssuer = ConfigurationManager.AppSettings["IdentityProviderIssuer"];
            var identityProviderSsoUri = new Uri(ConfigurationManager.AppSettings["IdentityProviderSsoUri"]);

            var Saml2Options = new Saml2AuthenticationOptions(false)
            {
                SPOptions = new SPOptions
                {
                    EntityId = new EntityId(saml2BaseUri.AbsoluteUri),
                    ReturnUrl = applicationBaseUri
                }
            };

            var identityProvider = new IdentityProvider(new EntityId(identityProviderIssuer), Saml2Options.SPOptions)
            {
                AllowUnsolicitedAuthnResponse = true,
                Binding = Saml2BindingType.HttpRedirect,
                SingleSignOnServiceUrl = identityProviderSsoUri
            };

            identityProvider.SigningKeys.AddConfiguredKey(
                new X509Certificate2(
                    HostingEnvironment.MapPath(
                        "~/App_Data/okta.cert")));

            Saml2Options.IdentityProviders.Add(identityProvider);

            return Saml2Options;
        }
    }
}
