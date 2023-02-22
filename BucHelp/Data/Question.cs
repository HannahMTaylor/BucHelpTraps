namespace BucHelp.Data
{
    //question object to be used as a template for posts on the question forum
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public string Answer { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }


    }
}
