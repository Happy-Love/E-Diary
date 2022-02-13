using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Swagger
{
    public class ProducesCreatedAttribute : ProducesResponseTypeAttribute
    {
        public ProducesCreatedAttribute(Type responseType) : base(responseType, StatusCodes.Status201Created)
        {
        }
    }
}
