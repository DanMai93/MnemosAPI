namespace MnemosAPI.Models
{
    public class EndCustomer
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Notes { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
