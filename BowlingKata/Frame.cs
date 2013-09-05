using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public interface IFrame
    {
        int PinsWithBall1 { get; set; }
        int PinsWithBall2 { get; set; }

        int FrameScore { get; set; }

        Frame NextFrame { get; set; }

        bool IsSpare();
        bool IsStrike();
    }

    public class Frame : IFrame
    {
        public int PinsWithBall1 { get; set; }
        public int PinsWithBall2 { get; set; }
        public int FrameScore { get; set; }
        public Frame NextFrame { get; set; }

        public Frame()
        {
            PinsWithBall1 = -1;
            PinsWithBall2 = -1;
            FrameScore = 0;
        }

        public bool IsSpare()
        {
            var isSpare = false;

            if (PinsWithBall1 != 10 && PinsWithBall1 + PinsWithBall2 == 10)
            {
                isSpare = true;
            }

            return isSpare;
        }

        public bool IsStrike()
        {
            var isStrike = false;

            if (PinsWithBall1 == 10)
            {
                isStrike = true;
            }

            return isStrike;
        }
    }
}
