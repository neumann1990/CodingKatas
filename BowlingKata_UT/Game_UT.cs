using System.Linq;
using BowlingKata;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata_UT
{
    [TestFixture]
    public class Game_UT
    {
        private Game _testObject;
        private IScoreEngine _scoreEngine;

        [SetUp]
        public void SetUp()
        {
            _scoreEngine = MockRepository.GenerateMock<IScoreEngine>();
            _testObject = new Game(_scoreEngine);
        }

        [TearDown]
        public void TearDown()
        {
            _scoreEngine.VerifyAllExpectations();
        }

        [Test]
        public void InitializeNewGame_Initializes_Frames_Collection_With_The_Correct_Number_Of_Frames()
        {
            Assert.That(_testObject.Frames.Count, Is.EqualTo(10));
        }

        [Test]
        public void InitializeNewGame_Initializes_Frames_Collection_Correct_NextFrame_Pointers()
        {
            for (var index = 0; index < 9; index++ )
            {
                var currentFrame = _testObject.Frames.ElementAt(index);
                var nextFrame = _testObject.Frames.ElementAt(index + 1);
                Assert.That(currentFrame.NextFrame, Is.SameAs(nextFrame));
            }

            var nextFrameOfLastFrame = _testObject.Frames.ElementAt(9).NextFrame;
            Assert.That(nextFrameOfLastFrame, Is.Null);
        }

        [Test]
        public void UpdateScore_Calls_Score_Engine_On_Each_Frame_With_Correct_Parameters()
        {
            for(var index = 0; index < 8; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                _scoreEngine.Expect(s => s.ScoreFrame(frameToScore)).Return(0);
            }

            var lastFrameToScore = _testObject.Frames.ElementAt(9);
            _scoreEngine.Expect(s => s.ScoreFrame(lastFrameToScore)).Return(0);

            _testObject.UpdateScore();
        }

        [Test]
        public void UpdateScore_Correctly_Sets_Individual_Frame_Totals()
        {
            var lastFrameIndex = _testObject.Frames.Count - 1 ;
            var secondToLastFrameIndex = _testObject.Frames.Count - 2;

            for (var index = 0; index <= secondToLastFrameIndex; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                _scoreEngine.Expect(s => s.ScoreFrame(frameToScore)).Return(index);
            }

            var lastFrameToScore = _testObject.Frames.ElementAt(lastFrameIndex);
            _scoreEngine.Expect(s => s.ScoreFrame(lastFrameToScore)).Return(lastFrameIndex);

            _testObject.UpdateScore();

            for (var index = 0; index <= lastFrameIndex; index++)
            {
                var frameScore = _testObject.Frames.ElementAt(index).FrameScore;
                Assert.That(frameScore, Is.EqualTo(index));
            }
        }

        [Test]
        public void UpdateScore_Returns_Correct_TotalScore()
        {
            _scoreEngine.Expect(s => s.ScoreFrame(Arg<Frame>.Is.Anything)).Return(1).Repeat.Any();

            var actualTotalScore = _testObject.UpdateScore();

            Assert.That(actualTotalScore, Is.EqualTo(10));
        }
    }
}
