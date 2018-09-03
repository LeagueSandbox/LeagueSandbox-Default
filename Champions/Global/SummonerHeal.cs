using System;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class SummonerHeal : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var units = ApiFunctionManager.GetChampionsInRange(owner, 850, true);
            units.Remove(owner);
            Champion mostWoundedAlliedChampion = null;
            float lowestHealthPercentage = 100;
            float maxHealth;
            foreach(var value in units) {
                if (owner.Team == value.Team)
                {
                    var currentHealth = value.Stats.CurrentHealth;
                    maxHealth = value.Stats.HealthPoints.Total;
                    if (currentHealth * 100 / maxHealth < lowestHealthPercentage && owner != value)
                    {
                        lowestHealthPercentage = currentHealth * 100 / maxHealth;
                        mostWoundedAlliedChampion = value;
                    }
                }
            }

            if (mostWoundedAlliedChampion != null)
            {
                PerformHeal(owner, spell, mostWoundedAlliedChampion);
            }

            PerformHeal(owner, spell, owner);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        private void PerformHeal(Champion owner, Spell spell, Champion target)
        {
            float healthGain = 75 + (target.Stats.Level * 15);
            if (target.HasBuffGameScriptActive("HealCheck", "HealCheck"))
            {
                healthGain *= 0.5f;
            }
            var newHealth = target.Stats.CurrentHealth + healthGain;
            target.Stats.CurrentHealth = Math.Min(newHealth, target.Stats.HealthPoints.Total);
            target.AddBuffGameScript("HealSpeed", "HealSpeed", spell, 1.0f, true);
            target.AddBuffGameScript("HealCheck", "HealCheck", spell, 35.0f, true);
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_02.troy", target);
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_speedboost.troy", target);
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}

