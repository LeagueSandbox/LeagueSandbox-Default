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
            float newHealth;
            float healthGain;
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
                healthGain = 75 + (owner.Stats.Level * 15);
                if (mostWoundedAlliedChampion.HasBuffGameScriptActive("HealCheck", "HealCheck"))
                {
                    healthGain *= 0.5f;
                }
                newHealth = mostWoundedAlliedChampion.Stats.CurrentHealth + healthGain;
                maxHealth = mostWoundedAlliedChampion.Stats.HealthPoints.Total;
                mostWoundedAlliedChampion.Stats.CurrentHealth = Math.Min(maxHealth, newHealth);
                mostWoundedAlliedChampion.AddBuffGameScript("HealSpeed", "HealSpeed", spell, 1.0f, true);
                mostWoundedAlliedChampion.AddBuffGameScript("HealCheck", "HealCheck", spell, 35.0f, true);
                ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_02.troy", mostWoundedAlliedChampion);
                ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_speedboost.troy", mostWoundedAlliedChampion);
            }

            healthGain = 75 + (owner.Stats.Level * 15);
            if (owner.HasBuffGameScriptActive("HealCheck", "HealCheck"))
            {
                healthGain *= 0.5f;
            }
            newHealth = owner.Stats.CurrentHealth + healthGain;
            maxHealth = owner.Stats.HealthPoints.Total;
            owner.Stats.CurrentHealth = Math.Min(maxHealth, newHealth);

            owner.AddBuffGameScript("HealSpeed", "HealSpeed", spell, 1.0f, true);
            owner.AddBuffGameScript("HealCheck", "HealCheck", spell, 35.0f, true);
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_02.troy",owner);
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal_speedboost.troy", owner);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
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

