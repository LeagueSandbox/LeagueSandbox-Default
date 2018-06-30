using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.GameObjects.Stats;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class SummonerRevive : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            if (!owner.IsDead)
            {
                return;
            }

            owner.Respawn();

            var statMod = new StatsModifier();
            statMod.MoveSpeed.PercentBonus = 125.0f / 100.0f;
            owner.AddStatModifier(statMod);
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Revive.troy", owner);
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("SummonerReviveSpeedBoost", 12.0f, 1, owner);
            ApiFunctionManager.CreateTimer(12.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                owner.RemoveStatModifier(statMod);
            });
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

