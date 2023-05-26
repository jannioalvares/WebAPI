using System.Net;

namespace WebAPI.Others
{
    public class ResponseVM<TEntity>
    {
        public  int Code { get; set; }
        public  string Status { get; set; }
        public  string Message { get; set; }
        public  TEntity? Data { get; set; }

        public ResponseVM<TEntity> Success(TEntity entity)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success",
                Data = entity
            };
        }

        public ResponseVM<TEntity> Success(string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = msg
            };
        }

        public ResponseVM<TEntity> Success(TEntity entity, string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = msg,
                Data = entity
            };
        }

        public ResponseVM<TEntity> Failed(TEntity entity, string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = msg,
                Data = entity
            };
        }

        public ResponseVM<TEntity> Failed(string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = msg
            };
        }

        public ResponseVM<TEntity> NotFound(string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = msg
            };
        }

        public ResponseVM<TEntity> NotFound(TEntity entity)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found",
                Data = entity
            };
        }

        public ResponseVM<TEntity> NotFound(TEntity entity, string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = msg,
                Data = entity
            };
        }

        public ResponseVM<TEntity> Error(string msg)
        {
            return new ResponseVM<TEntity>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = msg
            };
        }
    }
}
