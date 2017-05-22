using System;
using System.Net;
using System.Globalization;

namespace lesson3
{
    public class GiftCard : IItem
    {
        /// <summary>
        /// Creates a new GiftCard.
        /// </summary>
        /// <param name="amount">Amount must be greater than 0.</param>
        public GiftCard(decimal amount, Currency currency)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be greater than 0.", nameof(amount));

            Amount = amount;
            Currency = currency;
            Code = Guid.NewGuid().ToString();
            IsRedeemed = false;
        }

        /// <summary>
        /// Value of this gift card.
        /// </summary>
        public decimal Amount { get; }

        /// <summary>
        /// Currency of Amount.
        /// </summary>
        public Currency Currency { get; }

        /// <summary>
        /// The unique code to redeem this gift card.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Redeems this gift card. Can only be performed once.
        /// </summary>
        public void Redeem()
        {
            if (IsRedeemed) throw new InvalidOperationException ($"Gift card {Code} has already been redeemed.");
            IsRedeemed = true;
        }

        /// <summary>
        /// True, if this gift card has been redeemed. 
        /// </summary>
        public bool IsRedeemed { get; private set; }

        #region IItem implementation

        public decimal GetPrice (Currency currency)
        {
            return Amount * ExchangeRates.Get(Currency, currency);
        }

        public string Description => "GiftCard " + Code;

        #endregion
    }
}

