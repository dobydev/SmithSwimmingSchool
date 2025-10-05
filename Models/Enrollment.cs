namespace SmithSwimmingSchool.Models
{
    public class Enrollment
    {
        // EF will use this as PK by convention
        public int EnrollmentId { get; set; }  

        // FKs
        public int SwimmerId { get; set; }
        public int SessionId { get; set; }

        public DateTimeOffset EnrolledAt { get; set; } = DateTimeOffset.UtcNow;

        // Navigation Properties
        public Swimmer? Swimmer { get; set; }
        public Session? Session { get; set; }
    }
}