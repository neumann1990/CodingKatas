using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowlingKata;
using NUnit.Framework;

namespace BowlingKata_UT
{
    [TestFixture]
    public class ScoreEngine_UT
    {
        private ScoreEngine _testObject;

        [SetUp]
        public void SetUp()
        {
            _testObject = new ScoreEngine();
        }

        [TearDown]
        public void TearDown()
        {
            
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
