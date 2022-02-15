using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Entities.WalletAggregate;

public class Wallet : AuditableEntity<Guid>, IHasDomainEvent
{
    private Wallet() { }
   
    public Balance Balance { get; private set; } = new();
    public List<DomainEvent> DomainEvents { get; set; } = new();

    
    public Guid UserId { get; set; }

    public List<Transaction> Transactions { get; private set; } = new();

    //public void AddWithdrawalRequest(Amount amount)
    //{
    //    var transaction = RequestAggregate.Request.CreateWithdrawalRequest(amount, BankAccount);
    //    Requests.Add(transaction);
    //}

    //public void Charge(Transaction transaction)
    //{
    //    Transactions.Add(transaction);
    //    Balance.IncreaseWalletBalance(transaction.Amount);
    //}
    //public void Withdrawal(Transaction transaction)
    //{
    //    Transactions.Add(transaction);
    //    Balance.ReduceWalletBalance(transaction.Amount);
    //}

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
