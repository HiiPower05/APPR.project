using System;
using System.Collections.Generic;

namespace GiftOfTheGivers.Helpers
{
    /// <summary>Calculations for monetary donation amounts.</summary>
    public static class DonationCalculator
    {
        /// <summary>Adds donation amounts without floating-point rounding errors.</summary>
        /// <exception cref="ArgumentNullException">Amounts is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An amount is negative.</exception>
        /// <exception cref="OverflowException">The total exceeds decimal range.</exception>
        public static decimal CalculateTotal(IEnumerable<decimal> amounts)
        {
            if (amounts == null)
                throw new ArgumentNullException(nameof(amounts));

            decimal total = 0m;
            foreach (decimal amount in amounts)
            {
                if (amount < 0m)
                    throw new ArgumentOutOfRangeException(nameof(amounts), "Donation amounts cannot be negative.");
                total = checked(total + amount);
            }

            return total;
        }
    }
}
