using System.Numerics;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EzrealArcaneShift : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
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
            AttackableUnit target2 = null;
            var units = ApiFunctionManager.GetUnitsInRange(owner, 700, true);
            float distance = 700;
            foreach (var value in units)
            {
                if (owner.Team != value.Team && value is ObjAiBase)
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
                if (!((GameObject) target2 is BaseTurret))
                {
                    spell.AddProjectileTarget("EzrealArcaneShiftMissile", target2);
                }
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            target.TakeDamage(owner, 25f + spell.Level * 50f + owner.Stats.AbilityPower.Total * 0.75f,
                DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
