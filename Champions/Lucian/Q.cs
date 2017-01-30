using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Lucian
{
    public class Q
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1100;
            var trueCoords = current + range;

            spell.AddLaser(trueCoords.X, trueCoords.Y);
            spell.spellAnimation("SPELL1", owner);
            ApiFunctionManager.AddParticle(owner, "Lucian_Q_laser.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticleTarget(owner, "Lucian_Q_cas.troy", owner);
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            var damage = owner.GetStats().AttackDamage.Total * (0.45f + spell.Level * 0.15f) + (50 + spell.Level * 30);
            owner.DealDamageTo(spell.Target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}