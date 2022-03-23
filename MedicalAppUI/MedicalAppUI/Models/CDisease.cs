namespace MedicalAppUI.Models
{
    public class CDisease
    {
        public Guid DiagnosisGUIDfld { get; set; }
        public Guid PatientGUIDfld { get; set; }
        public Guid DoctorGUIDfld { get; set; }
        public Guid TransationGUIDfld { get; set; }

        public string DiagnosisDescriptionfld { get; set; }
        public string ICD10Codefld { get; set; }
        public string TreatmentDescriptionfld { get; set; }
        public string PatientFullName { get; set; }
        public string DateCreatedfld { get; set; }
        public string DoctorFullName { get; set; }
        public string Costfld { get; set; }
   
        public List<CDisease> DiseaseList { get; set; }

        public CDisease()
        {
            DiagnosisDescriptionfld = "";
            DateCreatedfld = "";
            ICD10Codefld = "";
            PatientFullName = "";
            DiseaseList = new List<CDisease>();
            TreatmentDescriptionfld = "";
            DoctorFullName = "";
            Costfld = "";

        }
    }
}
