
namespace BowlingKata
{
    public interface IFrame
    {
        int TotalPins { get; }
        int PinsWithBall1 { get; set; }
        int PinsWithBall2 { get; set; }

        int FrameScore { get; set; }

        IFrame NextFrame { get; set; }

        bool IsSpare();
        bool IsStrike();
    }

    public class Frame : IFrame
    {
        public int TotalPins { get { return 10; } }

        public int PinsWithBall1 { get; set; }
        public int PinsWithBall2 { get; set; }
        public int FrameScore { get; set; }
        public IFrame NextFrame { get; set; }

        public Frame()
        {
            PinsWithBall1 = -1;
            PinsWithBall2 = -1;
            FrameScore = 0;
        }

        public bool IsSpare()
        {
            var isSpare = false;

            if (PinsWithBall1 != TotalPins && PinsWithBall1 + PinsWithBall2 == TotalPins)
            {
                isSpare = true;
            }

            return isSpare;
        }

        public bool IsStrike()
        {
            var isStrike = false;

            if (PinsWithBall1 == TotalPins)
            {
                isStrike = true;
            }

            return isStrike;
        }
    }
}
