API keys
The API keys approach is a variation of the HTTP Basic authentication strategy.
This approach uses machine-generated strings to create unique pairs of identifying credentials and API access tokens. API keys can be sent as part of the payload, HTTP headers or query string, making them a good fit for consumer-facing web applications.
The benefit of API keys is that they decouple API access from the necessary credentials and validation tokens. 
Those credentials and tokens can be revoked and reissued as needed, such as if a user's permission level changes, 
or there is reason to believe the information has been compromised.
Unfortunately, API keys are susceptible to the same risks as Basic authentication, 
in that hackers could intercept and exploit the associated credentials. 
While they do provide a unique identification mechanism for front-end user interactions that can both apply and revoke credentials on demand, 
the simplicity of its design inhibits its ability to support layered authentication or MFA.

OAuth 2.0
OAuth (specifically, OAuth 2.0) is considered a gold standard when it comes to REST API authentication, 
especially in enterprise scenarios involving sophisticated web and mobile applications.
OAuth 2.0 can support dynamic collections of users, permission levels, scope parameters and data types. 
It's a preferred approach for organizations looking to secure large numbers of REST APIs that contain sensitive information.
OAuth 2.0 creates secured access tokens that are refreshed periodically using a series of procedural verification protocols known as grant types. 
These grant types act as proxy authentication mechanisms that direct the flow of API access requests without exposing the underlying credentials, 
tokens and other authentication information associated with those requests.
