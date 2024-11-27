namespace PatientManagerServices.Dtos;

public class PrescriptionDto
{
        public string Guid { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string MedicalHistoryGuid { get; set; }
        public string? IllnessGuid { get; set; }
}