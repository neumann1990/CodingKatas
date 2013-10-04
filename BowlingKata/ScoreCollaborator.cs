namespace BowlingKata
{
    public interface IScoreCollaborator
    {
        int? ScoreNormalFrame(IFrame frameToScore);
        int? ScoreSpareFrame(IFrame frameToScore);
        int? ScoreStrikeFrame(IFrame frameToScore);
    }

    public class ScoreCollaborator : IScoreCollaborator
    {
        public int? ScoreNormalFrame(IFrame frameToScore)
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

        public int? ScoreSpareFrame(IFrame frameToScore)
        {
            if(!frameToScore.IsSpare())
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

        public int? ScoreStrikeFrame(IFrame frameToScore)
        {
            if (!frameToScore.IsStrike())
            {
                return null;
            }

            var frameScore = frameToScore.TotalPins;

            var subsequentFrame1 = frameToScore.NextFrame;
            if (subsequentFrame1 == null || !subsequentFrame1.PinsWithBall1.HasValue)
            {
                return frameScore;
            }

            var frame1Ball1Pins = subsequentFrame1.PinsWithBall1;
            var frame1Ball2Pins = subsequentFrame1.PinsWithBall2;

            frameScore += frame1Ball1Pins.Value;

            var subsequentFrame2 = subsequentFrame1.NextFrame;
            if(subsequentFrame2 == null)
            {
                return frameScore;
            }

            var frame2Ball1Pins = subsequentFrame2.PinsWithBall1;

            if (subsequentFrame1.IsStrike() && frame2Ball1Pins.HasValue)
            {
                frameScore += frame2Ball1Pins.Value;
            }
            else if (frame1Ball2Pins.HasValue)
            {
                frameScore += frame1Ball2Pins.Value;                
            }

            return frameScore;
        }
    }
}
