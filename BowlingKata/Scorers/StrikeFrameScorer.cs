using System;

namespace BowlingKata.Scorers
{
    public class StrikeFrameScorer : IFrameScorer
    {
        public int? ScoreFrame(IFrame frameToScore)
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
            if (subsequentFrame2 == null)
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
