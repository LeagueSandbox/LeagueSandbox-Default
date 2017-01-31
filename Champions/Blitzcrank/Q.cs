using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Blitzcrank
{
    public class Q
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            spell.spellAnimation("SPELL1", owner);
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 925;
            var trueCoords = current + range;

            spell.AddProjectile("RocketGrabMissile", trueCoords.X, trueCoords.Y);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var ap = owner.GetStats().AbilityPower.Total;
            var damage = 25 + spell.Level * 55 + ap;
            owner.DealDamageTo(spell.Target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

            if (!spell.Target.IsDead)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Blitzcrank_Grapplin_tar.troy", spell.Target, 1, "L_HAND");
                var current = new Vector2(owner.X, owner.Y);
                var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
                var range = to * 50;
                var trueCoords = current + range;

                ApiFunctionManager.DashToLocation(spell.Target, trueCoords.X, trueCoords.Y, spell.ProjectileSpeed, true);
            }

            projectile.setToRemove();
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}