using bankapp_refactored_week4.Data;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace bankapp_refactored_week4.ClassLib
{
    class Controller
    {
        // <summary>
        /// methods runs program application
        /// </summary>
        public static void RunProgram()
        {
        //BEGINNING OF APPLICATION

        MainMenu: Console.WriteLine("Welcome to NaijaBank..");
            Console.WriteLine("If you are a new customer, Enter 1 to register. If you are an existing customer enter 2 to login");
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            var response = Console.ReadLine();

            //WHAT TO DO IF USER SELECTS OPTION TO CREATE ACCOUNT
            #region RetrieveUserInfo
            if (response == 1.ToString())
            {
                Console.WriteLine("Answer the following questions to create account");
                Console.Write("First name: ");
                var firstName = Console.ReadLine();
                Console.Write("Last name: ");
                var lastName = Console.ReadLine();
                Console.Write("Email: ");
                var email = Console.ReadLine();
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                //CHECK IF ALL REQUIRED VALUES TO CREATE CUSTOMER HAVE BEEN PROVIDED
                try
                {
                    Bank.Register(firstName, lastName, email, username, password);
                    Console.WriteLine("Congratulations, you have been registered as a customer at NaijaBank. Return to the Main Menu to Login");
                    Console.WriteLine("Press any key to return to the Main Menu");
                    Console.ReadLine();
                    goto MainMenu;
                }
                catch (Exception ex)
                {

                    Console.Write(ex.Message);
                    Console.Write("Press any key to return to try again");
                    Console.ReadLine();
                    goto MainMenu;
                }
            }
            #endregion
            // WHAT TO DO IF USER SELECTS OPTION TO LOGIN
            else if (response == 2.ToString())
            {
                LogIn(Bank.allCustomers);
            }
            else Console.WriteLine("Invalid response. Press any key to try again");
            Console.ReadLine();
            goto MainMenu;
        }
        //PROGRAM FLOW AFTER SUCCESSFULLY CREATING ACCOUNT
        static void LogIn(List<Customer> customers)
        {
        //Customer newCustomer;
        Login: Console.WriteLine("Enter your login details to perform account transactions");
            Console.Write("Enter Username: ");
            var Username = Console.ReadLine();
            Console.Write("Enter Password: ");
            var Password = Console.ReadLine();
            Customer customer = null;
            foreach (var user in customers)
            {
                if (user.Username == Username && user.Password == Password) customer = user;
                //customer = user.Username == Username && user.Password == Password ? user: null;
            }
            if (customer == null)
            {
                Console.WriteLine("Incorrect username or password");
                Console.ReadLine();
                goto Login;
            }
            else
            {
                customer.LogIn(Username, Password);
                // PROGRAM FLOW AFTER SUCCESSFUL LOGIN
                // #region LoggedInRegion
                Console.WriteLine($"Welcome, you are successfully logged in as {customer.FullName}");
            //Console.WriteLine("Which of your accounts would you like to transact on today?");
            LoginMenu: Console.WriteLine("What would you like to do? Enter the number that corresponds to your choice");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Select Account");
                var choice = Console.ReadLine();
                switch (int.Parse(choice))
                {
                    case 1:
                    // GET DETAILS NEEDED TO CREATE ACCOUNT
                    AccountChoice: Console.WriteLine("Select Account Type (Type the corresponding number of your selection)");
                        Console.WriteLine("1. Savings - Minimum deposit required is N100");
                        Console.WriteLine("2. Current - Minimum deposit required is N1000");
                        var accountType = Console.ReadLine();
                        string type;
                        if (accountType == 1.ToString())
                        {

                            Console.WriteLine("How much do you want to deposit?");
                            var amount = decimal.Parse(Console.ReadLine());
                            while (amount < 100)
                            {
                                type = "savings";
                                try
                                {
                                    new BankAccount(customer, type, amount);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                    Console.Write("Enter deposit amount: ");
                                    amount = decimal.Parse(Console.ReadLine());
                                }
                                //Console.WriteLine("Minimum deposit required for a savings account is N100");
                                
                                /*Console.Write("Enter deposit amount: ");
                                amount = decimal.Parse(Console.ReadLine());*/
                            }
                            type = "savings";
                            BankAccount customerAccount = new BankAccount(customer, type, amount);
                            Console.WriteLine(customerAccount.Note);
                            Console.WriteLine("Press any key to return to the account menu to perform transactions");
                            Console.ReadLine();
                            goto LoginMenu;
                            //LogIn();

                        }
                        else if (accountType == 2.ToString())
                        {
                            Console.WriteLine("How much do you want to deposit?");
                            var amount = decimal.Parse(Console.ReadLine());
                            while (amount < 1000)
                            {
                               /* if ()
                                {

                                }*/
                                // Console.WriteLine("Minimum deposit required for a current account is N1000");
                                type = "current";
                                try
                                {
                                    new BankAccount(customer, type, amount);
                                }
                                catch (Exception ex)
                                {

                                    Console.WriteLine(ex.Message);
                                    Console.Write("Enter deposit amount: ");
                                    amount = decimal.Parse(Console.ReadLine());
                                }
                                /*Console.Write("Enter deposit amount: ");
                                amount = decimal.Parse(Console.ReadLine());*/
                            }
                            type = "current";
                            BankAccount customerAccount = new BankAccount(customer, type, amount);
                            Console.WriteLine(customerAccount.Note);
                            Console.WriteLine("Press any key to return to the account menu to perform transactions");
                            Console.ReadLine();
                            goto LoginMenu;
                            //LogIn();
                        }
                        else Console.WriteLine("Invalid selection. Press any key to try again");
                        Console.ReadLine();
                        goto AccountChoice;
                    case 2:
                    GetAccount: Console.WriteLine(customer.GetAccounts());
                        Console.Write("Which of your above accounts do you want to transact with? Enter Account Number: ");
                        var AccNum = Console.ReadLine();
                        BankAccount SelectedAccount = null;
                        foreach (var account in customer.myAccounts)
                        {
                            Console.WriteLine(account.AccNumber);
                            if (account.AccNumber == int.Parse(AccNum))
                            {
                                SelectedAccount = account;

                            }
                        }
                        if (SelectedAccount == null)
                        {
                            Console.WriteLine("Invalid Account Number");
                            Console.WriteLine("Press any key to try again");
                            Console.ReadLine();
                            goto GetAccount;
                        }
                        else
                        {
                        TransactionChoice: Console.WriteLine("What would you like to do? Enter the number that corresponds to your choice");
                            Console.WriteLine("1. Deposit");
                            Console.WriteLine("2. Withdraw");
                            Console.WriteLine("3. Get Account Balance");
                            Console.WriteLine("4. Transfer");
                            Console.WriteLine("5. Get Account Statement");
                            Console.WriteLine("6. Log Out");

                            var action = Console.ReadLine();
                            switch (int.Parse(action))
                            {
                                case 1:
                                   Deposit: Console.Write("Deposit amount: ");
                                    var DepositAmount = decimal.Parse(Console.ReadLine());
                                    Console.Write("Optional note for this transaction: ");
                                    var DepositNote = Console.ReadLine();
                                    if (DepositAmount <= 0)
                                    {
                                        Console.WriteLine("Amount must be a non-zero positive number. Press any key to try again");
                                        Console.ReadLine();
                                        goto Deposit;
                                        /*Console.Write("Enter deposit amount: ");
                                        amount = decimal.Parse(Console.ReadLine());*/
                                        //goto LoginMenu;
                                    }
                                    else
                                    {
                                        SelectedAccount.MakeDeposit(DepositAmount, DateTime.Now, DepositNote);
                                        Console.Write($"You have successfully deposited {DepositAmount} into your account. Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    }
                                    
                                case 2:
                                    Withdraw: Console.Write("Withdrawal amount: ");
                                    var WithdrawalAmount = decimal.Parse(Console.ReadLine());
                                    Console.Write("Optional note for this transaction: ");
                                    var WithdrawalNote = Console.ReadLine();
                                    if (WithdrawalAmount <= 0)
                                    {
                                        Console.WriteLine("Amount must be a non-zero positive number. Press any key to try again");
                                        Console.ReadLine();
                                        goto Withdraw;
                                        /*Console.Write("Enter deposit amount: ");
                                        amount = decimal.Parse(Console.ReadLine());*/
                                        //goto LoginMenu;
                                    } else
                                    {
                                        SelectedAccount.MakeWithdrawal(WithdrawalAmount, DateTime.Now, WithdrawalNote);
                                        Console.Write($"You have successfully made a withdrawal of {WithdrawalAmount} from your account. Press any key to go to the transactions menu.");
                                        Console.ReadLine();
                                        break;
                                    }
                                case 3:
                                    Console.WriteLine($"Your Account Balance is {SelectedAccount.Balance}");
                                    Console.Write("Press any key to go to the transactions menu.");
                                    Console.ReadLine();
                                    break;
                                case 4:
                                    Console.WriteLine("Enter destination account number");
                                    int.TryParse(Console.ReadLine(), out int accNum);
                                    Console.Write("Amount: ");
                                    var TransferAmount = decimal.Parse(Console.ReadLine());
                                    Console.Write("Optional note for this transfer: ");
                                    var TransferNote = Console.ReadLine();
                                    SelectedAccount.TransferTo(accNum, TransferAmount, DateTime.Now, TransferNote);
                                    Console.Write("Press any key to go to the transactions menu.");
                                    Console.ReadLine();
                                    break;
                                case 5:
                                    Console.WriteLine(SelectedAccount.GetStatement());
                                    Console.Write("Press any key to go to the transactions menu.");
                                    Console.ReadLine();
                                    break;
                                case 6:
                                    Console.WriteLine("working");
                                    customer.LogOut();
                                    RunProgram();
                                    break;
                                default:
                                    Console.WriteLine("You have made an invalid selection");
                                    break;

                            }
                            goto TransactionChoice;
                        }
                }
                /*#endregion
                else Console.WriteLine("Incorrect username or password. Press any key to try again");
                Console.ReadLine();
                goto Login;*/
            }
        }
    }
}
