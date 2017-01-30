using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Lucian
{
    public class E
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 445;
            var trueCoords = current + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1500, false, "SPELL3");
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }

        public static void OnUpdate(double diff)
        {

        }
    }
}