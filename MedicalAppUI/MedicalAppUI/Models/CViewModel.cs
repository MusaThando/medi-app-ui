namespace MedicalAppUI.Models
{
    public class CViewModel
    {
        public Nullable<Guid> DocumentRepositoryGUIDfld { get; set; }
        public Nullable<Guid> SourceRecordGUIDfld { get; set; }
        public Nullable<Guid> PrimaryDoctorGUIDfld { get; set; }
        public Nullable<Guid> UserTypeGUIDfld { get; set; }
        public Nullable<Guid> PracticeGUIDfld { get; set; }
        public Nullable<Guid> PersonGUIDfld { get; set; }
        public Nullable<Guid> NextOfKinGUIDfld { get; set; }
        public string FullNamefld { get; set; }
        public string FirstNamefld { get; set; }
        public string Authorise { get; set; }
        public string Typefld { get; set; }
        public Guid DiagnosisGUIDfld { get; set; }
        public string DiagnosisDescriptionfld { get; set; }
        public string ICD10Codefld { get; set; }
        public string NextOfKinLastNamefld { get; set; }
        public string NextOfKinContactfld { get; set; }
        public string NextOfKinFirstNamefld { get; set; }
        public string LastNamefld { get; set; }
        public string Contactfld { get; set; }
        public string Emailfld { get; set; }
        public string IDNumberfld { get; set; }
        public string Addressfld { get; set; }
        public string PracticeAddressfld { get; set; }
        public string Usernamefld { get; set; }
        public string Passwordfld { get; set; }
        public string PasswordCheck { get; set; }
        public string RegistrationNumberfld { get; set; }
        public string PracticeRegistration { get; set; }
        public string PracticeNamefld { get; set; }
        public string ConfirmPasswordfld { get; set; }
        public string MedicalAidPlanfld { get; set; }
        public string MedicalAidNumberfld { get; set; }
        public string MedicalAidEmailfld { get; set; }
        public string MedicalAidContactfld { get; set; }
        public List<CViewModel> UsersList { get; set; }
        public List<CViewModel> DoctorList { get; set; }
        public List<CViewModel> AdminList { get; set; }
        public List<CViewModel> Transations { get; set; }
        public List<CViewModel> DiseaseList { get; set; }

        public int IDfld { get; set; }

        public string FileNamefld { get; set; }
        public string FileTypefld { get; set; }
        public byte[]? FileContentfld { get; set; }
        public bool Publicfld { get; set; }

        public CViewModel()
        {
            UsersList = new List<CViewModel>();
            DoctorList = new List<CViewModel>();
            AdminList = new List<CViewModel>();
            Transations = new List<CViewModel>();
            DiseaseList = new List<CViewModel>();

            PasswordCheck = "";
            MedicalAidEmailfld = "";
            MedicalAidPlanfld = "";
            MedicalAidNumberfld = "";
            MedicalAidContactfld = "";
            PracticeAddressfld = "";
            PracticeRegistration = "";
            PracticeNamefld = "";
            NextOfKinContactfld = "";
            NextOfKinFirstNamefld = "";
            NextOfKinLastNamefld = "";
            ICD10Codefld = "";
            DiagnosisDescriptionfld = "";

            Typefld = "";
            Authorise = "";

            FullNamefld = "";
            FirstNamefld = "";
            LastNamefld = "";
            Contactfld = "";
            IDNumberfld = "";
            Addressfld = "";
            Emailfld = "";
            RegistrationNumberfld = "";
            Publicfld = false;

            FileTypefld = "";
            FileNamefld = "";
            IDfld = 0;

            Usernamefld = "";
            Passwordfld = "";
            ConfirmPasswordfld = "";
        }

    }
}
