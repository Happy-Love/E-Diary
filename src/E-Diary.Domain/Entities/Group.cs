namespace E_Diary.Domain.Entities
{
    public class Group
    {
        /// <summary>
        /// The group identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The group name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The list of schoolers.
        /// </summary>
        public ICollection<Schooler> Schoolers { get; set; } = new List<Schooler>();
    }
}
