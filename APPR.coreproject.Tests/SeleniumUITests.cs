
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace APPR.coreproject.Tests
{
    public class SeleniumUITests
    {
        private const string BaseUrl = "https://localhost:7090";

        [Fact]
        public void AnonymousDonationTest()
        {
            var options = new ChromeOptions();
            options.AddArgument("--ignore-certificate-errors");

            using (IWebDriver driver = new ChromeDriver(options))
            {
                try
                {
                    driver.Navigate().GoToUrl($"{BaseUrl}/Donate");

                    var wait = new WebDriverWait(
                        driver, TimeSpan.FromSeconds(10));

                    // Enter donor information.
                    driver.FindElement(By.Id("Donation_DonorName"))
                        .SendKeys("Selenium Test Donor");

                    driver.FindElement(By.Id("Donation_DonorEmail"))
                        .SendKeys("selenium@example.com");

                    driver.FindElement(By.Id("Donation_Amount"))
                        .SendKeys("250");

                    // Select currency.
                    var currency = new SelectElement(
                        driver.FindElement(By.Id("Donation_Currency")));

                    currency.SelectByValue("ZAR");

                    // Select donation type.
                    var donationType = new SelectElement(
                        driver.FindElement(
                            By.Id("Donation_DonationType")));

                    donationType.SelectByValue("One-Time");

                    // Submit donation.
                    driver.FindElement(
                        By.CssSelector("button[type='submit']"))
                        .Click();

                    // Wait for the confirmation page.
                    wait.Until(d =>
                        d.FindElement(
                            By.CssSelector("#taxCertificate h1"))
                        .Displayed);

                    // Verify successful donation.
                    string confirmationText =
                        driver.FindElement(By.Id("taxCertificate")).Text;

                    Assert.Contains(
                        "Donation Confirmed", confirmationText);

                    Assert.Contains(
                        "Selenium Test Donor", confirmationText);

                    Assert.Contains(
                        "Your donation has been successfully recorded",
                        confirmationText);

                    Assert.Contains("DON-", confirmationText);
                }
                finally
                {
                    driver.Quit();
                }
            }
        }
    }
}
