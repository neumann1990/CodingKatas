using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public interface IGame
    {
        List<IFrame> Frames { get; set; }
        int UpdateScore();
    }

    public class Game
    {
        public List<IFrame> Frames { get; private set; }

        private readonly IScoreEngine _scoreEngine;


        public Game() : this(new ScoreEngine()){}

        public Game(IScoreEngine scoreEngine)
        {
            _scoreEngine = scoreEngine;

            InitializeNewGame();
        }

        public void InitializeNewGame()
        {
            Frames = new List<IFrame>
                         {
                             new Frame()
                         };

            for(var index = 1; index < 10; index++)
            {
                var newFrame = new Frame();
                Frames.ElementAt(index-1).NextFrame = newFrame;
                Frames.Add(newFrame);
            }
        }

        public int UpdateScore()
        {
            var totalScore = 0;

            foreach (var frameToScore in Frames)
            {
                var subsequentFrame1 = frameToScore.NextFrame;
                var subsequentFrame2 = subsequentFrame1 != null ? subsequentFrame1.NextFrame : null;

                var frameScore = _scoreEngine.ScoreFrame(frameToScore, subsequentFrame1, subsequentFrame2);
                frameToScore.FrameScore = frameScore;

                totalScore += frameScore;
            }

            return totalScore;
        }
    }
}
