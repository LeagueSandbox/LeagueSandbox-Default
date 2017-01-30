using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;

namespace Lux
{
    public class R
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 3340;
            var trueCoords = current + range;

            spell.AddLaser(trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticle(owner, "LuxMaliceCannon_beam.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.FaceDirection(owner, trueCoords, false);
            spell.spellAnimation("SPELL4", owner);
            ApiFunctionManager.AddParticleTarget(owner, "LuxMaliceCannon_cas.troy", owner);
        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            owner.DealDamageTo(spell.Target, 200f + spell.Level * 100f + owner.GetStats().AbilityPower.Total * 0.75f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}