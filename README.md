# OktaSaml2OwinSample

## How to run sample

- Create new application integration inside Okta
  - Follow this [guide](#how-to-create-new-application-integration-inside-okta) to create new application integration
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

## How to create new application integration inside Okta

- Prerequisite: Need to have an Okta server, sign-up [here](https://developer.okta.com/signup/) to get a test server
- Create new application integration
  - Sign in to your Okta and click `Admin` button
  - Switch to `Classic UI`
  - Click `Application` Menu
  - Click `Add Application`
  - Click `Create New App`
  - Choose Platform: `Web`, Sign on method: `SAML 2.0`
  - Type in App name (any app name would do), click `Next`
  - Type in these information:
    - Single sign on URL: {base uri (e.g. http://localhost:2687}/saml2/acs
    - Audience URI (SP Entity ID): {base uri (e.g. http://localhost:2687}/saml2
    - Name ID format: x509SubjectName
    - Attribute Statements:
      - Name: user.email, Value: user.email
      - Name: user.firstName, Value: user.firstName
      - Name: user.lastName, Value: user.lastName
  - Click `Next`
  - Choose `I'm an Okta customer adding an internal app`
  - Choose `This is an internal app that we have created`
  - Click `Finish`
- Get SAML information for the sample app
  - Inside the newly created application integration, click `Sign On` tab
  - Click `View Setup Instructions`
  - Copy the following information
    - Identity Provider Single Sign-On URL
    - Identity Provider Issuer
    - X.509 Certificate
