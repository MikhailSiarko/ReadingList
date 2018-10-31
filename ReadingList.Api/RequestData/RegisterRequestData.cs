namespace ReadingList.Api.RequestData
{
    public class RegisterRequestData : LoginRequestData
    {
        public string ConfirmPassword { get; set; }
    }
}