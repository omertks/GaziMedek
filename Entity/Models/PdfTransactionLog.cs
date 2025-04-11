using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class PdfTransactionLog : BaseLog
    {

        public int UserId { get; set; }

        public TransactionType TransactionType;

        public DateTime Date { get; set; }
    
    }
}
