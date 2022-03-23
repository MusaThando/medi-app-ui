namespace MedicalAppUI.Models
{
    public class CCredentials
    {
        public Nullable<Guid> CredentialGUIDfld { get; set; }
        public Nullable<Guid> PersonGUIDfld { get; set; }

        public string Usernamefld { get; set; }
        public string Passwordfld { get; set; }
        public string ConfirmPasswordfld { get; set; }

        public CCredentials()
        {
            Usernamefld = "";
            Passwordfld = "";
            ConfirmPasswordfld = "";
        }

    }
}
