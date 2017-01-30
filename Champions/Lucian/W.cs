using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;

namespace Lucian
{
    public class W
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 900;
            var trueCoords = current + range;

            spell.AddProjectile("LucianWMissile", trueCoords.X, trueCoords.Y);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            //float damage = 20 + (spellLevel * 40) + owner.GetStats().AbilityPower.Total * 0.9;
            //dealMagicalDamage(damage);
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}