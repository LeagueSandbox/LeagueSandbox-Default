using GameServerCore;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class CaitlynAceintheHole : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.AddProjectileTarget("CaitlynAceintheHoleMissile", target);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            if (target != null && !target.IsDead)
            {
                // 250/475/700
                var bonusAD = owner.Stats.AttackDamage.Total - owner.Stats.AttackDamage.BaseValue;
                var damage = new Damage(25 + (225 * spell.Level) + (bonusAD * 2), DamageType.DAMAGE_TYPE_PHYSICAL, 
                DamageSource.DAMAGE_SOURCE_SPELL, false);
                target.TakeDamage(owner, damage);
            }

            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
