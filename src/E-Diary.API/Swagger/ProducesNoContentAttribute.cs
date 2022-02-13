using Microsoft.AspNetCore.Mvc;

namespace E_Diary.API.Swagger
{
    public class ProducesNoContentAttribute : ProducesResponseTypeAttribute
    {
        public ProducesNoContentAttribute() : base(StatusCodes.Status204NoContent)
        {
        }
    }
}
