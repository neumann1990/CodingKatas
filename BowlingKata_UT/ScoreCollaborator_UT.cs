using BowlingKata;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata_UT
{
    [TestFixture]
    public class ScoreCollaborator_UT
    {
        private ScoreCollaborator _testObject;
        private IFrame _frameToScore;
        private IFrame _subsequentFrame1;
        private IFrame _subsequentFrame2;
        private const int TotalPins = 10;


        [SetUp]
        public void Setup()
        {
            _frameToScore = MockRepository.GenerateMock<IFrame>();
            _subsequentFrame1 = MockRepository.GenerateMock<IFrame>();
            _subsequentFrame2 = MockRepository.GenerateMock<IFrame>();

            _frameToScore.Stub(f => f.TotalPins).Return(TotalPins);
            _subsequentFrame1.Stub(f => f.TotalPins).Return(TotalPins);
            _subsequentFrame2.Stub(f => f.TotalPins).Return(TotalPins);

            _testObject = new ScoreCollaborator();
        } 

        [TearDown]
        public void TearDown()
        {
            _frameToScore.VerifyAllExpectations();
            _subsequentFrame1.VerifyAllExpectations();
            _subsequentFrame2.VerifyAllExpectations();
        }

        [Test]
        public void ScoreNormalFrame_Returns_Correct_Frame_Total()
        {
            const int ball1Pins = 1;
            const int ball2Pins = 2;
            const int expectedFrameScore = ball1Pins + ball2Pins;

            _frameToScore.Expect(f => f.PinsWithBall1).Return(ball1Pins);
            _frameToScore.Expect(f => f.PinsWithBall2).Return(ball2Pins);

            var actualFrameScore = _testObject.ScoreNormalFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreNormalFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreNormalFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreSpareFrame_Confirms_Frame_To_Score_Is_Spare()
        {
            const int subsequentFrameBall1Pins = 1;

            _frameToScore.Expect(f => f.IsSpare()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);
            _subsequentFrame1.Expect(f => f.PinsWithBall1).Return(subsequentFrameBall1Pins);

            _testObject.ScoreSpareFrame(_frameToScore);
        }

        [Test]
        public void ScoreSpareFrame_Returns_Null_If_Frame_Not_A_Spare_Frame()
        {
            _frameToScore.Expect(f => f.IsSpare()).Return(false);

            var actualFrameScore = _testObject.ScoreSpareFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreSpareFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreSpareFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
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

            _testObject.ScoreStrikeFrame(_frameToScore);
        }

        [Test]
        public void ScoreStrikeFrame_Returns_Null_If_Frame_Not_A_Strike_Frame()
        {
            _frameToScore.Expect(f => f.IsStrike()).Return(false);

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Does_Not_Include_SubsequentFrame1_Scores_In_Score_If_It_Has_Not_Been_Rolled()
        {
            const int expectedFrameScore = TotalPins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(_subsequentFrame1);

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreStrikeFrame_Handles_Null_Subsequent_Frame_1()
        {
            const int expectedFrameScore = TotalPins;

            _frameToScore.Expect(f => f.IsStrike()).Return(true);
            _frameToScore.Expect(f => f.NextFrame).Return(null);

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

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

            var actualFrameScore = _testObject.ScoreStrikeFrame(_frameToScore);

            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }
    }
}
