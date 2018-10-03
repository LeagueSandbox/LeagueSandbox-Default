using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class SummonerMana : IGameScript
    {
        private const float PERCENT_MAX_MANA_HEAL = 0.40f;

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            var units = ApiFunctionManager.GetChampionsInRange(owner, 600, true);
            IChampion nearbyIChampion = null;
            float lowestManaPercentage = 100;
            float maxMana;
            //float newMana; // not used?
            for (var i = 0; i <= units.Count - 1; i++)
            {
                var value = units[i];
                if (owner.Team == value.Team && i != 0)
                {
                    var currentMana = value.Stats.CurrentMana;
                    maxMana = value.Stats.ManaPoints.Total;
                    if (currentMana * 100 / maxMana < lowestManaPercentage && owner != value)
                    {
                        lowestManaPercentage = currentMana * 100 / maxMana;
                        nearbyIChampion = value;
                    }
                }
            }

            if (nearbyIChampion != null)
            {
                var mp2 = nearbyIChampion.Stats.CurrentMana;
                var maxMp2 = nearbyIChampion.Stats.ManaPoints.Total;
                if (mp2 + maxMp2 * PERCENT_MAX_MANA_HEAL < maxMp2)
                    nearbyIChampion.Stats.CurrentMana = mp2 + maxMp2 * PERCENT_MAX_MANA_HEAL;
                else
                    nearbyIChampion.Stats.CurrentMana = maxMp2;
                ApiFunctionManager.AddParticleTarget(nearbyIChampion, "global_ss_clarity_02.troy", nearbyIChampion);
            }

            var mp = owner.Stats.CurrentMana;
            var maxMp = owner.Stats.ManaPoints.Total;
            if (mp + maxMp * PERCENT_MAX_MANA_HEAL < maxMp)
                owner.Stats.CurrentMana = mp + maxMp * PERCENT_MAX_MANA_HEAL;
            else
                owner.Stats.CurrentMana = maxMp;
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_clarity_02.troy", owner);
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(IChampion owner)
        {
        }

        public void OnDeactivate(IChampion owner)
        {
        }
    }
}

