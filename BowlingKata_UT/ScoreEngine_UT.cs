using BowlingKata;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata_UT
{
    [TestFixture]
    public class ScoreEngine_UT
    {
        private IScoreCollaborator _scoreCollaborator;
        private ScoreEngine _testObject;

        [SetUp]
        public void SetUp()
        {
            _scoreCollaborator = MockRepository.GenerateMock<IScoreCollaborator>();
            _testObject = new ScoreEngine(_scoreCollaborator);
        }

        [TearDown]
        public void TearDown()
        {
            _scoreCollaborator.VerifyAllExpectations();
        }

        [Test]
        public void ScoreFrame_Calls_Collaborator_To_Score_Strike_Frame()
        {
            const int expectedFrameScore = 1;

            var frameToScore = MockRepository.GenerateMock<IFrame>();

            frameToScore.Expect(f => f.IsStrike()).Return(true);
            _scoreCollaborator.Expect(s => s.ScoreStrikeFrame(frameToScore)).Return(expectedFrameScore);

            var actualFrameScore = _testObject.ScoreFrame(frameToScore);
            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreFrame_Calls_Collaborator_To_Score_Spare_Frame()
        {
            const int expectedFrameScore = 1;

            var frameToScore = MockRepository.GenerateMock<IFrame>();

            frameToScore.Expect(f => f.IsStrike()).Return(false);
            frameToScore.Expect(f => f.IsSpare()).Return(true);
            _scoreCollaborator.Expect(s => s.ScoreSpareFrame(frameToScore)).Return(expectedFrameScore);

            var actualFrameScore = _testObject.ScoreFrame(frameToScore);
            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
        }

        [Test]
        public void ScoreFrame_Calls_Collaborator_To_Score_Normal_Frame()
        {
            const int expectedFrameScore = 1;

            var frameToScore = MockRepository.GenerateMock<IFrame>();

            frameToScore.Expect(f => f.IsStrike()).Return(false);
            frameToScore.Expect(f => f.IsSpare()).Return(false);
            _scoreCollaborator.Expect(s => s.ScoreNormalFrame(frameToScore)).Return(expectedFrameScore);

            var actualFrameScore = _testObject.ScoreFrame(frameToScore);
            Assert.That(actualFrameScore, Is.EqualTo(expectedFrameScore));
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
