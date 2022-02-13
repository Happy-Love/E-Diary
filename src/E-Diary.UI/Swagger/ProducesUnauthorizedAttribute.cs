using E_Diary.API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Swagger
{
    public class ProducesUnauthorizedAttribute : ProducesResponseTypeAttribute
    {
        public ProducesUnauthorizedAttribute() : base(typeof(ErrorResult), StatusCodes.Status401Unauthorized)
        {
        }
    }
}
