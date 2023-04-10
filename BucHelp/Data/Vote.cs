namespace BucHelp.Data
{
    public class Vote
    {
        int vote;
        string voteType;

        public Vote()
        {
            
        }
        public Vote(int vote, string voteType)
        {
            this.vote = vote;
            this.voteType = voteType;

            UpdateVote();
        }

        private void UpdateVote()
        {
            //stub
        }

    }
}
