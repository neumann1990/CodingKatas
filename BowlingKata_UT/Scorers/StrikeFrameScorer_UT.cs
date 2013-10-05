using BowlingKata.Scorers;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata.Tests.Scorers
{
    [TestFixture]
    public class StrikeFrameScorer_UT
    {
        private const int TotalPins = 10;

        private IFrame _frameToScore;
        private IFrame _subsequentFrame1;
        private IFrame _subsequentFrame2;
        private StrikeFrameScorer _testObject;


        [SetUp]
        public void Setup()
        {
            _frameToScore = MockRepository.GenerateMock<IFrame>();
            _subsequentFrame1 = MockRepository.GenerateMock<IFrame>();
            _subsequentFrame2 = MockRepository.GenerateMock<IFrame>();

            _frameToScore.Stub(f => f.TotalPins).Return(TotalPins);
            _subsequentFrame1.Stub(f => f.TotalPins).Return(TotalPins);
            _subsequentFrame2.Stub(f => f.TotalPins).Return(TotalPins);

            _testObject = new StrikeFrameScorer();
        }

        [TearDown]
        public void TearDown()
        {
            _frameToScore.VerifyAllExpectations();
            _subsequentFrame1.VerifyAllExpectations();
            _subsequentFrame2.VerifyAllExpectations();
        }

        [Test]
        public void ScoreStrikeFrame_Confirms_Frame_To_Score_Is_Spare()
        {
            const int subsequentFrame1Ball1Pins = 3;
            const int subsequentFrame1Ball2Pins = 4;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            _subsequentFrame1.Expect(s => s.NextFrame).Return(_subsequentFrame2);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrame1Ball1Pins);
            _subsequentFrame1.Expect(f => f.PinsWithBall2).Return(subsequentFrame1Ball2Pins);

            _testObject.ScoreFrame(_frameToScore);
        }

        [Test]
        public void ScoreStrikeFrame_Returns_Null_If_Frame_Not_A_Strike_Frame()
        {
            _frameToScore.Expect(f => f.IsStrike()).Return(false);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.Null);
        }

        [Test]
        public void ScoreStrikeFrame_Returns_Correct_Frame_Total_When_Subsequent_Frame_Is_A_Normal_Frame()
        {
            const int subsequentFrame1Ball1Pins = 3;
            const int subsequentFrame1Ball2Pins = 4;
            const int expectedFrameScore = TotalPins + subsequentFrame1Ball1Pins + subsequentFrame1Ball2Pins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            _subsequentFrame1.Expect(s => s.NextFrame).Return(_subsequentFrame2);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrame1Ball1Pins);
            _subsequentFrame1.Expect(f => f.PinsWithBall2).Return(subsequentFrame1Ball2Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Returns_Correct_Frame_Total_When_Subsequent_Frame_Is_A_Spare_Frame()
        {
            const int subsequentFrame1Ball1Pins = 0;
            const int subsequentFrame1Ball2Pins = TotalPins;
            const int subsequentFrame2Ball1Pins = 2;
            const int expectedFrameScore = TotalPins + subsequentFrame1Ball1Pins + subsequentFrame1Ball2Pins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            _subsequentFrame1.Expect(s => s.NextFrame).Return(_subsequentFrame2);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrame1Ball1Pins);
            _subsequentFrame1.Expect(f => f.PinsWithBall2).Return(subsequentFrame1Ball2Pins);

            _subsequentFrame2.Expect(f => f.PinsWithBall1).Return(subsequentFrame2Ball1Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Returns_Correct_Frame_Total_When_Subsequent_Frame_Is_A_Strike_Frame()
        {
            const int subsequentFrame1Ball1Pins = TotalPins;
            const int subsequentFrame1Ball2Pins = 0;
            const int subsequentFrame2Ball1Pins = 2;
            const int expectedFrameScore = TotalPins + subsequentFrame1Ball1Pins + subsequentFrame2Ball1Pins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrame1Ball1Pins);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            _subsequentFrame1.Expect(s => s.NextFrame).Return(_subsequentFrame2);
            _subsequentFrame1.Expect(f => f.IsStrike()).Return(true);

            _subsequentFrame1.Expect(f => f.PinsWithBall2).Return(subsequentFrame1Ball2Pins);
            _subsequentFrame2.Expect(f => f.PinsWithBall1).Return(subsequentFrame2Ball1Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Does_Not_Include_SubsequentFrame1_Scores_In_Score_If_It_Has_Not_Been_Rolled()
        {
            const int expectedFrameScore = TotalPins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Does_Not_Include_SubsequentFrame2_Scores_In_Score_If_It_Has_Not_Been_Rolled()
        {
            const int subsequentFrame1Ball1Pins = TotalPins;
            int? subsequentFrame2Ball1Pins = null;
            const int expectedFrameScore = TotalPins + subsequentFrame1Ball1Pins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            _subsequentFrame1.Expect(s => s.NextFrame).Return(_subsequentFrame2);
            _subsequentFrame1.Expect(s => s.IsStrike()).Return(true);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrame1Ball1Pins);
            _subsequentFrame2.Expect(f => f.PinsWithBall1).Return(subsequentFrame2Ball1Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Handles_Null_Subsequent_Frame_1()
        {
            const int expectedFrameScore = TotalPins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(null);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Handles_Null_Subsequent_Frame_2()
        {
            const int subsequentFrame1Ball1Pins = TotalPins;
            const int subsequentFrame1Ball2Pins = 5;
            const int expectedFrameScore = TotalPins + subsequentFrame1Ball1Pins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            _subsequentFrame1.Expect(s => s.NextFrame).Return(null);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrame1Ball1Pins);
            _subsequentFrame1.Expect(f => f.PinsWithBall2).Return(subsequentFrame1Ball2Pins);

            var actualFrameScore = _testObject.ScoreFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }
    }
}
