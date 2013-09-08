namespace BowlingKata
{
    public interface IScoreEngine
    {
        int ScoreFrame(IFrame frameToScore, IFrame subsequentFrame1, IFrame subsequentFrame2);
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

        public int ScoreFrame(IFrame frameToScore, IFrame subsequentFrame1, IFrame subsequentFrame2)
        {
            int frameScore;

            if(frameToScore.IsStrike())
            {
                frameScore = _scoreCollaborator.ScoreStrikeFrame(frameToScore, subsequentFrame1, subsequentFrame2);
            }
            else if(frameToScore.IsSpare())
            {
                frameScore = _scoreCollaborator.ScoreSpareFrame(frameToScore, subsequentFrame1);
            }
            else
            {
                frameScore = _scoreCollaborator.ScoreNormalFrame(frameToScore);
            }

            return frameScore;
        }
    }
}
