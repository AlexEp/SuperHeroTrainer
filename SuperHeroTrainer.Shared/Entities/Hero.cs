using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroTrainer.Shared.Entities
{
    public enum HeroAbility {
        Attacker,
        Defender
    }
    public class Hero
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HeroAbility Ability { get; set; }
        public int SuitColor { get; set; }
        public double StartingPower { get; set; }
        public double Power { get; set; }
        public DateTime? StartedtoTrain { get; set; }

        public DateTime? LastTrainDate { get; set; }
        public int? LastTrainDateCount { get; set; }

    }
}
