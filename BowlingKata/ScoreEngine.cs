namespace BowlingKata
{
    public interface IScoreEngine
    {
        int? ScoreFrame(IFrame frameToScore);
    }

    public class ScoreEngine : IScoreEngine
    {
        private readonly IScoreCollaborator _scoreCollaborator;

        public ScoreEngine() : this(new ScoreCollaborator())
        {}

        public ScoreEngine(IScoreCollaborator scoreCollaborator)
        {
            _scoreCollaborator = scoreCollaborator;
        }

        public int? ScoreFrame(IFrame frameToScore)
        {
            int? frameScore;

            if(frameToScore.IsStrike())
            {
                frameScore = _scoreCollaborator.ScoreStrikeFrame(frameToScore);
            }
            else if(frameToScore.IsSpare())
            {
                frameScore = _scoreCollaborator.ScoreSpareFrame(frameToScore);
            }
            else
            {
                frameScore = _scoreCollaborator.ScoreNormalFrame(frameToScore);
            }

            return frameScore;
        }
    }
}
