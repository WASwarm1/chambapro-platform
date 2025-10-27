namespace ChambaPro.Platform.API.Service.Domain.Model.Aggregates
{
    public class Reserve
    {
        public string Id { get; private set; }
        public DateTime Date { get; private set; }
        public TimeSpan Time { get; private set; }
        public string Description { get; private set; }
        public string Client { get; private set; }
        public string Category { get; private set; }
        public string Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? TechnicianId { get; private set; }
        public string? ClientId { get; private set; }

        public Reserve(string id, DateTime date, TimeSpan time, string description, 
            string client, string category, string? technicianId = null, 
            string? clientId = null)
        {
            Id = id;
            Date = date;
            Time = time;
            Description = description;
            Client = client;
            Category = category;
            Status = "active";
            CreatedAt = DateTime.UtcNow;
            TechnicianId = technicianId;
            ClientId = clientId;
        }

        public void Update(string description, string category, DateTime? date = null, TimeSpan? time = null)
        {
            Description = description;
            Category = category;
            Date = date ?? Date;
            Time = time ?? Time;
        }

        public void Cancel()
        {
            Status = "cancelled";
        }

        public void AssignTechnician(string technicianId)
        {
            TechnicianId = technicianId;
        }
    }
}