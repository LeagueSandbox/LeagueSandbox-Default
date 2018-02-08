using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class SummonerRevive : GameScript
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

            ChampionStatModifier statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = 125.0f / 100.0f;
            owner.AddStatModifier(statMod);
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Revive.troy", owner);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerReviveSpeedBoost", 12.0f, 1, owner);
            ApiFunctionManager.CreateTimer(12.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
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

