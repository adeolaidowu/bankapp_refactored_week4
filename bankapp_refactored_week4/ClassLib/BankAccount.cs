using bankapp_refactored_week4.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace bankapp_refactored_week4.ClassLib
{
    public class BankAccount
    {
        public int AccNumber { get; }
        public string Owner { get; set; }
        //property to get account balance
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var transaction in myTransactions)
                {
                    balance += transaction.Amount;
                }
                return balance;
            }
        }
        public DateTime DateCreated { get; set; }
        public string Note { get; }
        public string AccountType { get; }

        //generate random account number
        private static int seedAccNum = 1234567890;

        //initialize a new list to store all transactions related to this account
        private List<Transaction> myTransactions = new List<Transaction>();


        //constructor function for bank account class
        public BankAccount(Customer customer, string type, decimal initialBalance)
        {
            Owner = customer.FullName;
            DateCreated = DateTime.Now;
            AccountType = type;
            if(type == "savings")
            {
                if(initialBalance < 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(initialBalance), "Amount must be over N100");
                }
                else
                {
                    MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
                }
            }
            else if(type == "current")
            {
                if (type == "current" && initialBalance < 1000)
                {
                    throw new ArgumentOutOfRangeException(nameof(initialBalance), "Amount must be over N1000");
                }
                else
                {
                    MakeDeposit(initialBalance, DateTime.Now, "Initial Balance");
                }
            }
            
            AccNumber = seedAccNum;
            seedAccNum++;
            Note = $"Congratulations, a {AccountType} account with the number {AccNumber} has been created for {Owner} with CustomerId of {customer.CustomerId}. It was created on {DateCreated.ToShortDateString()} with an initial deposit of {initialBalance}";
            customer.myAccounts.Add(this);
        }
        //method for making deposits
        public void MakeDeposit(decimal amount, DateTime date, string note = "")
        {
            if (AccountType.ToLower() == "savings")
            {

                if (amount <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a non-zero positive number");
                }
                else
                {
                    var deposit = new Transaction(amount, date, note);
                    myTransactions.Add(deposit);
                }

            }
            else if (AccountType.ToLower() == "current")
            {
                if (amount <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a non-zero positive number");
                }
                else
                {
                    var deposit = new Transaction(amount, date, note);
                    myTransactions.Add(deposit);
                }
            }
        }
        //method for making withdrawals
        public void MakeWithdrawal(decimal amount, DateTime date, string note = "")
        {
            if (AccountType.ToLower() == "savings")
            {
                if (amount <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be above 0 naira");
                }
                else if (Balance - amount < 100)
                {
                    throw new InvalidOperationException("You cannot withdraw past the minimum account balance");
                }
                var withdrawal = new Transaction(-amount, date, note);
                myTransactions.Add(withdrawal);
            }
            else if (AccountType.ToLower() == "current")
            {
                if (amount <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be above 0 naira");
                }
                else if (Balance - amount < 0)
                {
                    throw new InvalidOperationException("You cannot withdraw past the minimum account balance");
                }
                var withdrawal = new Transaction(-amount, date, note);
                myTransactions.Add(withdrawal);
            }
        }
        //method to transfer funds to another account
        public void TransferTo(int accNum, decimal amount, DateTime date, string note)
        {
            string feedback = "";
            Customer SelectedCustomer = null;
            BankAccount SelectedAccount = null;
            foreach (var customer in Bank.allCustomers)
            {
                foreach (var account in customer.myAccounts)
                {
                    if (account.AccNumber == accNum)
                    {
                        SelectedCustomer = customer;
                        SelectedAccount = account;
                    }
                }
                if (SelectedCustomer != null) break;
            }
            feedback = $"You have successfully transferred {amount} to {SelectedAccount.Owner}";
            this.MakeWithdrawal(amount, date, note);
            SelectedAccount.MakeDeposit(amount, date, note);
            Console.WriteLine(feedback);
        }
        //method to fetch statement of account
        public string GetStatement()
        {
            var statement = new StringBuilder();
            statement.AppendLine("Date\t\tAmount\t\tNote"); // statement header
            foreach (var txn in myTransactions)
            {
                //rows
                statement.AppendLine($"{txn.Date.ToShortDateString()}\t{txn.Amount}\t\t{txn.Note}");
            }
            return statement.ToString();
        }
    }
}
