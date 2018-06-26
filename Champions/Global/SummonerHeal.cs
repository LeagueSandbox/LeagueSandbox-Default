using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

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
            Champion mostWoundedAlliedChampion = null;
            float lowestHealthPercentage = 100;
            float maxHealth;
            float newHealth;
            for (var i = 0; i <= units.Count - 1; i++)
            {
                var value = units[i];
                if (owner.Team == value.Team && i != 0)
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
                newHealth = mostWoundedAlliedChampion.Stats.CurrentHealth + 75 + owner.Stats.Level * 15;
                maxHealth = mostWoundedAlliedChampion.Stats.HealthPoints.Total;
                mostWoundedAlliedChampion.Stats.CurrentHealth = Math.Min(maxHealth, newHealth);

                ApiFunctionManager.AddBuffHudVisual("SummonerHeal", 1.0f, 1, mostWoundedAlliedChampion, 1.0f);
                var statMod2 = new StatsModifier
                {
                    MoveSpeed =
                    {
                        PercentBonus = 0.3f
                    }
                };
                mostWoundedAlliedChampion.AddStatModifier(statMod2);
                ApiFunctionManager.CreateTimer(1.0f, () => { mostWoundedAlliedChampion.RemoveStatModifier(statMod2); });
                ApiFunctionManager.AddParticleTarget(mostWoundedAlliedChampion, "global_ss_heal_02.troy",
                    mostWoundedAlliedChampion);
                ApiFunctionManager.AddParticleTarget(mostWoundedAlliedChampion, "global_ss_heal_speedboost.troy",
                    mostWoundedAlliedChampion);
            }

            newHealth = owner.Stats.CurrentHealth + 75 + owner.Stats.Level * 15;
            maxHealth = owner.Stats.HealthPoints.Total;
            owner.Stats.CurrentHealth = Math.Min(maxHealth, newHealth);

            ApiFunctionManager.AddBuffHudVisual("SummonerHeal", 1.0f, 1, owner, 1.0f);
            var statMod = new StatsModifier
            {
                MoveSpeed =
                {
                    PercentBonus = 0.3f
                }
            };
            owner.AddStatModifier(statMod);
            ApiFunctionManager.CreateTimer(1.0f, () => { owner.RemoveStatModifier(statMod); });
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_heal.troy", owner);
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

