using E_Diary.API.Dto;
using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Swagger
{
    public class ProducesNotFoundAttribute : ProducesResponseTypeAttribute
    {
        public ProducesNotFoundAttribute() : base(typeof(ErrorResult), StatusCodes.Status404NotFound)
        {
        }
    }
}
