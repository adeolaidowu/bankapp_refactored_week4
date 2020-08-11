using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.ClassLib
{
    class Transaction
    {
        public decimal Amount { get; }
        public DateTime Date { get; }
        public string Note { get; }

        public Transaction(decimal amount, DateTime date, string note)
        {
            Amount = amount;
            Date = date;
            Note = note;
        }
    }
}
