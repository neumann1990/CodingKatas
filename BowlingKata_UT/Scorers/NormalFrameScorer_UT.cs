using BowlingKata;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata_UT
{
    [TestFixture]
    public class NormalFrameScorer_UT
    {
        private NormalFrameScorer _testObject;
        private IFrame _frameToScore;

        [SetUp]
        public void Setup()
        {
            _frameToScore = MockRepository.GenerateMock<IFrame>();
            _testObject = new NormalFrameScorer();
        }

        [TearDown]
        public void TearDown()
        {
            _frameToScore.VerifyAllExpectations();
        }

        [Test]
        public void ScoreNormalFrame_Returns_Correct_Frame_Total()
        {
            const int ball1Pins = 1;
            const int ball2Pins = 2;
            const int expectedFrameScore = ball1Pins + ball2Pins;

            _frameToScore.Expect(f => f.PinsWithBall1).Return(ball1Pins);
            _frameToScore.Expect(f => f.PinsWithBall2).Return(ball2Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreNormalFrame_Returns_Null_If_Ball1_Has_Not_Been_Rolled()
        {
            int? ball1Pins = null;
            const int ball2Pins = 2;
            int? expectedFrameScore = null;

            _frameToScore.Expect(f => f.PinsWithBall1).Return(ball1Pins);
            _frameToScore.Expect(f => f.PinsWithBall2).Return(ball2Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreNormalFrame_Does_Not_Include_Ball2_In_Score_If_It_Has_Not_Been_Rolled()
        {
            const int ball1Pins = 2;
            int? ball2Pins = null;
            const int expectedFrameScore = ball1Pins;

            _frameToScore.Expect(f => f.PinsWithBall1).Return(ball1Pins);
            _frameToScore.Expect(f => f.PinsWithBall2).Return(ball2Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

    }
}
