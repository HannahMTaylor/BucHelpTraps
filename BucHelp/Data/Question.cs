namespace BucHelp.Data
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }


    }
}
