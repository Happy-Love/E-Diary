using System.Runtime.Serialization;

namespace E_Diary.Core.Exceptions
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }

        protected ValidationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
