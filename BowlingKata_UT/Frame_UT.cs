using BowlingKata;
using NUnit.Framework;

namespace BowlingKata_UT
{
    [TestFixture]
    public class Frame_UT
    {
        [Test]
        public void Constructor_Correctly_Initializes_Fields()
        {
            var frame = new Frame();

            Assert.That(frame.PinsWithBall1, Is.Null);
            Assert.That(frame.PinsWithBall2, Is.Null);
            Assert.That(frame.FrameScore, Is.Null);
            Assert.That(frame.NextFrame, Is.Null);
        }

        [Test]
        public void IsSpare_Returns_True_When_Given_Spare_Frame()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 9,
                PinsWithBall2 = 1
            };

            var isSpare = frame.IsSpare();
            Assert.That(isSpare, Is.True);
        }

        [Test]
        public void IsSpare_Returns_False_When_Given_Frame_With_Score_Less_Than_10()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 8,
                PinsWithBall2 = 1
            };

            var isSpare = frame.IsSpare();
            Assert.That(isSpare, Is.False);
        }

        [Test]
        public void IsSpare_Returns_False_When_Strike_Frame()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 10,
                PinsWithBall2 = 0
            };

            var isSpare = frame.IsSpare();
            Assert.That(isSpare, Is.False);
        }

        [Test]
        public void IsSpare_Returns_True_When_Gutter_Ball_Followed_By_Spare()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 0,
                PinsWithBall2 = 10
            };

            var isSpare = frame.IsSpare();
            Assert.That(isSpare, Is.True);
        }

        [Test]
        public void IsStrike_Returns_True_When_Given_Strike_Frame()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 10,
                PinsWithBall2 = 0
            };

            var isStrike = frame.IsStrike();
            Assert.That(isStrike, Is.True);
        }

        [Test]
        public void IsStrike_Returns_False_When_Gutter_Ball_Followed_By_Spare()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 0,
                PinsWithBall2 = 10
            };

            var isStrike = frame.IsStrike();
            Assert.That(isStrike, Is.False);
        }

        [Test]
        public void IsStrike_Returns_False_When_Given_Spare_Frame()
        {
            var frame = new Frame()
            {
                PinsWithBall1 = 9,
                PinsWithBall2 = 1
            };

            var isStrike = frame.IsStrike();
            Assert.That(isStrike, Is.False);
        }
    }
}
