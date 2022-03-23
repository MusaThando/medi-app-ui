namespace MedicalAppUI.Models
{
    public class CUsers : CMedicalAid
    {
        public Nullable<Guid> UserTypeGUIDfld { get; set; }
        public string FullNamefld { get; set; }
        public string FirstNamefld { get; set; }
        public string LastNamefld { get; set; }
        public string Contactfld { get; set; }
        public string IDNumberfld { get; set; }
        public string Addressfld { get; set; }
        public string Emailfld { get; set; }

        //Medical Aid Properties
        //public string MedicalAidPlanfld { get; set; }
        //public string MedicalAidNumberfld { get; set; }
        //public string MedicalAidEmailfld { get; set; }
        //public string MedicalAidContactfld { get; set; }


        public int IDfld { get; set; }
        public DateTime DateTimeLastUpdatedfld { get; set; }

        public List<CUsers> UsersList { get; set; }
        public List<CUsers> AdminList { get; set; }
        public List<CUsers> DoctorList { get; set; }
        public object PrimaryDoctorGUIDfld { get; internal set; }

        public CUsers()
        {
            UsersList = new List<CUsers>();
            AdminList = new List<CUsers>();
            DoctorList = new List<CUsers>();
            FullNamefld = "";
            FirstNamefld = "";
            LastNamefld = "";
            Contactfld = "";
            IDNumberfld = "";
            Addressfld = "";
            Emailfld = "";
            MedicalAidContactfld = "";
            MedicalAidEmailfld = "";
            MedicalAidNumberfld = "";
            MedicalAidPlanfld = "";

        }

    }
}
