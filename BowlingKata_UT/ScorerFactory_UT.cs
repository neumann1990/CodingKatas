using BowlingKata;
using BowlingKata.Scorers;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata_UT
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

        /*[Test]
        public void ScoreGame_Gutter_Game_Scores_Zero()
        {
            var frames = new Frame[10];
            for (var i = 0; i < frames.Length; i++)
            {
                frames[i] = new Frame()
                                     {
                                         PinsWithBall1 = 0,
                                         PinsWithBall2 = 0
                                     };
            }

            var actualScore = _testObject.ScoreGame(frames);
            Assert.That(actualScore, Is.EqualTo(0));
        }

        [Test]
        public void ScoreGame_All_Ones_Scores_20()
        {
            var frames = new Frame[10];
            for (var i = 0; i < frames.Length; i++)
            {
                frames[i] = new Frame()
                {
                    PinsWithBall1 = 1,
                    PinsWithBall2 = 1
                };
            }

            var actualScore = _testObject.ScoreGame(frames);
            Assert.That(actualScore, Is.EqualTo(20));
        }

        [Test]
        public void ScoreGame_Spare_Scores_Correctly()
        {
            var frames = new Frame[10];
            frames[0] = new Frame()
                             {
                                 PinsWithBall1 = 9,
                                 PinsWithBall2 = 1
                             };
            frames[1] = new Frame()
                             {
                                 PinsWithBall1 = 3,
                                 PinsWithBall2 = 0
                             };

            for (var i = 2; i < frames.Length; i++)
            {
                frames[i] = new Frame()
                {
                    PinsWithBall1 = 0,
                    PinsWithBall2 = 0
                };
            }

            var actualScore = _testObject.ScoreGame(frames);
            Assert.That(actualScore, Is.EqualTo(16));
        }*/

    }
}
