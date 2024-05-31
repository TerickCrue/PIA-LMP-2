namespace PIA_LMP_API.Data.Helpers
{
    public class CustomException : Exception
    {
        public CustomException(string errorMessage) {
            ErrorMessage = errorMessage;
        }
        public string ErrorMessage
        {
            get; set;
        }
    }
}
