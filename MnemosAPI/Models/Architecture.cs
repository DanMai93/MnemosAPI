namespace MnemosAPI.Models
{
    public class Architecture
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    }
}
