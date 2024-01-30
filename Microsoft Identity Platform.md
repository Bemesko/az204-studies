## Sources
- [Explore the Microsoft identity platform - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/modules/explore-microsoft-identity-platform/)
- [OAuth 2.0 and OpenID Connect protocols on the Microsoft identity platform - Microsoft identity platform | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity-platform/v2-protocols)
- [Overview of permissions and consent in the Microsoft identity platform - Microsoft identity platform | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity-platform/permissions-consent-overview#openid-connect-scopes)
- [Microsoft Entra multifactor authentication overview - Microsoft Entra ID | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity/authentication/concept-mfa-howitworks)
- [Microsoft identity platform app types and authentication flows - Microsoft identity platform | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity-platform/authentication-flows-app-scenarios)
- [Microsoft identity platform app types and authentication flows - Microsoft identity platform | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity-platform/authentication-flows-app-scenarios)
- [Diagrams And Movies Of All The OAuth 2.0 Flows | by Takahiko Kawasaki | Medium](https://darutk.medium.com/diagrams-and-movies-of-all-the-oauth-2-0-flows-194f3c3ade85)
- [Diagrams of All The OpenID Connect Flows | by Takahiko Kawasaki | Medium](https://darutk.medium.com/diagrams-of-all-the-openid-connect-flows-6968e3990660)
- [Microsoft identity platform and OAuth2.0 On-Behalf-Of flow - Microsoft identity platform | Microsoft Learn](https://learn.microsoft.com/en-us/entra/identity-platform/v2-oauth2-on-behalf-of-flow)
## Key Questions
- What is the Microsoft Identity Platform?
- What are the main components?
- What are the 3 types of service principals?
- How do service principals relate to application objects?
- How do permissions and user consent work?
- What authentication flows/patterns are there? When should I use each one?
- What is the difference between single tenant and multi tenant apps?
- What are application objects?
- What do application objects describe?
- What are service principal objects?
- What is conditional access? When should I use it?
### My Answers
- What is the Microsoft Identity Platform?
	- A bunch of Microsoft services that apps can use to not care about having to implement authentication
- What are the main components?
	- Oauth2/OIDC authentication services
	- Identity Platform Endpoint
	- MSAL or other open source libraries
	- App Management Portal/API/Powershell
- What are the 3 types of service principals?
	- Application
	- Managed Identities
	- Legacy
- How do service principals relate to application objects?
	- Application service principals are a tenant-specific instance of the globally unique application object
- How do permissions and user consent work?
	- Apps send the scopes they need to the Microsoft Identity APIs and if the user or admin hasn't yet consented they will be explicitly asked
- What authentication flows/patterns are there? When should I use each one?
- ![](media/Pasted%20image%2020240130084949.png)
- What is the difference between single tenant and multi tenant apps?
	- Single tenant only accept users from their home tenant (1 app, 1 service principal)
	- Multi tenant have a home tenant but can create service principals in other tenants (1 app, N service principals)
- What are application objects?
	- Globally unique representation of an app that is used as a blueprint to create service principals in other tenants
- What do application objects describe?
	- They describe what permissions their corresponding service principals will need and how it will handle them
- What are service principal objects?
	- Service principals are the tenant-specific instances of application objects
- What is conditional access? When should I use it?
	- Entra ID feature that lets admins control who can access stuff, along with how, where and when
	- You should use it when you have security requirements that enforce these rules or very sensitive applications
	- Some authentication flows need to handle conditional access explicitly, but not all of them

---
## Learning Path Notes
### Overview 
- Microsoft Identity Platform is a bunch of stuff that allows apps and people to authenticate and authorize using Microsoft technologies
- The key components are:
	- Oauth2 and OIDC compliant authentication services using one of many authentication sources
		- Entra ID: for users in the home tenant
		- Entra External ID: for users in other tenants
		- Azure AD B2C: for customer accounts
		- Personal Microsoft Accounts (Xbox, Live, Skype)
	- Open Source libraries such as [Microsoft Authentication Library](Microsoft%20Authentication%20Library) (MSAL): to be implemented in apps
	- Microsoft Identity Platform Endpoint: works with MSAL or other compliant libraries; Implements human readable scopes
	- Application Management Portal: Users can setup authentication using the Azure Portal
	- App configuration API and Powershell: Things can also be automated

Apps that integrate with the Identity platform get native support for a bunch of authentication related features such as passwordless, step up authentication and conditional access.
### Service Principals
- security principals
	- necessary for everything to authenticate against microsoft
	- user principals are for users, service principals are for applications

- an app will have an application object and a service principal object
	- the application object is globally unique and exists in the tenant in which the application was created (home tenant)
		- serves as a blueprint for creating service principal objects
	- service principal objects will exist in all tenants in which the application will be allowed access
		- created based on the application object
		- Each tenant can assign their own permissions to its service principal, even though they are all the same app

- types of service principals
	- application: represents an instance of an application in a specific tenant
	- managed identity: [[Managed Identities]]
	- legacy: created before app registrations were introduced

### Permissions and Consent
- what are permissions: small chunks of functionality that an app needs to use
- oauth2: authentication/authorization flow that is really annoying to try to understand. Uses tokens generated by an authorization service to authenticate and tell what permissions you have
- open id connect scopes: scopes that are presented in the format `<resource-id>/This.That`
- administrator consent: Azure administrators can consent to application permissions on behalf of all users in their organization, or give consent to applications that require elevated access
- ommited resource id: If a resource id is omitted during the authorization process, the Identity Platform assumes it's Microsoft Graph (`scope=User.Read` = `https://graph.microsoft.com/User.Read`)
- permission types
	- delegated: User is actively using the app the the app requests permission from to user to act on behalf of them
	- app only: Usually background apps or APIs that don't have a user actively using them when they need permissions
- consent types
	- static user consent: All permissions the app will ever need are presented and consented during the first time an user or admin opens the app
	- incremental/dynamic user consent: The app will need some permissions upfront and will gradually ask for more in its requests during; this makes admin consent harder because not all permissions the app needs are visible
	- admin consent: admin consents on behalf of all users for that organization

- example of consent request
```
GET https://login.microsoftonline.com/common/oauth2/v2.0/authorize?
client_id=6731de76-14a6-49ae-97bc-6eba6914391e
&response_type=code
&redirect_uri=http%3A%2F%2Flocalhost%2Fmyapp%2F
&response_mode=query
&scope=
https%3A%2F%2Fgraph.microsoft.com%2Fcalendars.read%20
https%3A%2F%2Fgraph.microsoft.com%2Fmail.send
&state=12345
```
- app sends this request to the microsoft oauth2 endpoint and uses the `scope` parameter to say that it needs the `calendars.read`and `mail.send` permissions in Microsoft Graph
- If the identity platform checks that the users hasn't consented to one of those two yet, it asks explicitly before moving on

### Conditional Access
- Entra ID feature that allows setting rules of how users should be able to access services
- You can enforce:
	- MFA
	- Logins only in InTune registered devices
	- Permit access only to specific regions and IPs
- Usually apps don't need to do anything to handle Conditional Access, unless they are one of the below. If they are, the code will need to handle the Conditional Access challenges or redirect them to the application that called them
	- Apps using the on behalf of authentication flow
	- Apps accessing multiple services/resources
	- Single page apps using MSAL.js
	- Web apps calling a resource

---
## Exam Prep Notes
![](media/Pasted%20image%2020240130072607.png)
- how to work with the identity platform 2.0
- study right sde of diagram
- register an app to use all 3 account types
- choose the authentication pattern (spa vs web vs app)
- authentication flows and scenarios
- 