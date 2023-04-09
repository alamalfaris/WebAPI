using WebAPI.Entities;
using WebAPI.Request.Student;
using WebAPI.Responses;

namespace WebAPI.Services.StudentServices
{
    public interface IStudentService
    {
        Task<SearchResponse<Student>> Search(StudentRequest request);
        Task<BaseResponse<Student>> Insert(Student student);
        Task<BaseResponse<Student>> Update(Student student);
        Task<BaseResponse<Student>> Delete(int id);
    }
}
