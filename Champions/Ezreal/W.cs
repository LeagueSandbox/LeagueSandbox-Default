using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Ezreal
{
    public class W
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "ezreal_bow_yellow.troy", owner, 1, "L_HAND");
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1000;
            var trueCoords = current + range;

            spell.AddProjectile("EzrealEssenceFluxMissile", trueCoords.X, trueCoords.Y);
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }

        public static void OnUpdate(double diff)
        {

        }
    }
}