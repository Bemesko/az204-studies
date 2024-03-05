using System.Threading.Tasks;
using Microsoft.Identity.Client;

const string _clientId = "25e0e726-abc8-41a6-9dca-3fb4ddf39071";
const string _tenantId = "495a6f2f-e021-4750-b745-9b4282842206";

var app = PublicClientApplicationBuilder
    .Create(_clientId)
    .WithAuthority(AzureCloudInstance.AzurePublic, _tenantId)
    .WithRedirectUri("http://localhost")
    .Build();

string[] scopes = { "user.read" };

AuthenticationResult result = await app.AcquireTokenInteractive(scopes).ExecuteAsync();

Console.WriteLine($"Token:\t{result.AccessToken}");

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
