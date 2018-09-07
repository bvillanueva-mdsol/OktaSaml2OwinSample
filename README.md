# OktaSaml2OwinSample

## How to run sample

- Create new application integration inside Okta
  - Follow this [guide](todo) to create new application integration
    - Please use the value of ApplicationBaseUri in [Web.Config](https://github.com/bvillanueva-mdsol/OktaSaml2OwinSample/blob/master/OktaSamlSample/Web.config) as your base uri in the registration
  - Get the following information from the newly created integration
    - Identity Provider Single Sign-On URL
    - Identity Provider Issuer
    - X.509 Certificate
- Run sample
  - Clone this repo
  - Open in Visual Studio 2017 or any IDE available
  - Open Web.config under OktaSamlSample project
    - Update IdentityProviderIssuer value with Identity Provider Issuer
    - Update IdentityProviderSsoUri value with Identity Provider Single Sign-On URL
  - Open okta.cert under App_data directory of OktaSamlSample project
    - Update contents of okta.cert with X.509 Certificate
  - Build and Run
