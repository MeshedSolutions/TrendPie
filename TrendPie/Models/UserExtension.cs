namespace TrendPie.Models
{
    public partial class User
    {
        public string ConfirmPassword { get; set; }
        public bool Agree { get; set; }
        public bool ExistingCustomer { get; set; }
        public int CurrentAmountPerCampaign { get; set; }
    }
}