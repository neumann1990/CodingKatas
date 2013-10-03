namespace BowlingKata
{
    public interface IScoreCollaborator
    {
        int? ScoreNormalFrame(IFrame frameToScore);
        int? ScoreSpareFrame(IFrame frameToScore, IFrame subsequentFrame);
        int? ScoreStrikeFrame(IFrame frameToScore, IFrame subsequentFrame1, IFrame subsequentFrame2);
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

        public int? ScoreSpareFrame(IFrame frameToScore, IFrame subsequentFrame)
        {
            if(!frameToScore.IsSpare())
            {
                return null;
            }

            var subsequentFrameBall1Pins = subsequentFrame.PinsWithBall1;

            var frameScore = frameToScore.TotalPins;

            if (subsequentFrameBall1Pins > 0)
            {
                frameScore += subsequentFrameBall1Pins.GetValueOrDefault(0);
            }

            return frameScore;
        }

        public int? ScoreStrikeFrame(IFrame frameToScore, IFrame subsequentFrame1, IFrame subsequentFrame2)
        {
            if (!frameToScore.IsStrike())
            {
                return null;
            }

            var subsequentFrame1Ball1Pins = subsequentFrame1.PinsWithBall1;
            var subsequentFrame1Ball2Pins = subsequentFrame1.PinsWithBall2;
            var subsequentFrame2Ball1Pins = subsequentFrame2.PinsWithBall1;

            var frameScore = frameToScore.TotalPins;
            if(!subsequentFrame1Ball1Pins.HasValue)
            {
                return frameScore;
            }

            frameScore += subsequentFrame1Ball1Pins.GetValueOrDefault(0);

            if (subsequentFrame1Ball1Pins == subsequentFrame1.TotalPins && subsequentFrame2Ball1Pins > 0)
            {
                frameScore += subsequentFrame2Ball1Pins.GetValueOrDefault(0);
            }
            else if (subsequentFrame1Ball1Pins != subsequentFrame1.TotalPins && subsequentFrame1Ball2Pins > 0)
            {
                frameScore += subsequentFrame1Ball2Pins.GetValueOrDefault(0);                
            }

            return frameScore;
        }
    }
}
