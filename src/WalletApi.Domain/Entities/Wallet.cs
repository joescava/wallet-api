namespace WalletApi.Domain.Entities;

public class WalletApi{
    public int id {get; set; }
    public string DocumentId {get; set; } = default;
    public string Name {get; set; } = default;
    public decimal Balance {get; set; }
    public DateTime CreatedAt {get; set; }
    public DateTime UpdateAt {get; set; }

    public ICollection<TransactionHistory> Transactions {get; set; } = new List<TransactionHistory>();
}