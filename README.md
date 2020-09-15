# AspNetCore.Totp
An ASP.NET Core library for generating and validating one time passwords for Google & Microsoft Authenticator.

# Getting Started

## Installing the package

Open up an existing project, or create a new one. Add a reference to the AspNetCore.Totp library. 

.NET Core CLI
```  
dotnet add package AspNetCore.Totp
```

PowerShell (Nuget Package Manager)
```
Install-Package AspNetCore.Totp
```

Manual entry (.csproj) 
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
    ...
    <ItemGroup>
        <PackageReference Include="AspNetCore.Totp" Version="x.x.x" />
    </ItemGroup>
</Project>
```

## Public Namespace Structure

AspNetCore.Totp
- `CLASS` TotpGenerator
- `CLASS` TotpValidator
- `CLASS` TotpSetupGenerator

AspNetCore.Totp.Models
- `CLASS` TotpSetup
- `CLASS` QrCodeImage

AspNetCore.Totp.Interface
- `INTERFACE` IQrCodeImage
- `INTERFACE` ITotpGenerator
- `INTERFACE` ITotpSetup
- `INTERFACE` ITotpSetupGenerator
- `INTERFACE` ITotpValidator

## Using the package

__Class: TotpGenerator__

Constructor Parameters: `None`

Description: Used for generating the TOTP code, using a super secret code for your app. 

Example
```C#
var generator = new TotpGenerator();
var code = generator.Generate(_userIdentity.AccountSecretKey);
```

__TotpValidator__

Constructor Parameters: `TotpGenerator`

Description: Generates a new token and compares against a given TOTP code to check validity.

Example
```C#
var generator = new TotpGenerator();
var validator = new TotpValidator(generator);
var code = validator.Validate(_userIdentity.AccountSecretKey, code);
```

__TotpSetupGenerator__

Constructor Parameters: `None`

Description: Used to fetch a QR code image from the google charts api and return it as a TotpSetup class containing the image. 

Example
```C#
var qrGenerator = new TotpSetupGenerator();
var qrCode = qrGenerator.Generate(
	issuer: "TestCo",
	accountIdentity: _userIdentity.Id.ToString(),
	accountSecretKey: _userIdentity.AccountSecretKey
);
```

### Example Implementation

```C#
using System;
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    internal struct UserIdentity
    {
        public int Id { get; set; }
        public string AccountSecretKey { get; set; }
    }
    
    internal static class AuthProvider
    {
        public static UserIdentity GetUserIdentity()
        {
            return new UserIdentity()
            {
                Id = new Random().Next(0, 999),
                AccountSecretKey = Guid.NewGuid().ToString()
            };
        }
    }
    
    [ApiController]
    [Route("[controller]")]
    public class TotpController : ControllerBase
    {
        private readonly ITotpGenerator _totpGenerator;
        private readonly ITotpSetupGenerator _totpQrGenerator;
        private readonly ITotpValidator _totpValidator;
        private readonly UserIdentity _userIdentity;

        public TotpController()
        {
            _totpGenerator = new TotpGenerator();
            _totpValidator = new TotpValidator(_totpGenerator);
            _totpQrGenerator = new TotpSetupGenerator();
            _userIdentity = AuthProvider.GetUserIdentity();
        }

        [HttpGet("code")]
        public int GetCode()
        {
            return _totpGenerator.Generate(_userIdentity.AccountSecretKey);
        }

        [HttpGet("qr-code")]
        public IActionResult GetQr()
        {
            var qrCode = _totpQrGenerator.Generate(
                "TestCo",
                _userIdentity.Id.ToString(),
                _userIdentity.AccountSecretKey
            );
            return File(qrCode.QrCodeImageBytes, "image/png");
        }

        [HttpPost("validate")]
        public bool Validate([FromBody] int code)
        {
            return _totpValidator.Validate(_userIdentity.AccountSecretKey, code);
        }
    }
}
```

# License
[MIT License](License.md)
