using ConwayLife.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConwayLife.Tests
{
    [TestFixture]
    public class LifeGameTests
    {
        LifeGame game;
        LifeRules rules = new LifeRules();
        PlayField field = new PlayField();

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullLifeRules()
        {
            game = new LifeGame(null, field);

        }

        [Test]
        [ExpectedException("System.ArgumentNullException")]
        public void NullPlayField()
        {
            game = new LifeGame(rules, null);
        }

    }
}
