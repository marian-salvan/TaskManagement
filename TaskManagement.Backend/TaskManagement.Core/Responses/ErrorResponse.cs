namespace TaskManagement.Core.Responses
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        //can be used for ui localization
        public string Code { get; set; }
    }
}
