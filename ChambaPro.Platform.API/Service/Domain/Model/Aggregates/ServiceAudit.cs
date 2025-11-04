namespace ChambaPro.Platform.API.Service.Domain.Model.Aggregates;

public class ServicesAudit
{
    public int Id { get; private set; }
    public int ServiceId { get; private set; }
    public string Action { get; private set; }
    public string Description { get; private set; }
    public int PerformedBy { get; private set; } 
    public DateTime PerformedAt { get; private set; }
    public string OldValues { get; private set; }
    public string NewValues { get; private set; }

    public ServicesAudit(int serviceId, string action, string description, int performedBy, string oldValues = "", string newValues = "")
    {
        ServiceId = serviceId;
        Action = action;
        Description = description;
        PerformedBy = performedBy;
        PerformedAt = DateTime.UtcNow;
        OldValues = oldValues;
        NewValues = newValues;
    }

    private ServicesAudit() { }
}