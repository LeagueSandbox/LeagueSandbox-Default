using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Global
{
    public class SummonerSmite : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Smite.troy", target, 1);
            var damage = 370 + owner.GetStats().Level * 20;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
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
