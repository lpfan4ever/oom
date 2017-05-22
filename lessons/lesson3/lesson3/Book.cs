using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace lesson3
{
    struct Price
    {
        public decimal Amount { get; }
        public Currency Unit { get; }
        public Price(decimal amount, Currency unit)
        {
            Amount = amount;
            Unit = unit;
        }
    }
    public class Book : IItem
    {
        private decimal m_price;

        /// <summary>
        /// Creates a new book object.
        /// </summary>
        /// <param name="title">Title must not be empty.</param>
        /// <param name="isbn">International Standard Book Number.</param>
        /// <param name="price">Price must not be negative.</param>
        public Book(string title, string isbn, decimal price, Currency currency)
        {
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title must not be empty.", nameof(title));
            if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN must not be empty.", nameof(isbn));

            Title = title;
            ISBN = isbn;
            UpdatePrice(price, currency);
        }

        /// <summary>
        /// Gets the book title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the International Standard Book Number.
        /// </summary>
        public string ISBN { get; }

        /// <summary>
        /// Gets the currency of this book's price.
        /// </summary>
        public Currency Currency { get; private set;}

        /// <summary>
        /// Gets the price of this book.
        /// </summary>
        public decimal Price => m_price;

        /// <summary>
        /// Updates the book's price.
        /// </summary>
        /// <param name="newPrice">Price must not be negative.</param>
        /// <param name="newCurrency">Currency.</param>
        public void UpdatePrice(decimal newPrice, Currency currency)
        {
            if (newPrice < 0) throw new ArgumentException("Price must not be negative.", nameof(newPrice));
            m_price = newPrice;
            Currency = currency;
        }

        #region IItem implementation

        /// <summary>
        /// Gets a textual description of this item.
        /// </summary>
        public string Description => Title;

        /// <summary>
        /// Gets the book's price in the given currency.
        /// </summary>
        public decimal GetPrice(Currency currency)
        {
            return Price * ExchangeRates.Get(Currency, currency);
        }

        #endregion
    }
}

