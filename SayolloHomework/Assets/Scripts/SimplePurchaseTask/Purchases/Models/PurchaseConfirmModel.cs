using System;

namespace AndriiYefimov.SayolloHW2.Purchases.Models
{
    [Serializable]
    public class PurchaseConfirmModel
    {
        public string Email;
        public string CardNumber;
        public string ExpirationDate;
    }
}