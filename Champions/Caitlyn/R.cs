using LeagueSandbox.GameServer.Logic.GameObjects;

namespace Caitlyn
{
    public class R
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            spell.AddProjectileTarget("CaitlynAceintheHoleMissile", target);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            if (target != null && !target.IsDead)
            {
                var damage = spell.getEffectValue(0) + owner.GetStats().AttackDamage.Total * 2;
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
            projectile.setToRemove();
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}