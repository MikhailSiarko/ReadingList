namespace ReadingList.Api.RequestData
{
    public struct RegisterRequestData
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}