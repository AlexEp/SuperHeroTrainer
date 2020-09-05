using SuperHeroTrainer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperHeroTrainer.Shared.DTO
{
    public class HeroDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HeroAbility Ability { get; set; }
        public string SuitColor { get; set; }
        public double Power { get; set; }
        public int? LastTrainDateCount { get; set; }

    }
}
