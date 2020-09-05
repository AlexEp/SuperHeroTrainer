using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SuperHeroTrainer.Core;
using SuperHeroTrainer.Shared.Entities;
using SuperHeroTrainer.Shared.Interfaces.Repository;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainet.Core.Test
{
    [TestClass]
    public class HeroesService_test
    {
        [TestMethod]
        public async Task Create_Super_Heroues()
        {
            var heroRepository = new Mock<IHeroRepository>();

            var service = new HeroesService(heroRepository.Object);
            IList <Hero> list = new List<Hero>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(await service.CreateHeroAsync("Hero" + i));
            }


            Assert.IsTrue(list.GroupBy(h => h.Ability).Count() > 1);
            Assert.IsTrue(list.GroupBy(h => h.SuitColor).Count() > 1);
        }
    }
}
