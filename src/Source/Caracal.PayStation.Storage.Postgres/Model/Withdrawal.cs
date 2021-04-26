using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caracal.PayStation.Storage.Postgres.Model {
    [Table("withdrawals")]
    public class Withdrawal {
        [Key]
        [Required]
        [Column("id")]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        [Column("account")]
        public string Account { get; set; }
        
        
        [Column("amount")]
        [MaxLength(100)]
        public string Amount { get; set; }
        
        [MaxLength(32)]
        [Column("status")]
        public string Status { get; set; }
    }
}