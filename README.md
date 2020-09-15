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

Manual entry 
```xml
<!-- File .csproj -->

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
- `FACTORY` TotpSetupGenerator
- `CLASS` TotpSetup 

## Using the package

__Class: TotpGenerator__

Constructor Paramaters: `None`

Description: Used for generating the TOTP code, using a super secret code for your app. 

Methods:

```C#
int Generate(string accountSecretKey);
int Generate(string accountSecretKey, long counter, int digits = 6);
IEnumerable<int> GetValidTotps(string accountSecretKey, TimeSpan timeTolerance);
```

Example
```C#
var generator = new TotpGenerator();
var code = generator.Generate(_userIdentity.AccountSecretKey);
```

__TotpValidator__

Constructor Paramaters: `TotpGenerator`

Description: Generates a new token and compares against a given TOTP code to check validity.

Example
```C#
var generator = new TotpGenerator();
var validator = new TotpValidator(generator);
var code = validator.Validate(_userIdentity.AccountSecretKey, code);
```

__TotpSetupGenerator__

Constructor Paramaters: `None`

Description: Calls the google charts api to generate a qr code for 

Methods:
```C#
TotpSetup Generate(string issuer, string accountIdentity, string accountSecretKey, int qrCodeWidth = 300, int qrCodeHeight = 300, bool useHttps = true);
string GetQrImage(string url, int timeoutInSeconds = 30);
```

Example
```C#
var qrGenerator = new TotpSetupGenerator();
var qrCode = qrGenerator.Generate(
	issuer: "TestCo",
	accountIdentity: _userIdentity.Id.ToString(),
	accountSecretKey: _userIdentity.AccountSecretKey
);
```



### Implementation

```C#
using System;
using AspNetCore.Totp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthApi.Controllers
{
    struct UserIdentity {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountSecretKey { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class TotpController : ControllerBase
    {
        private readonly TotpGenerator _totpGenerator;
        private readonly TotpValidator _totpValidator;
        private readonly TotpSetupGenerator _totpQrGenerator;
        private readonly ILogger<TotpController> _logger;

        // For example only, you should be using an IAuthProvider
        static UserIdentity _userIdentity = 
            new UserIdentity {
                Id = new Random().Next(0, 999),
                Name = "Tom Jones",
                AccountSecretKey = Guid.NewGuid().ToString()
            };

        public TotpController(ILogger<TotpController> logger)
        {
            _logger = logger;
            _totpGenerator = new TotpGenerator();
            _totpValidator = new TotpValidator(_totpGenerator);
            _totpQrGenerator = new TotpSetupGenerator();
        }

        [HttpGet("code")]
        public int GetCode()
        {
           return _totpGenerator.Generate(_userIdentity.AccountSecretKey);
        }

        [HttpGet("qr")]
        public IActionResult GetQr() {
            var qrCode = _totpQrGenerator.Generate(
                issuer: "TestCo",
                accountIdentity: _userIdentity.Id.ToString(),
                accountSecretKey: _userIdentity.AccountSecretKey
            );

            var imageData = qrCode.QrCodeImage.Split(",")[1];
            var bytes = Convert.FromBase64String(imageData);
            return File(bytes, "image/png");
        }

        [HttpPost("validate")]
        public bool Validate([FromBody] int code) {
            return _totpValidator.Validate(_userIdentity.AccountSecretKey, code);
        }
    }
}

```


# License
[MIT License](License.md)
