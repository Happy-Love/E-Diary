using E_Diary.API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Swagger
{
    public class ProducesBadRequestAttribute : ProducesResponseTypeAttribute
    {
        public ProducesBadRequestAttribute() : base(typeof(ErrorResult), StatusCodes.Status400BadRequest)
        {
        }
    }
}
