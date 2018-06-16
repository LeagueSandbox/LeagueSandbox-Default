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
            owner.Stats.MultiplicativeMovementSpeedBonus.Add(1.25f);
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Revive.troy", owner);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerReviveSpeedBoost", 12.0f, 1, BuffType.Haste, owner);

            ApiFunctionManager.CreateTimer(12.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.Stats.MultiplicativeMovementSpeedBonus.Remove(1.25f);
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

