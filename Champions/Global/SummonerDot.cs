using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;

namespace Spells
{
    public class SummonerDot : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, ObjAIBase target)
        {
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerDot", 5.0f, 1, target);
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Ignite.troy", target, 1);
            var damage = 10 + owner.GetStats().Level * 4;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            ApiFunctionManager.CreateTimer(1.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            });
            ApiFunctionManager.CreateTimer(2.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            });
            ApiFunctionManager.CreateTimer(3.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
            });
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, false);
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
            });
        }

        public void OnFinishCasting(Champion owner, Spell spell, ObjAIBase target)
        {

        }

        public void ApplyEffects(Champion owner, ObjAIBase target, Spell spell, Projectile projectile)
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
