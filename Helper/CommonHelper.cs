using WebAPI.Entities;
using WebAPI.Request;
using WebAPI.Responses;

namespace WebAPI.Helper
{
    public class CommonHelper
    {
        public static string ConvertForLike(string value)
        {
            return value != null? value : string.Empty;
        }

        public static void SetErrorStudent(BaseResponse<Student> response, string message)
        {
            response.Message = message;
            response.IsSuccess = false;
            response.Data = null;
        }

        public static void SetInsert(CommonEntity entity, string user)
        {
            entity.CreatedBy = user;
            entity.CreatedTime = DateTime.Now;
        }

        public static void SetUpdate(CommonEntity entity, string user)
        {
            entity.UpdatedBy = user;
            entity.UpdatedTime = DateTime.Now;
        }


        public static void SetDelete(CommonEntity entity, string user)
        {
            entity.IsDeleted = true;
            entity.UpdatedBy = user;
            entity.UpdatedTime = DateTime.Now;
        }

        public static SearchResponse<T> GenerateSearchResponse<T>(List<T> data, 
            CommonRequest request, int totalPage, int totalData)
        {
            SearchResponse<T> response = new SearchResponse<T>();
            
            response.Data = data;
            response.Message = Constants.SUCCESS_MESSAGE;
            response.PageNumber = request.PageNumber;
            response.PageSize = request.PageSize;
            response.TotalData = totalData;
            response.TotalPage = (int)totalPage;
            response.TotalDataInPage = data.Count();

            return response;
        }
    }
}
