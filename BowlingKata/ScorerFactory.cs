using BowlingKata.Scorers;

namespace BowlingKata
{
    public interface IScorerFactory
    {
        IFrameScorer GetScorer(IFrame frameToScore);
    }

    public class ScorerFactory : IScorerFactory
    {
        public IFrameScorer GetScorer(IFrame frameToScore)
        {
            IFrameScorer frameScorer;

            if(frameToScore.IsStrike())
            {
                frameScorer = new StrikeFrameScorer();
            }
            else if(frameToScore.IsSpare())
            {
                frameScorer = new SpareFrameScorer();
            }
            else
            {
                frameScorer = new NormalFrameScorer();
            }

            return frameScorer;
        }
    }
}
