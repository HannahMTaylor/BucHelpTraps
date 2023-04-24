//Vote Code built by Stephen with a ph
namespace BucHelp.Data
{
    /// <summary>
    /// Beginning work for Vote tracking system.  Will most likely need more work or a refactor.
    /// </summary>
    public class Vote
    {
        int vote;
        string voteType;

        public Vote()
        {
            vote = 0;
            voteType = "U";
            ///check for databes for vote
        }

        public int GetVote()
        {
            return vote;
        }
        public void SelectVote(int vote, string voteType)
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
