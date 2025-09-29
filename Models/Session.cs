using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;

namespace SmithSwimmingSchool.Models
{
    public class Session
    {
        public int SessionId { get; set; }

        [Display(Name = "Seat Capacity")]
        public int SeatCapacity { get; set; }

        [Display(Name = "Start Time")]
        public DateTimeOffset DailyStartTime { get; set; }

        [Display(Name = "Start Date")]
        public DateTimeOffset StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTimeOffset EndDate { get; set; }

        // Navigation properties
    }
}
