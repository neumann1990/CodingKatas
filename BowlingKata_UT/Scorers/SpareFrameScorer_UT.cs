using BowlingKata.Scorers;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata.Tests.Scorers
{
    [TestFixture]
    public class SpareFrameScorer_UT
    {
        private SpareFrameScorer _testObject;
        private IFrame _frameToScore;
        private IFrame _subsequentFrame1;
        private const int TotalPins = 10;


        [SetUp]
        public void Setup()
        {
            _frameToScore = MockRepository.GenerateMock<IFrame>();
            _subsequentFrame1 = MockRepository.GenerateMock<IFrame>();

            _frameToScore.Stub(f => f.TotalPins).Return(TotalPins);
            _subsequentFrame1.Stub(f => f.TotalPins).Return(TotalPins);

            _testObject = new SpareFrameScorer();
        }

        [TearDown]
        public void TearDown()
        {
            _frameToScore.VerifyAllExpectations();
            _subsequentFrame1.VerifyAllExpectations();
        }


        [Test]
        public void ScoreSpareFrame_Confirms_Frame_To_Score_Is_Spare()
        {
            const int subsequentFrameBall1Pins = 1;

            _frameToScore.Expect(f => f.IsSpare()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrameBall1Pins);

            _testObject.ScoreFrame(_frameToScore);
        }

        [Test]
        public void ScoreSpareFrame_Returns_Null_If_Frame_Not_A_Spare_Frame()
        {
            _frameToScore.Expect(f => f.IsSpare()).Return(false);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.Null);
        }

        [Test]
        public void ScoreSpareFrame_Returns_Correct_Frame_Total()
        {
            const int subsequentFrameBall1Pins = 3;
            const int expectedFrameScore = TotalPins + subsequentFrameBall1Pins;

            _frameToScore.Expect(f => f.IsSpare()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrameBall1Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreSpareFrame_Does_Not_Include_SubsequentFrame_Ball1_In_Score_If_It_Has_Not_Been_Rolled()
        {
            int? subsequentFrameBall1Pins = null;
            const int expectedFrameScore = TotalPins;

            _frameToScore.Expect(f => f.IsSpare()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrameBall1Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

    }
}
