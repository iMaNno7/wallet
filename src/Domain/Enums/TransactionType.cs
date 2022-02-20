using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Enums;
public enum TransactionType
{
    [Display(Name = "واریز")]
    Deposit = 0,
    [Display(Name = "برداشت")]
    Withdrawal = 1
}

