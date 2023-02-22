namespace BucHelp.Data
{
    //question object to be used as a template for posts on the question forum
    public class Question
    {
        public int QuestionID { get; set; }
 //Feature/Question_Submit_Button
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? UserName { get; set; }
//Feature/questionPostPage
        public string Answer { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
 //Feature/Question_Submit_Button
        public List<Question> QuestionsList = new List<Question>();

        public Question()
        {

        }
    }
}
