namespace E_Diary.API.Filters.ExceptionFilters
{
    public class NotFoundOnExceptionAttribute : HttpStatusCodeOnExceptionAttribute
    {
        public NotFoundOnExceptionAttribute(params Type[] exceptionTypes)
            : base(StatusCodes.Status404NotFound, exceptionTypes)
        {
        }
    }
}
