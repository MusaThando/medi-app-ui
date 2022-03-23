namespace MedicalAppUI.Models
{
    public class CPractice
    {
        public Nullable<Guid> PracticeGUIDfld { get; set; }
        public string PracticeNamefld { get; set; }
        public string PracticeRegistration { get; set; }
        public string Addressfld { get; set; }

        public CPractice()
        {
            PracticeNamefld = "";
            PracticeRegistration = "";
            Addressfld = "";
        }
    }
}
