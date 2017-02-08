using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Kassadin
{
    public class Q
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "Kassadin_Base_cas.troy", owner, 1, "L_HAND");
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            spell.AddProjectileTarget("NullLance", target, true);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total * 0.7f;
            var damage = 30 + spell.Level * 50 + ap;

            if (target != null && !ApiFunctionManager.IsDead(target))
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                if (target.IsDead)
                {
                    
            }
            projectile.setToRemove();

        }

        public static void OnUpdate(double diff)
        {

        }
    }
}
