using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BowlingKata;
using NUnit.Framework;

namespace BowlingKata_UT
{
    [TestFixture]
    public class ScoreCollaborator_UT
    {
        private ScoreCollaborator _testObject;

        [SetUp]
        public void Setup()
        {
            _testObject = new ScoreCollaborator();
        } 

    }
}
