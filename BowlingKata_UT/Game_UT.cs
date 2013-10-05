﻿using System.Linq;
using BowlingKata;
using NUnit.Framework;
using Rhino.Mocks;

namespace BowlingKata_UT
{
    [TestFixture]
    public class Game_UT
    {
        private IScorerFactory _scorerFactory;
        private IFrameScorer _frameScorer;
        private Game _testObject;

        [SetUp]
        public void SetUp()
        {
            _scorerFactory = MockRepository.GenerateMock<IScorerFactory>();
            _frameScorer = MockRepository.GenerateMock<IFrameScorer>();
            _testObject = new Game(_scorerFactory);
        }

        [TearDown]
        public void TearDown()
        {
            _scorerFactory.VerifyAllExpectations();
            _frameScorer.VerifyAllExpectations();
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
            for(var index = 0; index < _testObject.Frames.Count; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                _scorerFactory.Expect(s => s.GetScorer(frameToScore)).Return(_frameScorer);
                _frameScorer.Expect(f => f.ScoreFrame(frameToScore)).Return(1);
            }

            _testObject.UpdateScore();
        }

        [Test]
        public void UpdateScore_Correctly_Sets_Individual_Frame_Totals()
        {
            const int frameScore = 2;

            for (var index = 0; index < _testObject.Frames.Count; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                _scorerFactory.Expect(s => s.GetScorer(frameToScore)).Return(_frameScorer);
                _frameScorer.Expect(f => f.ScoreFrame(frameToScore)).Return(frameScore);
            }

            _testObject.UpdateScore();

            for (var index = 0; index < _testObject.Frames.Count; index++)
            {
                var currentFrameScore = _testObject.Frames.ElementAt(index).FrameScore;
                Assert.That(currentFrameScore, Is.EqualTo(frameScore));
            }
        }

        [Test]
        public void UpdateScore_Returns_Correct_TotalScore()
        {
            const int frameScore = 2;

            for (var index = 0; index < _testObject.Frames.Count; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                _scorerFactory.Expect(s => s.GetScorer(frameToScore)).Return(_frameScorer);
                _frameScorer.Expect(f => f.ScoreFrame(frameToScore)).Return(frameScore);
            }

            var actualTotalScore = _testObject.UpdateScore();

            Assert.That(actualTotalScore, Is.EqualTo(frameScore * _testObject.Frames.Count));
        }
    }
}
