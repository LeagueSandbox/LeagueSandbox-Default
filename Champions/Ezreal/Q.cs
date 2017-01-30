using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Ezreal
{
    public class Q
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "ezreal_bow.troy", owner, 1, "L_HAND");
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1150;
            var trueCoords = current + range;

            spell.AddProjectile("EzrealMysticShotMissile", trueCoords.X, trueCoords.Y);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var ad = owner.GetStats().AttackDamage.Total * 1.1f;
            var ap = owner.GetStats().AbilityPower.Total * 0.4f;
            var damage = 15 + spell.Level * 20 + ad + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
            spell.LowerCooldown(0, 1);
            spell.LowerCooldown(1, 1);
            spell.LowerCooldown(2, 1);
            spell.LowerCooldown(3, 1);
            projectile.setToRemove();
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}