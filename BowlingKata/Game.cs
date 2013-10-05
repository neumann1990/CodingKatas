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

        private const int DefaultFrameNumber = 10;
        private readonly IScorerFactory _scorerFactory;

        public Game() : this(new ScorerFactory()){}

        public Game(IScorerFactory scorerFactory)
        {
            _scorerFactory = scorerFactory;

            InitializeGame();
        }

        public void InitializeGame()
        {
            Frames = new List<IFrame>
                         {
                             new Frame()
                         };

            for (var index = 0; index < DefaultFrameNumber - 1; index++)
            {
                var newFrame = new Frame();
                Frames.ElementAt(index).NextFrame = newFrame;
                Frames.Add(newFrame);
            }
        }

        public int UpdateScore()
        {
            var totalScore = 0;

            foreach (var frameToScore in Frames)
            {
                var frameScorer = _scorerFactory.GetScorer(frameToScore);
                frameToScore.FrameScore = frameScorer.ScoreFrame(frameToScore);

                totalScore += frameToScore.FrameScore.GetValueOrDefault(0);
            }

            return totalScore;
        }
    }
}
