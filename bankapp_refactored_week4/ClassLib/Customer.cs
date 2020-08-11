using bankapp_refactored_week4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.ClassLib
{
    public class Customer : Bank
    {
        public int CustomerId { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Email { get; set; }
        public string Username { get; }
        public string Password { get; }
        public bool IsLoggedIn { get; private set; } = false;
        private static int seedId = 1234;
        public Customer(string firstname, string lastname, string email, string username, string password)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            CustomerId = seedId;
            Username = username;
            Password = password;
            seedId++;
            Bank.allCustomers.Add(this);

        }

        public void LogIn(string username, string password)
        {
            if (Username == username && Password == password)
            {
                IsLoggedIn = true;
            }
            else
            {
                Console.WriteLine("Incorrect username or password");
            }
        }
        public void LogOut()
        {
            IsLoggedIn = false;
            Console.WriteLine("You have been logged out of your account");
        }
        public List<BankAccount> myAccounts = new List<BankAccount>();
        public string GetAccounts()
        {
            var accounts = new StringBuilder();
            accounts.AppendLine("Acc Num\t\t Acc Type\t\t");// Header
            foreach (var account in myAccounts)
            {
                accounts.AppendLine($"{account.AccNumber}\t{account.AccountType}");
            }
            return accounts.ToString();
        }
    }
}
