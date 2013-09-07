using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BowlingKata
{
    public interface IScoreEngine
    {
        //int ScoreGame(Frame frames);
        int ScoreFrame(IFrame frameToScore, IFrame subsequentFrame1, IFrame subsequentFrame2);
    }

    public class ScoreEngine : IScoreEngine
    {
        private ScoreCollaborator _scoreCollaborator;

        public ScoreEngine() : this(new ScoreCollaborator())
        {}

        public ScoreEngine(ScoreCollaborator scoreCollaborator)
        {
            _scoreCollaborator = scoreCollaborator;
        }

        /*public int ScoreGame(Frame[] frames)
        {
            var totalScore = 0;

            foreach (var frame in frames)
            {
                if(frame.PinsWithBall1 == -1)
                {
                    return totalScore;
                }
                totalScore += frame.PinsWithBall1 + frame.PinsWithBall2;
            }

            return totalScore;
        }*/

        public int ScoreFrame(IFrame frameToScore, IFrame subsequentFrame1, IFrame subsequentFrame2)
        {

            return 0;
        }
    }
}
