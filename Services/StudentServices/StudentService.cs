using WebAPI.Entities;
using WebAPI.Helper;
using WebAPI.Request.Student;
using WebAPI.Responses;

namespace WebAPI.Services.StudentServices
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly ILogger<StudentService> _logger;

        public StudentService(DataContext context, ILogger<StudentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SearchResponse<Student>> Search(StudentRequest request)
        {
            var checkPage = ValidationUtils.CheckPage(request);
            if (!checkPage.isValid)
            {
                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, checkPage.message));

                return new SearchResponse<Student>() { Message = checkPage.message, IsSuccess = false };
                
            }

            List<Student> students = new List<Student>();

            var totalData = _context.Students.Where(s => !s.IsDeleted).Count();
            var totalPage = Math.Ceiling(totalData / (float)request.PageSize);

            students = await _context.Students
                            .Where(s => (s.FirstName.ToLower().Contains(request.Name.ToLower()) ||
                            s.LastName.ToLower().Contains(request.Name.ToLower())) &&
                            s.Major.ToLower().Contains(request.Major.ToLower()) && 
                            !s.IsDeleted)
                            .OrderByDescending(s => s.CreatedTime)
                            .Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToListAsync();

            _logger.LogInformation(String.Format(Constants.SUCCESS_MESSAGE_LOG, "Search success"));

            return CommonHelper.GenerateSearchResponse<Student>(students, request, 
                (int)totalPage, totalData);
        }

        public async Task<BaseResponse<Student>> Insert(Student student)
        {
            BaseResponse<Student> response = new BaseResponse<Student>();

            var checkMandatory = ValidationUtils.CheckMandatory(student);
            if (!checkMandatory.isValid)
            {
                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, checkMandatory.message));

                response.IsSuccess = false;
                response.Message = checkMandatory.message;
                return response;
            }

            CommonHelper.SetInsert(student, "admin");

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            response.Data = student;
            response.Message = Constants.SUCCESS_MESSAGE;

            _logger.LogInformation(String.Format(Constants.INSERT_SUCCESS_MESSAGE_LOG, 
                "Student", student.Id));
            
            return response;
        }

        public async Task<BaseResponse<Student>> Update(Student student)
        {
            BaseResponse<Student> response = new BaseResponse<Student>();

            var checkMandatory = ValidationUtils.CheckMandatory(student);
            if (!checkMandatory.isValid)
            {
                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, checkMandatory.message));

                response.IsSuccess = false;
                response.Message = checkMandatory.message;
                return response;
            }

            var dbStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == student.Id);

            if (dbStudent == null)
            {
                response.IsSuccess = false;
                response.Message = String.Format(Constants.NOTFOUND_MESSAGE, "Student");

                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, response.Message));
                
                return response;
            }

            dbStudent.FirstName = student.FirstName;
            dbStudent.LastName = student.LastName;
            dbStudent.Major = student.Major;
            CommonHelper.SetUpdate(dbStudent, "admin");

            await _context.SaveChangesAsync();

            response.Message = Constants.SUCCESS_MESSAGE;
            response.Data = dbStudent;

            return response;
        }

        public async Task<BaseResponse<Student>> Delete(int id)
        {
            BaseResponse<Student> response = new BaseResponse<Student>();

            var dbStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (dbStudent == null)
            {
                response.IsSuccess = false;
                response.Message = String.Format(Constants.NOTFOUND_MESSAGE, "Student");

                _logger.LogError(String.Format(Constants.BAD_REQUEST_MESSAGE_LOG, response.Message));
                
                return response;
            }

            CommonHelper.SetDelete(dbStudent, "admin");

            await _context.SaveChangesAsync();

            response.Message = Constants.SUCCESS_MESSAGE;
            response.Data = dbStudent;

            _logger.LogInformation(String.Format(Constants.SUCCESS_MESSAGE_LOG, "Delete success"));

            return response;
        }
    }
}
