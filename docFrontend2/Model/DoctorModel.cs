namespace docFrontend.Model // Ensure this matches the folder name
{
    public class Doctor
    {
        public string Name { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public double ConsultationFee { get; set; } // Change to double
    }
}