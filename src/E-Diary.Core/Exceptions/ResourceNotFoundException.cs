using System.Runtime.Serialization;

namespace E_Diary.Core.Exceptions
{
    [Serializable]
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string resourceName) : base($"resource: {resourceName} not found")
        {
        }

        protected ResourceNotFoundException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
