using NUnit.Framework;
using bankapp_refactored_week4.ClassLib;
using bankapp_refactored_week4.Data;
using System;

namespace bankapp_refactored_week4_Test
{
    [TestFixture]
    public class Test

    {
        [Test]
        public void CheckLoginValid()
        {
            // Arrange
            Customer newCust = new Customer("harvey", "specter", "h@specter.com", "harvey", "hardman");
            // Act
            newCust.LogIn("harvey", "hardman");
            //Assert
            Assert.That(newCust.IsLoggedIn, Is.EqualTo(true));
        }

        [Test]
        public void CheckLoginInvalid()
        {
            // Arrange
            Customer newCust = new Customer("harvey", "specter", "h@specter.com", "harvey", "hardman");
            // Act
            newCust.LogIn("harvey", "ross");
            //Assert
            Assert.That(newCust.IsLoggedIn, Is.EqualTo(false));
        }

        [Test]
        public void RegisterValidDetails()
        {
            // Arrange
            Customer cust1 = Bank.Register("harvey", "specter", "h@specter.com", "harvey", "hardman");
            Customer cust2 = cust1;
            //Assert
            Assert.That(cust1, Is.SameAs(cust2));
        }

      [Test]
        public void RegisterInValidDetails()
        {
            Assert.Throws<ArgumentException>(() => Bank.Register("", "specter", "", "harvey", "hardman"));
        }

        // TESTS FOR DEPOSIT METHOD
        [Test]
        public void DepositValidAmount()
        {
            //Arrange
            var randomCust = new Customer("tom", "riddle", "tom@riddle.com", "tom", "marvolo");
            var randomAcc = new BankAccount(randomCust, "current", 72000);
            //Act
            randomAcc.MakeDeposit(28000, DateTime.Now, "saving for new wand");
            //Assert
            Assert.That(randomAcc.Balance, Is.EqualTo(100000));
        }

        [Test]
        public void DepositInvalidAmount()
        {
            //Arrange
            var randomCust = new Customer("tom", "riddle", "tom@riddle.com", "tom", "marvolo");
            var randomAcc = new BankAccount(randomCust, "savings", 2000);
            
            //Assert
            Assert.That(() => randomAcc.MakeDeposit(0, DateTime.Now, "depositing invalid amount"), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        // TESTS FOR WITHDRAWAL METHOD
        [Test]
        public void WithdrawValidAmount()
        {
            //Arrange
            var randomCust = new Customer("tom", "riddle", "tom@riddle.com", "tom", "marvolo");
            var randomAcc = new BankAccount(randomCust, "savings", 72000);
            //Act
            randomAcc.MakeWithdrawal(28000, DateTime.Now, "testing valid withdraw method");
            //Assert
            Assert.That(randomAcc.Balance, Is.EqualTo(44000));
        }

        [Test]
        public void WithdrawInvalidAmount()
        {
            //Arrange
            var randomCust = new Customer("tom", "riddle", "tom@riddle.com", "tom", "marvolo");
            var randomAcc = new BankAccount(randomCust, "current", 72000);
            
            //Assert
            Assert.That(() => randomAcc.MakeWithdrawal(100000, DateTime.Now, "saving for new wand"), Throws.TypeOf<InvalidOperationException>());
        }

        // TEST FOR TRANSFER METHOD
        [Test]
        public void TransferTestTo()
        {
            //Arrange
            var randomCust = new Customer("tom", "riddle", "tom@riddle.com", "tom", "marvolo");
            var randomAcc = new BankAccount(randomCust, "savings", 72000);
            var randomCust2 = new Customer("mason", "greenwood", "mason@manutd.com", "mason", "greenwood");
            var randomAcc2 = new BankAccount(randomCust2, "current", 95000000);
            //Act
            randomAcc2.TransferTo(randomAcc.AccNumber,1000000,DateTime.Now,"gift");
            //Assert
            Assert.That(randomAcc.Balance, Is.EqualTo(1072000));
        }

        [Test]
        public void TransferTestFrom()
        {
            //Arrange
            var randomCust = new Customer("tom", "riddle", "tom@riddle.com", "tom", "marvolo");
            var randomAcc = new BankAccount(randomCust, "savings", 72000);
            var randomCust2 = new Customer("mason", "greenwood", "mason@manutd.com", "mason", "greenwood");
            var randomAcc2 = new BankAccount(randomCust2, "current", 95000000);
            //Act
            randomAcc2.TransferTo(randomAcc.AccNumber, 1000000, DateTime.Now, "gift");
            //Assert
            Assert.That(randomAcc2.Balance, Is.EqualTo(94000000));
        }

        // TEST FOR TRANSACTION
        [Test]
        public void TransactionTest()
        {
            //Act
            var prevTxnCount = Bank.allTransactions.Count;
            var txn = new Transaction(1000, DateTime.Now, "test txn");
            var currentTxnCount = Bank.allTransactions.Count;
            //Assert
            Assert.That(currentTxnCount, Is.EqualTo(prevTxnCount+1));
        }
    }
}
