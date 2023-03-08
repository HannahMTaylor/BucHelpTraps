using System.ComponentModel.DataAnnotations;
namespace BucHelp.Data
{
    //question object to be used as a template for posts on the question forum
    public class Question
    {
        public int QuestionID { get; set; }
        //Feature/Question_Submit_Button

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }
        public string? UserName { get; set; }
        public string? Category { get; set; }
//Feature/questionPostPage
        public string? Answer { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
 //Feature/Question_Submit_Button
        public List<Question> QuestionsList = new List<Question>(); //Temp list for questions until DB connections are created

        public List<Answer> Answers = new List<Answer>(); //List that holds the answer responces can be replaced when DB connections are created

        public Question()
        {
            //The Below is a Stub to be removed later
            for(int i =0; i < 5; i++)
            {
                Answers.Add(new Answer(1, 1, 1, "Answer", "This is a test", 0, Created, LastUpdated));
            }
            
        }
        //Featrue/Question_Categories
        public enum Categories
        {
            UNCATEGORIZED,
            CSCI,
            MATH,
            ENGL,
            DORM,
            CAMPUS
        }
    }
}
