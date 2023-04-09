using WebAPI.Entities;
using WebAPI.Helper;
using WebAPI.Request.Book;
using WebAPI.Responses;

namespace WebAPI.Services.BookServices
{
    public class BookService : IBookService
    {
        private readonly DataContext _context;
        private readonly ILogger<BookService> _logger;

        public BookService(DataContext context, ILogger<BookService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<BaseResponse<Book>> Insert(Book book)
        {
            BaseResponse<Book> response = new BaseResponse<Book>();

            var checkMandatory = ValidationUtils.CheckMandatory(book);
            if (!checkMandatory.isValid)
            {
                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG,
                    checkMandatory.message));

                response.IsSuccess = false;
                response.Message = checkMandatory.message;
                return response;
            }

            CommonHelper.SetInsert(book, "admin");

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            response.Data = book;
            response.Message = Constants.SUCCESS_MESSAGE;

            _logger.LogInformation(String.Format(Constants.INSERT_SUCCESS_MESSAGE_LOG, "Book", book.Id));

            return response;
        }

        public async Task<BaseResponse<Book>> Update(Book book)
        {
            BaseResponse<Book> response = new BaseResponse<Book>();

            var checkMandatory = ValidationUtils.CheckMandatory(book);
            if (!checkMandatory.isValid)
            {
                response.IsSuccess = false;
                response.Message = checkMandatory.message;

                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG,
                    checkMandatory.message));

                return response;
            }

            var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            if (dbBook == null)
            {
                response.IsSuccess = false;
                response.Message = String.Format(Constants.NOTFOUND_MESSAGE, "Book");

                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG,
                    response.Message));

                return response;
            }

            dbBook.Title = book.Title;
            dbBook.Author = book.Author;
            dbBook.PublishedYear = book.PublishedYear;
            CommonHelper.SetUpdate(dbBook, "admin");

            await _context.SaveChangesAsync();

            response.Data = dbBook;
            response.Message = Constants.SUCCESS_MESSAGE;

            return response;
        }

        public async Task<SearchResponse<Book>> Search(BookRequest request)
        {
            var checkPage = ValidationUtils.CheckPage(request);
            if (!checkPage.isValid)
            {
                _logger.LogInformation(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, checkPage.message));

                return new SearchResponse<Book>() { Message = checkPage.message, IsSuccess = false };
            }

            var totalData = _context.Books.Where(b => !b.IsDeleted).Count();
            var totalPage = Math.Ceiling(totalData / (float)request.PageSize);

            var books = await _context.Books
                .Where(b => b.Title.ToLower().Contains(request.Title.ToLower()) &&
                    b.Author.ToLower().Contains(request.Author.ToLower()) &&
                    b.PublishedYear.Contains(request.PublishedYear) &&
                    !b.IsDeleted)
                .OrderByDescending(b => b.CreatedTime)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            _logger.LogInformation(String.Format(Constants.SUCCESS_MESSAGE_LOG, "Search success"));

            return CommonHelper.GenerateSearchResponse(books, request, (int)totalPage, totalData);
        }

        public async Task<BaseResponse<Book>> Delete(int id)
        {
            BaseResponse<Book> response = new BaseResponse<Book>();

            var dbBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (dbBook == null)
            {
                response.Message = String.Format(Constants.NOTFOUND_MESSAGE, "Book");
                response.IsSuccess = false;

                _logger.LogError(Constants.BAD_REQUEST_MESSAGE_LOG, response.Message);

                return response;
            }

            CommonHelper.SetDelete(dbBook, "admin");

            await _context.SaveChangesAsync();

            response.Message = Constants.SUCCESS_MESSAGE;
            response.Data = dbBook;

            _logger.LogInformation(String.Format(Constants.SUCCESS_MESSAGE_LOG, "Delete success"));

            return response;
        }

        public async Task<SearchResponse<Book>> SearchWithSP(BookRequest request)
        {
            var checkPage = ValidationUtils.CheckPage(request);
            if (!checkPage.isValid)
            {
                _logger.LogInformation(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, checkPage.message));

                return new SearchResponse<Book>() { Message = checkPage.message, IsSuccess = false };
            }

            var totalData = _context.Books.Where(b => !b.IsDeleted).Count();
            var totalPage = Math.Ceiling(totalData / (float)request.PageSize);

            var books = await _context.Books
                .FromSqlRaw("SelectAllBooks")
                //.Skip((request.PageNumber - 1) * request.PageSize)
                //.Take(request.PageSize)
                .ToListAsync();

            _logger.LogInformation(String.Format(Constants.SUCCESS_MESSAGE_LOG, "Search success"));

            return CommonHelper.GenerateSearchResponse(books, request, (int)totalPage, totalData);
        }
    }
}
