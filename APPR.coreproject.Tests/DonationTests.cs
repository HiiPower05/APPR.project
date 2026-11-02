using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using APPR.coreproject.Models;
using Xunit;

namespace APPR.coreproject.Tests
{
    public class DonationTests
    {
        // Test 1: A valid donation should pass validation.
        [Fact]
        public void ValidDonation_ShouldPassValidation()
        {
            var donation = new Donation
            {
                Amount = 500,
                Currency = "ZAR",
                DonationType = "Once-off",
                DonorName = "John Smith",
                DonorEmail = "john@example.com",
                TransactionReference = "TXN12345"
            };

            var results = ValidateModel(donation);

            Assert.Empty(results);
        }

        // Test 2: An invalid donation amount should fail validation.
        [Theory]
        [InlineData(0)]
        [InlineData(1000001)]
        public void InvalidDonationAmount_ShouldFailValidation(decimal amount)
        {
            var donation = new Donation
            {
                Amount = amount,
                Currency = "ZAR",
                DonationType = "Once-off",
                DonorName = "John Smith",
                DonorEmail = "john@example.com"
            };

            var results = ValidateModel(donation);

            Assert.Contains(results, r => r.MemberNames.Contains("Amount"));
        }

        // Test 3: An invalid email address should fail validation.
        [Fact]
        public void InvalidDonorEmail_ShouldFailValidation()
        {
            var donation = new Donation
            {
                Amount = 500,
                Currency = "ZAR",
                DonationType = "Once-off",
                DonorName = "John Smith",
                DonorEmail = "invalid-email"
            };

            var results = ValidateModel(donation);

            Assert.Contains(results, r => r.MemberNames.Contains("DonorEmail"));
        }

        // Helper method to validate donation properties.
        private static List<ValidationResult> ValidateModel(object model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model);

            Validator.TryValidateObject(
                model,
                context,
                results,
                validateAllProperties: true
            );

            return results;
        }
    }
}
