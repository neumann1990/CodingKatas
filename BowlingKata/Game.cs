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

            InitializeGame();
        }

        public void InitializeGame()
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
                var frameScore = _scoreEngine.ScoreFrame(frameToScore);
                frameToScore.FrameScore = frameScore;

                totalScore += frameScore.GetValueOrDefault(0);
            }

            return totalScore;
        }
    }
}
