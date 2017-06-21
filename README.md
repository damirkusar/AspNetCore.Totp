# AspNetCore.Totp
An ASP.NET Core library for generating and validating one time passwords for google authenticator.

# Getting Started
	```
	Install-Package AspNetCore.Totp
	```

	Use TotpSetupGenerator class to generate the QR Code to show to user.
	Use TotpGenerator class to generate a one time password like the Google authenticator does.
	Use TotpValidator class to validate the one time password supplied by the user. 


# License
[MIT License](License.md)