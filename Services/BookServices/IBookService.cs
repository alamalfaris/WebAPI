using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities;
using WebAPI.Request.Book;
using WebAPI.Responses;

namespace WebAPI.Services.BookServices
{
    public interface IBookService
    {
        Task<BaseResponse<Book>> Insert(Book book);
        Task<BaseResponse<Book>> Update (Book book);
        Task<SearchResponse<Book>> Search(BookRequest request);
        Task<SearchResponse<Book>> SearchWithSP(BookRequest request);
        Task<BaseResponse<Book>> Delete(int id);
    }
}
