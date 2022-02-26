using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.WalletAggregate;

public class Wallet : AuditableEntity<Guid>, IHasDomainEvent
{
    private Wallet() { }
    public Wallet(string userId, string title)
    {
        IdentityUser = userId;
        Title = title;
    }

    public Balance Balance { get; private set; } = new();
    public List<DomainEvent> DomainEvents { get; set; } = new();


    public string Title { get; set; }
    public string IdentityUser { get; set; }

    public bool IscActive { get; set; }

    public List<Transaction> Transactions { get; private set; } = new();

    public void ChangeStateWallet(bool status)
    {
        IscActive = status;
    }
    //public void AddWithdrawalRequest(Amount amount)
    //{
    //    var transaction = RequestAggregate.Request.CreateWithdrawalRequest(amount, BankAccount);
    //    Requests.Add(transaction);
    //}

    public void AddTransactiom(Transaction transaction)
    {
        Transactions.Add(transaction);
        if (transaction.TransactionType == TransactionType.Withdrawal)
            Withdrawal(transaction.Amount);
        else if (transaction.TransactionType == TransactionType.Deposit)
            Charge(transaction.Amount);
    }
    public void updateBalance(Amount oldAmount,Amount newAmount, TransactionType transactionType)
    {
        if (transactionType == TransactionType.Withdrawal)
        {
            Withdrawal(newAmount);
            Charge(oldAmount);
        }
        else if (transactionType == TransactionType.Deposit)
        {
            Withdrawal(oldAmount);
            Charge(newAmount);
        }
    }
    public void RefactorTransaction(Amount amount, TransactionType transactionType)
    {
        if (transactionType == TransactionType.Withdrawal)
        {
            Charge(amount);
        }
        else if (transactionType == TransactionType.Deposit)
        {
            Withdrawal(amount);
        }
    }
    public void ChangeTransactionType(Amount amount, TransactionType transactionType)
    {
        if (transactionType == TransactionType.Withdrawal)
        {
            Withdrawal(amount);
            Withdrawal(amount);
        }
        else if (transactionType == TransactionType.Deposit)
        {
            Charge(amount);
            Charge(amount);
        }
    }
    public void ChangeTransactionTypeAndAmount(Amount oldAmount, Amount newAmount, TransactionType transactionType)
    {
        //زمانی که درخواست واریز بوده اول قیمت قدیم رو کم میکنیم بعد قیمت جدید را
        if (transactionType == TransactionType.Withdrawal)
        {
            Withdrawal(oldAmount);
            Withdrawal(newAmount);
        }
        //زمانی که درخواست برداشت بود اول قیمت قدیم را اضافه میکنیم بعد جدید رو
        else if (transactionType == TransactionType.Deposit)
        {
            Charge(newAmount);
            Charge(oldAmount);
        }
    }

    private void Charge(Amount amount)
    {
        Balance.IncreaseWalletBalance(amount);
    }
    private void Withdrawal(Amount amount)
    {
        Balance.ReduceWalletBalance(amount);
    }

    //public void AddTransactionReceiver(Transaction transaction)
    //{
    //    Transactions.Add(transaction);
    //    Balance.IncreaseWalletBalance(transaction.Amount);
    //}

    //public void AddTransactionTransmitter(Transaction transaction)
    //{
    //    Transactions.Add(transaction);
    //    Balance.ReduceWalletBalance(transaction.Amount);
    //}
    //public void CreatePaymentRequest(PaymentAggregate.Payment payment)
    //{
    //    Payments.Add(payment);
    //}

}
