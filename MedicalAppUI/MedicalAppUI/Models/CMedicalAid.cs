namespace MedicalAppUI.Models
{
    public class CMedicalAid
    {
        public string MedicalAidEmailfld { get; set; }
        public string MedicalAidPlanfld { get; set; }
        public string MedicalAidNumberfld { get; set; }
        public string MedicalAidContactfld { get; set; }
        public Nullable<Guid> MedicalAidGUIDfld { get; set; }
        public Nullable<Guid> PersonGUIDfld { get; set; }

        public CMedicalAid()
        {
            MedicalAidEmailfld = "";
            MedicalAidPlanfld = "";
            MedicalAidNumberfld = "";
            MedicalAidContactfld = "";
        }

    }
}
