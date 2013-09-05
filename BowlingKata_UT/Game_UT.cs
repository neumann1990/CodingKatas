using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            _testObject = new Game();
        }

        [TearDown]
        public void TearDown()
        {
            _scoreEngine.VerifyAllExpectations();
        }

        [Test]
        public void Constructor_Initializes_Frames_Collection_With_The_Correct_Number_Of_Frames()
        {
            Assert.That(_testObject.Frames.Count, Is.EqualTo(10));
        }

        [Test]
        public void Constructor_Initializes_Frames_Collection_Correct_NextFrame_Pointers()
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
        public void Constructor_Correctly_Initializes_Pin_Totals()
        {
            foreach (var frame in _testObject.Frames)
            {
                Assert.That(frame.PinsWithBall1, Is.EqualTo(-1));
                Assert.That(frame.PinsWithBall2, Is.EqualTo(-1));
                Assert.That(frame.FrameScore, Is.EqualTo(0));
            }
        }

        [Test]
        public void ScoreGame_Calls_Score_Engine_On_Each_Frame_With_Correct_Parameters()
        {
            for(var index = 0; index < 8; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                var subsequentFrame1 = frameToScore.NextFrame;
                var subsequentFrame2 = subsequentFrame1.NextFrame;
                _scoreEngine.Expect(s => s.ScoreFrame(frameToScore, subsequentFrame1, subsequentFrame2));
            }

            var lastFrameToScore = _testObject.Frames.ElementAt(9);
            _scoreEngine.Expect(s => s.ScoreFrame(lastFrameToScore, null, null));
        }

        [Test]
        public void ScoreGame_Correctly_Sets_Individual_Frame_Totals()
        {
            var frameTotals = new List<int>();

            for (var index = 0; index < 9; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                var subsequentFrame1 = frameToScore.NextFrame;
                var subsequentFrame2 = subsequentFrame1.NextFrame;
                _scoreEngine.Expect(s => s.ScoreFrame(frameToScore, subsequentFrame1, subsequentFrame2));
            }

            for (var index = 0; index < 8; index++)
            {
                var frameToScore = _testObject.Frames.ElementAt(index);
                var subsequentFrame1 = frameToScore.NextFrame;
                var subsequentFrame2 = subsequentFrame1.NextFrame;
                _scoreEngine.Expect(s => s.ScoreFrame(frameToScore, subsequentFrame1, subsequentFrame2));
            }

            var lastFrameToScore = _testObject.Frames.ElementAt(9);
            _scoreEngine.Expect(s => s.ScoreFrame(lastFrameToScore, null, null));


        }


//        [Test]
//        public void ScoreGame_Returns_0_If_No_Frames_Played()
//        {
//            var scoreTotal = _testObject.ScoreGame();
//
//            Assert.That(scoreTotal, Is.EqualTo(0));
//        }
//
//        [Test]
//        public void ScoreGame_Calls_ScoreFrame_On_Each_Frame()
//        {
//            for(var index = 0; index < _testObject.Frames.Length; index++)
//            {
//                var subsequentIndex1 = index + 1;
//                var subsequentIndex2 = index + 2;
//
//                var frameToScore = _testObject.Frames[index];
//
//                Frame subsequentFrame1 = null;
//                if(subsequentIndex1 <= _testObject.Frames.Length)
//                {
//                    subsequentFrame1 = _testObject.Frames[subsequentIndex1]; 
//                }
//
//                if(subsequentIndex2 <= _testObject.Frames.Length)
//                {
//                    subsequentFrame2 = _testObject.Frames[subsequentIndex2]; 
//                }
//
//                var subsequentFrame2 = _testObject.Frames[index];
//
//                _scoreEngine.ScoreFrame(_testObject.Frames[index])
//            }
//        }
    }
}
