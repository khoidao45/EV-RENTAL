using System.ComponentModel.DataAnnotations;

namespace EVStation_basedRendtalSystem.Services.StationAPI.DTOs
{
    public class CreateStationRequestDto
    {
        [Required(ErrorMessage = "Station name is required")]
        [MaxLength(200, ErrorMessage = "Station name cannot exceed 200 characters")]
        public string StationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(500, ErrorMessage = "Address cannot exceed 500 characters")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        public string City { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Province cannot exceed 100 characters")]
        public string? Province { get; set; }

        [MaxLength(20, ErrorMessage = "Postal code cannot exceed 20 characters")]
        public string? PostalCode { get; set; }

        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string? PhoneNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }

        [Range(0, 10000, ErrorMessage = "Total parking slots must be between 0 and 10000")]
        public int TotalParkingSlots { get; set; } = 0;

        [Range(0, 10000, ErrorMessage = "Available slots must be between 0 and 10000")]
        public int AvailableSlots { get; set; } = 0;

        [MaxLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string Status { get; set; } = "Active";

        [MaxLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string? Description { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
        public double? Latitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
        public double? Longitude { get; set; }

        public TimeSpan? OpeningTime { get; set; }

        public TimeSpan? ClosingTime { get; set; }

        public bool IsOpen24Hours { get; set; } = false;
    }
}

