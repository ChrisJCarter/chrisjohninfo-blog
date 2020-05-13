using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChrisJohnInfo.Blog.AdminUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((ctx, builder) =>
                    {
                        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                        var isDevelopment = string.Equals("Development", envName, StringComparison.OrdinalIgnoreCase);
                        if (!isDevelopment)
                        {
                            var keyVaultEndpoint = GetKeyVaultEndpoint();
                            if (!string.IsNullOrEmpty(keyVaultEndpoint))
                            {
                                var azureServiceTokenProvider = new AzureServiceTokenProvider();
                                var keyVaultClient = new KeyVaultClient(
                                    new KeyVaultClient.AuthenticationCallback(
                                        azureServiceTokenProvider.KeyVaultTokenCallback));
                                builder.AddAzureKeyVault(
                                    keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
                            }
                        }
                    }
                ).UseStartup<Startup>()
                .Build();
        private static string GetKeyVaultEndpoint() => "https://kv-chrisjohninfo.vault.azure.net/";
    }
}

