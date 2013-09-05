using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public interface IScoreCollaborator
    {
        int ScoreOfFirstBallAfterGivenFrame(Frame[] frame, int frameIndex);
    }

    public class ScoreCollaborator
    {
        public int ScoreOfFirstBallAfterGivenFrame(Frame[] frame, int frameIndex)
        {
            return 0;
        }
    }
}
