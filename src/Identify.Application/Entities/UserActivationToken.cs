namespace Identify.Application.Entities
{
    public class UserActivationToken
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TokenId { get; set; }
        public OneTimeUseToken Token { get; set; }
    }
}
