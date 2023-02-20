namespace BucHelp.Data
{
    public class QuestionObjService
    {
        //temporary list of static questions
        List<Question> questions = new List<Question>();

        Question q1 = new Question { QuestionID = 0, Title = "Question 1", Description = "Where is graduation normally held?", Answer = "In the Mini-Dome"};
        Question q2 = new Question { QuestionID = 1, Title = "Quesiont 2", Description = "How do I apply to graduate?", Answer = "in Goldlink" };





        public Task<List<Question>> GetPostedQuestionsAsync()
        {
            questions.Add(q1);
            questions.Add(q2);
            return Task.FromResult(questions);
        }
    }
}
