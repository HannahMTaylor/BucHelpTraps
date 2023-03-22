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

        public string? Title { get; set; }

        public string? Description{ get; set; }

        public int votes { get; set; }

        public DateTime Creeated = new DateTime();

        public DateTime LastUpdated = new DateTime();

        public Answer() 
        {
            
        }

        public Answer(int answerId, int questionId, int userId, string? title, string? description, int votes, DateTime creeated, DateTime lastUpdated)
        {
            AnswerId = answerId;
            QuestionId = questionId;
            UserId = userId;
            Title = title;
            Description = description;
            this.votes = votes;
            Creeated = creeated;
            LastUpdated = lastUpdated;
        }

        

    }
}
