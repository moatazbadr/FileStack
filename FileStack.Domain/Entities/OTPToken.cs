using FileStack.Domain.Constants;

namespace FileStack.Domain.Entities
{
    public class OTPToken
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string CodeHash { get; set; }

        public DateTime ExpirationTime { get; set; }
        public bool IsUsed { get; set; }
        public int AttemptCount { get; set; }
        public DateTime CreatedAt { get; set; }

        public PurposeOTP PurposeOfOTP { get; set; }
    }
}
