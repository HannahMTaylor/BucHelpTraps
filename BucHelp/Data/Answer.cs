namespace BucHelp.Data
{
    /// <summary>
    /// Class that will represent responces to questions
    /// </summary>
    public class Answer
    {
        public int AnswerId { get; set; }
        
        public int QuestionId { get; set; }

        public int UserId { get; set; }

        public string Description{ get; set; }

        public int Votes { get; set; }

        public DateTime Created = new DateTime();

        public DateTime LastUpdated = new DateTime();

        public Answer() 
        {
            
        }

        public Answer(int answerId, int questionId, int userId, string description, int votes, DateTime created, DateTime lastUpdated)
        {
            AnswerId = answerId;
            QuestionId = questionId;
            UserId = userId;
            Description = description;
            Votes = votes;
            Created = created;
            LastUpdated = lastUpdated;
        }

        

    }
}
