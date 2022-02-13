namespace E_Diary.Domain.Entities
{
    public class Schooler
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The middle name.
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string? LastName { get; set; }
        
        /// <summary>
        /// The group.
        /// </summary>
        public Group? Group { get; set; }

        /// <summary>
        /// The group identifier. 
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// The list of schooler marks.
        /// </summary>
        public ICollection<SchoolerMark> SchoolerMarks { get; set; } = new List<SchoolerMark>();
    }
}
