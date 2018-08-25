using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LeagueSandbox_Default.Champions.Global
{
    public class SummonerMana : IGameScript
    {
        private const float PERCENT_MAX_MANA_HEAL = 0.40f;

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var units = ApiFunctionManager.GetChampionsInRange(owner, 600, true);
            Champion nearbychampion = null;
            float lowestManaPercentage = 100;
            float maxMana;
            float newMana;
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
                        nearbychampion = value;
                    }
                }
            }

            if (nearbychampion != null)
            {
                var mp2 = nearbychampion.Stats.CurrentMana;
                var maxMp2 = nearbychampion.Stats.ManaPoints.Total;
                if (mp2 + maxMp2 * PERCENT_MAX_MANA_HEAL < maxMp2)
                    nearbychampion.Stats.CurrentMana = mp2 + maxMp2 * PERCENT_MAX_MANA_HEAL;
                else
                    nearbychampion.Stats.CurrentMana = maxMp2;
                ApiFunctionManager.AddParticleTarget(nearbychampion, "global_ss_clarity_02.troy", nearbychampion);
            }

            var mp = owner.Stats.CurrentMana;
            var maxMp = owner.Stats.ManaPoints.Total;
            if (mp + maxMp * PERCENT_MAX_MANA_HEAL < maxMp)
                owner.Stats.CurrentMana = mp + maxMp * PERCENT_MAX_MANA_HEAL;
            else
                owner.Stats.CurrentMana = maxMp;
            ApiFunctionManager.AddParticleTarget(owner, "global_ss_clarity_02.troy", owner);
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

