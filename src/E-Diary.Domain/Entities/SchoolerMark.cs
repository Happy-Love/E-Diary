namespace E_Diary.Domain.Entities
{
    public class SchoolerMark
    {
        /// <summary>
        /// The schooler mark identifier.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The schooler identifier.
        /// </summary>
        public int SchoolerId { get; set; }

        /// <summary>
        /// The schooler.
        /// </summary>
        public Schooler? Schooler { get; set; }

        /// <summary>
        /// The subject identifier.
        /// </summary>
        public int SubjectId { get; set; }

        /// <summary>
        /// The subject.
        /// </summary>
        public Subject? Subject { get; set; }

        /// <summary>
        /// The created at date.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; set; }

        /// <summary>
        /// The score.
        /// </summary>
        public int? Score { get; set; }
    }
}
