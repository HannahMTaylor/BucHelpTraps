namespace BucHelp.Data
{
    public class QuestionObjService
    {
        //temporary list of static questions
        Question[] questions = new Question[4];

        Question q1 = new Question { QuestionID = 0, Title = "title1", Description = "Where is graduation normally held", Answer = "In the Mini-Dome"};
        Question q2 = new Question { QuestionID = 1, Title = "title1", Description = "How do I apply to graduate?", Answer = "in Goldlink" };
        
        

        


        //need a method to get the data
        public Question[] GetPostedQuestions()
        {
            questions[0] = q1;
            questions[1] = q2;

            //add code here to interface with Liam's db service
            return questions;
        }
    }
}
