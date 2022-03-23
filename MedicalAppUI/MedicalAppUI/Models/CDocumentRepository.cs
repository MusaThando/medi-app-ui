namespace MedicalAppUI.Models
{
    public class CDocumentRepository
    {
        public Nullable<Guid> DocumentRepositoryGUIDfld { get; set; }
        public Nullable<Guid> SourceRecordGUIDfld { get; set; }
        public int IDfld { get; set; }
        public string FileNamefld { get; set; }
        public string UserTypefld { get; set; }
        public string FileTypefld { get; set; }
        public double Size { get; set; }
        public byte[]? FileContentfld { get; set; }
        public bool Publicfld { get; set; }
        public string RequestorPage { get; set; }
        public IFormFile? File { get; set; }

        public List<CDocumentRepository> DocumentList { get; set; }
        public CDocumentRepository()
        {
            RequestorPage = "";
            Publicfld = false;
            UserTypefld = "";
            Size = 0;
            FileTypefld = "";
            FileNamefld = "";
            File = null;
            IDfld = 0;
            DocumentRepositoryGUIDfld = Guid.Empty;
            SourceRecordGUIDfld = Guid.Empty;
            DocumentList = new List<CDocumentRepository>();
        }

    }
}
