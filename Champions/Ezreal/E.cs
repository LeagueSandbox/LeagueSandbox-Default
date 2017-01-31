using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

namespace Ezreal
{
    public class E
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {

        }

        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;

            if (to.Length() > 475)
            {
                to = Vector2.Normalize(to);
                var range = to * 475;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }
            ApiFunctionManager.AddParticle(owner, "Ezreal_arcaneshift_cas.troy", owner.X, owner.Y);
            ApiFunctionManager.TeleportTo(owner, trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticleTarget(owner, "Ezreal_arcaneshift_flash.troy", owner);
            Unit target2 = null;
            var units = ApiFunctionManager.GetUnitsInRange(owner, 700, true);

            foreach (var value in units)
            {
                float distance = 700;
                if (owner.Team != value.Team)
                {
                    if (Vector2.Distance(new Vector2(trueCoords.X, trueCoords.Y), new Vector2(value.X, value.Y)) <=
                        distance)
                    {
                        target2 = value;
                        distance = Vector2.Distance(new Vector2(trueCoords.X, trueCoords.Y),
                            new Vector2(value.X, value.Y));
                    }
                }
            }
            if (target2 != null)
            {
                if (!ApiFunctionManager.UnitIsTurret(target2))
                {
                    spell.AddProjectileTarget("EzrealArcaneShiftMissile", target2);
                }
            }
        }

        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            owner.DealDamageTo(target, 25f + spell.Level * 50f + owner.GetStats().AbilityPower.Total * 0.75f,
                DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            projectile.setToRemove();
        }

        public static void OnUpdate(double diff)
        {

        }
    }
}