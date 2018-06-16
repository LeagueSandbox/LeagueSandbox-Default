using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class SummonerHeal : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var units = ApiFunctionManager.GetChampionsInRange(owner, 850, true);
            var mostWoundedAlliedChampion = target as Champion;
            float lowestHealthPercentage = 100;
            float maxHealth;
            float newHealth;
            if (mostWoundedAlliedChampion != null)
            {
                for (var i = 0; i <= units.Count - 1; i++)
                {
                    var value = units[i];
                    if (owner.Team == value.Team && i != 0)
                    {
                        var currentHealth = value.Stats.CurrentHealth;
                        maxHealth = value.Stats.TotalHealth;
                        if (currentHealth * 100 / maxHealth < lowestHealthPercentage && owner != value)
                        {
                            lowestHealthPercentage = currentHealth * 100 / maxHealth;
                            mostWoundedAlliedChampion = value;
                        }
                    }
                }
            }

            if (mostWoundedAlliedChampion != null)
            {
                newHealth = mostWoundedAlliedChampion.Stats.CurrentHealth + 75 + owner.Stats.Level * 15;
                maxHealth = mostWoundedAlliedChampion.Stats.TotalHealth;

                mostWoundedAlliedChampion.Stats.CurrentHealth = Math.Min(maxHealth, newHealth);

                ApiFunctionManager.AddBuffHUDVisual("SummonerHeal", 1.0f, 1, BuffType.Heal, mostWoundedAlliedChampion, 1.0f);
                mostWoundedAlliedChampion.Stats.MultiplicativeMovementSpeedBonus.Add(0.3f);
                ApiFunctionManager.CreateTimer(1.0f,
                    () => mostWoundedAlliedChampion.Stats.MultiplicativeMovementSpeedBonus.Remove(0.3f));
                ApiFunctionManager.AddParticleTarget(mostWoundedAlliedChampion, "global_ss_heal_02.troy",
                    mostWoundedAlliedChampion);
                ApiFunctionManager.AddParticleTarget(mostWoundedAlliedChampion, "global_ss_heal_speedboost.troy",
                    mostWoundedAlliedChampion);
            }

            newHealth = owner.Stats.CurrentHealth + 75 + owner.Stats.Level * 15;
            maxHealth = owner.Stats.TotalHealth;

            owner.Stats.CurrentHealth = Math.Min(maxHealth, newHealth);

            ApiFunctionManager.AddBuffHUDVisual("SummonerHeal", 1.0f, 1, BuffType.Heal, owner, 1.0f);
            owner.Stats.MultiplicativeMovementSpeedBonus.Add(0.3f);
            ApiFunctionManager.CreateTimer(1.0f, () => owner.Stats.MultiplicativeMovementSpeedBonus.Remove(0.3f));
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

