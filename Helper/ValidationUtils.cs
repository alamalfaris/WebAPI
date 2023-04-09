using WebAPI.Entities;
using WebAPI.Request;
using WebAPI.Responses;

namespace WebAPI.Helper
{
    public class ValidationUtils
    {
        public static (bool isValid, string message) CheckPage(CommonRequest request)
        {
            if (request.PageNumber < 1)
            {
                return (false, Constants.PAGE_NUMBER_VALIDATION_MESSAGE);
            }
            if (request.PageSize < 1)
            {
                return (false, Constants.PAGE_SIZE_VALIDATION_MESSAGE);
            }

            return (true, "");
        }

        public static (bool isValid, string message) CheckMandatory(Student student)
        {
            if (String.IsNullOrEmpty(student.FirstName) || String.IsNullOrWhiteSpace(student.FirstName))
            {
                return (false, String.Format(Constants.MANDATORY_MESSAGE,
                    "First Name"));
            }
            if (String.IsNullOrEmpty(student.LastName) || String.IsNullOrWhiteSpace(student.LastName))
            {
                return (false, String.Format(Constants.MANDATORY_MESSAGE,
                    "Last Name"));
            }
            if (String.IsNullOrEmpty(student.Major) || String.IsNullOrWhiteSpace(student.Major))
            {
                return (false, String.Format(Constants.MANDATORY_MESSAGE,
                    "Major"));
            }

            return (true, "");
        }

        public static (bool isValid, string message) CheckMandatory(Book book)
        {
            if (String.IsNullOrEmpty(book.Title) || String.IsNullOrWhiteSpace(book.Title))
            {
                return (false, String.Format(Constants.MANDATORY_MESSAGE,
                    "Title"));
            }
            if (String.IsNullOrEmpty(book.Author) || String.IsNullOrWhiteSpace(book.Author))
            {
                return (false, String.Format(Constants.MANDATORY_MESSAGE,
                    "Author"));
            }
            if (String.IsNullOrEmpty(book.PublishedYear) || String.IsNullOrWhiteSpace(book.PublishedYear))
            {
                return (false, String.Format(Constants.MANDATORY_MESSAGE,
                    "Published Year"));
            }

            return (true, "");
        }
    }
}
