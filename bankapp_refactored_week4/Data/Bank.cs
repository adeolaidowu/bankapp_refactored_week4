using bankapp_refactored_week4.ClassLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.Data
{
    public class Bank
    {
        public static List<Customer> allCustomers = new List<Customer>();
        public static List<Transaction> allTransactions = new List<Transaction>();

        // register new customer
        public static Customer Register(string fname, string lname, string email, string username, string password)
        {
            Customer customer = null;
            if (fname.Length == 0 || lname.Length == 0 || email.Length == 0 || !email.Contains('@') || username.Length == 0 || password.Length == 0)
            {
                throw new ArgumentException("All fields are required. ");
            }
            else
            {
                customer = new Customer(fname, lname, email, username, password);
            }
            return customer;
        }

            //method to fetch all bank customers
            public static string GetAllCustomers()
        {
            if (allCustomers.Count > 0)
            {
                var BankCustomers = new StringBuilder();
                BankCustomers.AppendLine("Customer Name\t\tCustomer Email");
                foreach (var customer in allCustomers)
                {
                    BankCustomers.AppendLine($"{customer.FullName}\t\t{customer.Email}\t\t{customer.Username}");
                }
                return BankCustomers.ToString();
            }
            return "You have no customers in your bank presently";
        }
    }
}
