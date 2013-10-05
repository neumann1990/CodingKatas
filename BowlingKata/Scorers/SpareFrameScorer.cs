namespace BowlingKata.Scorers
{
    public class SpareFrameScorer : IFrameScorer
    {
        public int? ScoreFrame(IFrame frameToScore)
        {
            if (!frameToScore.IsSpare())
            {
                return null;
            }

            var subsequentFrame = frameToScore.NextFrame;
            var subsequentFrameBall1Pins = subsequentFrame.PinsWithBall1;

            var frameScore = frameToScore.TotalPins;

            if (subsequentFrameBall1Pins > 0)
            {
                frameScore += subsequentFrameBall1Pins.GetValueOrDefault(0);
            }

            return frameScore;
        }
    }
}
