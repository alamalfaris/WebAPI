namespace WebAPI.Helper
{
    public class Constants
    {
        public const string SUCCESS_MESSAGE = "success";
        public const string ERROR_MESSAGE = "error";

        public const string MANDATORY_MESSAGE = "{0} must be filled";
        public const string PAGE_SIZE_VALIDATION_MESSAGE = "Page Size must filled and more than 0";
        public const string PAGE_NUMBER_VALIDATION_MESSAGE = "Page Number must filled and more than 0";
        public const string NOTFOUND_MESSAGE = "{0} not found";

        public const string INSERT_SUCCESS_MESSAGE_LOG = "200 OK: Insert {0} with id/code {1} success";
        public const string BAD_REQUEST_MESSAGE_LOG = "400 BAD REQUEST: {0}";
        public const string SUCCESS_MESSAGE_LOG = "200 OK: {0}";
    }
}
