using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Annie
{
    public class Q
    {
        public static void onStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void onFinishCasting(Champion owner, Spell spell, Unit target)
        {

            spell.AddProjectileTarget("Disintegrate", target, false);

        }

        public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var AP = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 45 + spell.Level * 35 + AP;

            if (target != null && !ApiFunctionManager.IsDead(target))
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                if (target.IsDead)
                {
                    spell.LowerCooldown(0, spell.getCooldown());
                    float manaToRecover = 55 + spell.Level * 5;
                    var newMana = owner.GetStats().CurrentMana + manaToRecover;
                    var maxMana = owner.GetStats().ManaPoints.Total;
                    if (newMana >= maxMana)
                    {
                        owner.GetStats().CurrentMana = maxMana;
                    }
                    else
                    {
                        owner.GetStats().CurrentMana = newMana;
                    }
                }
            }
            projectile.setToRemove();

        }

        public static void onUpdate(double diff)
        {

        }
    }
}