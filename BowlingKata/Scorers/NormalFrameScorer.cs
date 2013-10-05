namespace BowlingKata
{
    public class NormalFrameScorer : IFrameScorer
    {
        public int? ScoreFrame(IFrame frameToScore)
        {
            var pinsWithBall1 = frameToScore.PinsWithBall1;
            var pinsWithBall2 = frameToScore.PinsWithBall2;

            if (pinsWithBall1 == null || pinsWithBall1 < 0)
            {
                return null;
            }

            var frameScore = pinsWithBall1.GetValueOrDefault(0);
            if (pinsWithBall2 > 0)
            {
                frameScore += pinsWithBall2.GetValueOrDefault(0);
            }

            return frameScore;
        }
    }
}
