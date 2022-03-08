using System.Runtime.Serialization;

namespace E_Diary.Core.Exceptions
{
    [Serializable]
    public class OperationNotAllowedException : Exception
    {
        public OperationNotAllowedException(string? reason) : base($"Operation can't be approved by billing service, because:\n {reason}")
        {
        }

        protected OperationNotAllowedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
