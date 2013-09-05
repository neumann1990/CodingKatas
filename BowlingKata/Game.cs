using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        private ScoreEngine _scoreEngine;

        public Game() : this(new ScoreEngine()){}

        public Game(ScoreEngine scoreEngine)
        {
            _scoreEngine = scoreEngine;

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
            return 0;
        }
    }
}
