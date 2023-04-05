namespace BucHelp.Data
{
    public class Vote
    {
        bool vote;
        string voteType;

        public Vote(bool vote, string voteType)
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
