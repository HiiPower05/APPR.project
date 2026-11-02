using System;

using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;
using APPR.coreproject.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace APPR.coreproject.Tests
{
    public class DonationIntegrationTests : IClassFixture<DonationWebApplicationFactory>
    {
        private readonly DonationWebApplicationFactory _factory;

        public DonationIntegrationTests(
            DonationWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task SubmitDonation_ShouldSaveDonationAndRedirectToConfirmation()
        {
            // Arrange: Create the test database.
            await using (var scope = _factory.Services.CreateAsyncScope())
            {
                var db = scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

                await db.Database.EnsureCreatedAsync();
            }

            using var client = _factory.CreateClient(
                new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                    HandleCookies = true
                });

            // Act 1: Open the donation form.
            var getResponse = await client.GetAsync("/Donate/Index");

            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

            var html = await getResponse.Content.ReadAsStringAsync();

            // Obtain the anti-forgery token from the form.
            var tokenMatch = Regex.Match(
                html,
                @"<input\b(?=[^>]*\bname=""__RequestVerificationToken"")(?=[^>]*\bvalue=""([^""]+)"")[^>]*>",
                RegexOptions.IgnoreCase);

            Assert.True(
                tokenMatch.Success,
                "The anti-forgery token was not found in the donation form.");

            var token = tokenMatch.Groups[1].Value;

            // Act 2: Submit a donation through the actual Razor Page.
            var formData = new Dictionary<string, string>
            {
                ["Donation.DonorName"] = "Integration Test Donor",
                ["Donation.DonorEmail"] = "integration@example.com",
                ["Donation.Amount"] = "250",
                ["Donation.Currency"] = "ZAR",
                ["Donation.DonationType"] = "One-Time",
                ["__RequestVerificationToken"] = token
            };

            var postResponse = await client.PostAsync(
                "/Donate/Index",
                new FormUrlEncodedContent(formData));

            // Assert 1: The application redirects to confirmation.
            // Diagnostic: inspect the response if the POST does not redirect.
            if (postResponse.StatusCode != HttpStatusCode.Redirect)
            {
                var responseHtml = await postResponse.Content.ReadAsStringAsync();

                Assert.Fail(
                    $"Expected a redirect but received {(int)postResponse.StatusCode} " +
                    $"{postResponse.StatusCode}.\nResponse HTML:\n{responseHtml}");
            }

            var redirectUrl =
                postResponse.Headers.Location?.ToString() ?? "";

            Assert.Contains("/Donate/Confirmation", redirectUrl);

            // Assert 2: The donation was saved to SQLite.
            await using var verificationScope =
                _factory.Services.CreateAsyncScope();

            var verificationDb = verificationScope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var savedDonation = await verificationDb.Donations
                .SingleAsync(d =>
                    d.DonorEmail == "integration@example.com");

          

            Assert.Equal(250.00m, savedDonation.Amount);
            Assert.Equal("ZAR", savedDonation.Currency);
            Assert.Equal("One-Time", savedDonation.DonationType);
            Assert.Equal("Integration Test Donor", savedDonation.DonorName);

            // Assert 3: A transaction reference was generated.
            Assert.StartsWith("DON-", savedDonation.TransactionReference);
            Assert.Equal(16, savedDonation.TransactionReference.Length);

            Assert.NotEqual(default(DateTime), savedDonation.DonationDate);

            // Assert 4: The simulated Azure Function received the request.
            Assert.NotNull(_factory.CertificateHandler.RequestBody);

            using var requestJson = JsonDocument.Parse(
                _factory.CertificateHandler.RequestBody!);

            var request = requestJson.RootElement;

            Assert.Equal(
                savedDonation.TransactionReference,
                request.GetProperty("transactionReference").GetString());

            Assert.Equal(
                "Integration Test Donor",
                request.GetProperty("donorName").GetString());

            Assert.Equal(
                250.00m,
                request.GetProperty("amount").GetDecimal());

            // Assert 5: The certificate number is included in the redirect.
            Assert.Contains("CERT-TEST-001", redirectUrl);
        }
    }

    // Provides an isolated SQLite database and fake HTTP client.
    public class DonationWebApplicationFactory
        : WebApplicationFactory<APPR.coreproject.Program>
    {
        private readonly SqliteConnection _connection =
            new SqliteConnection("Data Source=:memory:");

        public FakeCertificateHandler CertificateHandler { get; } =
            new FakeCertificateHandler();

        public DonationWebApplicationFactory()
        {
            _connection.Open();
        }

        protected override void ConfigureWebHost(
            IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureTestServices(services =>
            {
                // Remove the original SQL Server database registration.
                services.RemoveAll<ApplicationDbContext>();
                services.RemoveAll<DbContextOptions<ApplicationDbContext>>();
                services.RemoveAll<IDbContextOptionsConfiguration<ApplicationDbContext>>();

                // Register SQLite for integration testing.
                services.AddDbContext<ApplicationDbContext>(
                    options => options.UseSqlite(_connection));

                // Replace the real HTTP client with a fake one.
                services.RemoveAll<IHttpClientFactory>();

                services.AddSingleton<IHttpClientFactory>(
                    _ => new FakeHttpClientFactory(CertificateHandler));
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection.Dispose();
            }

            base.Dispose(disposing);
        }
    }

    // Simulates a successful Azure Function response.
    public class FakeCertificateHandler : HttpMessageHandler
    {
        public string? RequestBody { get; private set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestBody = request.Content == null
                ? null
                : await request.Content.ReadAsStringAsync(
                    cancellationToken);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new
                {
                    CertificateNumber = "CERT-TEST-001"
                })
            };
        }
    }

    // Supplies the fake HTTP handler to the donation page.
    public class FakeHttpClientFactory : IHttpClientFactory
    {
        private readonly HttpMessageHandler _handler;

        public FakeHttpClientFactory(HttpMessageHandler handler)
        {
            _handler = handler;
        }

        public HttpClient CreateClient(string name)
        {
            return new HttpClient(_handler, disposeHandler: false);
        }
    }
}
