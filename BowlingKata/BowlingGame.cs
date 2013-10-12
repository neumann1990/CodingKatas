using System.Collections.Generic;
using System.Linq;

namespace BowlingKata
{
    public interface IBowlingGame
    {
        List<IFrame> Frames { get; set; }
        void InitializeGame();
        int UpdateScore();
    }

    public class BowlingGame
    {
        public List<IFrame> Frames { get; private set; }
        public IFrame CurrentFrame { get; private set; }

        private const int DefaultFrameNumber = 10;
        private readonly IScorerFactory _scorerFactory;

        public BowlingGame() : this(new ScorerFactory()){}

        public BowlingGame(IScorerFactory scorerFactory)
        {
            _scorerFactory = scorerFactory;

            InitializeGame();
        }

        public void InitializeGame()
        {
            Frames = new List<IFrame>();

            for (var index = 0; index < DefaultFrameNumber; index++)
            {
                Frames.Add(new Frame());
            }

            for (var currentFrameIndex = 0; currentFrameIndex < DefaultFrameNumber; currentFrameIndex++)
            {
                var prevFrameIndex = currentFrameIndex - 1;
                var nextFrameIndex = currentFrameIndex + 1;

                var prevFrame = Frames.ElementAtOrDefault(prevFrameIndex);
                var nextFrame = Frames.ElementAtOrDefault(nextFrameIndex);

                Frames.ElementAt(currentFrameIndex).PrevFrame = prevFrame;
                Frames.ElementAt(currentFrameIndex).NextFrame = nextFrame;
            }

            CurrentFrame = Frames.ElementAt(0);
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
