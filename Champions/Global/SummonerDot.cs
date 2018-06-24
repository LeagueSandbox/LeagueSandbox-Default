using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class SummonerDot : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("SummonerDot", 5.0f, 1, (ObjAIBase) target);
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Global_SS_Ignite.troy", target, 1);
            ((ObjAIBase)target).AddBuffGameScript("GrievousWounds", "GrievousWounds", spell, 4.0f);
            var damage = 10 + owner.GetStats().Level * 4;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL, false);
            ApiFunctionManager.CreateTimer(1.0f,
                () =>
                {
                    target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL,
                        false);
                });
            ApiFunctionManager.CreateTimer(2.0f,
                () =>
                {
                    target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL,
                        false);
                });
            ApiFunctionManager.CreateTimer(3.0f,
                () =>
                {
                    target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL,
                        false);
                });
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SUMMONER_SPELL, false);
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
            });
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

