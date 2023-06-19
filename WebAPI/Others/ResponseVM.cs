using System.Net;

namespace WebAPI.Others
{
    public class ResponseMessageVM<TEntity>
    {
        public  int Code { get; set; }
        public  string Status { get; set; }
        public  string Message { get; set; }
        public  TEntity? Data { get; set; }

        public ResponseMessageVM<TEntity> Success(TEntity entity)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = entity
            };
        }

        public ResponseMessageVM<TEntity> Success(string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = msg
            };
        }

        public ResponseMessageVM<TEntity> Success(TEntity entity, string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = msg,
                Data = entity
            };
        }

        public ResponseMessageVM<TEntity> Failed(TEntity entity, string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = msg,
                Data = entity
            };
        }

        public ResponseMessageVM<TEntity> Failed(string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = msg
            };
        }

        public ResponseMessageVM<TEntity> NotFound(string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = msg
            };
        }

        public ResponseMessageVM<TEntity> NotFound(TEntity entity)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found",
                Data = entity
            };
        }

        public ResponseMessageVM<TEntity> NotFound(TEntity entity, string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = msg,
                Data = entity
            };
        }

        public ResponseMessageVM<TEntity> Error(string msg)
        {
            return new ResponseMessageVM<TEntity>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = msg
            };
        }
    }
}
