using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class SummonerHaste : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            owner.Stats.AdditiveMovementSpeedBonus += 0.27f;
            var hasteBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerHaste", 10.0f, 1, BuffType.Haste, owner, 10.0f);
            ApiFunctionManager.CreateTimer(10, () => owner.Stats.AdditiveMovementSpeedBonus -= 0.27f);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
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

