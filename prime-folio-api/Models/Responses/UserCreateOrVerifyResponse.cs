namespace Models.Responses
{
    public class UserCreateOrVerifyResponse
    {
        public bool IsPrimeFolioVerified { get; set; }

        public bool IsCreated { get; set; }

        public User User { get; set; }
    }
}
