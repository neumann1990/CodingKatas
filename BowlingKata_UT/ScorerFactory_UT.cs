using BowlingKata.Scorers;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata.Tests
{
    [TestFixture]
    public class ScorerFactory_UT
    {
        private ScorerFactory _testObject;

        [SetUp]
        public void SetUp()
        {
            _testObject = new ScorerFactory();
        }

        [Test]
        public void GetScorer_Returns_Correct_Scorer_For_Strike_Frame()
        {
            var frameToScore = MockRepository.GenerateMock<IFrame>();

            frameToScore.Expect(f => f.IsStrike()).Return(true);

            var actualFrameScorer = _testObject.GetScorer(frameToScore);
            Assert.That(actualFrameScorer, Is.TypeOf(typeof(StrikeFrameScorer)));
        }

        [Test]
        public void GetScorer_Returns_Correct_Scorer_For_Spare_Frame()
        {
            var frameToScore = MockRepository.GenerateMock<IFrame>();

            frameToScore.Expect(f => f.IsStrike()).Return(false);
            frameToScore.Expect(f => f.IsSpare()).Return(true);

            var actualFrameScorer = _testObject.GetScorer(frameToScore);
            Assert.That(actualFrameScorer, Is.TypeOf(typeof(SpareFrameScorer)));
        }

        [Test]
        public void ScoreFrame_Calls_Collaborator_To_Score_Normal_Frame()
        {
            var frameToScore = MockRepository.GenerateMock<IFrame>();

            frameToScore.Expect(f => f.IsStrike()).Return(false);
            frameToScore.Expect(f => f.IsSpare()).Return(false);

            var actualFrameScorer = _testObject.GetScorer(frameToScore);
            Assert.That(actualFrameScorer, Is.TypeOf(typeof(NormalFrameScorer)));
        }
    }
}
