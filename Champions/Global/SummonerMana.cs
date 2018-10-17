using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class SummonerMana : IGameScript
    {
        private const float PERCENT_MAX_MANA_HEAL = 0.40f;

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            foreach (var unit in GetChampionsInRange(owner, 600, true))
            {
                if (unit.Team == owner.Team)
                {
                    RestoreMana(owner);
                }
            }
        }

        private void RestoreMana(IChampion target)
        {
            var maxMp = target.Stats.ManaPoints.Total;
            var newMp = target.Stats.CurrentMana + (maxMp * PERCENT_MAX_MANA_HEAL);
            if (newMp < maxMp)
                target.Stats.CurrentMana = newMp;
            else
                target.Stats.CurrentMana = maxMp;
            AddParticleTarget(target, "global_ss_clarity_02.troy", target);
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

