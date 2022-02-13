namespace E_Diary.Domain.Entities
{
    public class Subject
    {
        /// <summary>
        /// The subject identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of subject.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The marks of schooler.
        /// </summary>
        public ICollection<SchoolerMark> SchoolerMarks { get; set; } = new List<SchoolerMark>();
    }
}
