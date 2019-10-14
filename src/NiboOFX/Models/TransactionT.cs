using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NiboOFX.Models
{
    [Table("TransactionT")]
    public class TransactionT
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual decimal Value { get; set; }
        public virtual string Description { get; set; }

        public TransactionT(string type, DateTime date, decimal value, string description)
        {
            this.Type = type;
            this.TransactionDate = date;
            this.Value = value;
            this.Description = description;
        }

        public TransactionT() { }

        
    }

    public class TransactionTComparer : IEqualityComparer<TransactionT>
    {
        public bool Equals(TransactionT x, TransactionT y)
        {
            return (x.Description.Trim().Equals(y.Description.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                (x.Type.Trim().Equals(y.Type.Trim(), StringComparison.InvariantCultureIgnoreCase)) &&
                (x.Value == y.Value) &&
                (x.TransactionDate.Date == y.TransactionDate.Date);
        }

        public int GetHashCode(TransactionT obj)
        {
            return obj.Description.Trim().GetHashCode();
        }
    }
}
