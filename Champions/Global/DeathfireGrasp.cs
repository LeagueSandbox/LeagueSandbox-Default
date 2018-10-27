using LeagueSandbox.GameServer.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using GameServerCore.Enums;
using GameServerCore;

namespace Spells
{
    public class DeathfireGrasp : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.AddProjectileTarget("DeathfireGraspSpell",target);
            var p1 = AddParticleTarget(owner, "deathFireGrasp_tar.troy", target);
            var p2 = AddParticleTarget(owner, "obj_DeathfireGrasp_debuff.troy", target);
            AddBuffHudVisual("DeathfireGraspSpell", 4.0f, 1, BuffType.COMBAT_DEHANCER, (ObjAiBase)target, 4.0f);
            CreateTimer(4.0f, () =>
            {
                RemoveParticle(p1);
                RemoveParticle(p2);
            });
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var damage = new Damage(target.Stats.HealthPoints.Total * 0.15f, DamageType.DAMAGE_TYPE_MAGICAL, 
                DamageSource.DAMAGE_SOURCE_SPELL, false);
            target.TakeDamage(owner, damage);
            projectile.SetToRemove();
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
