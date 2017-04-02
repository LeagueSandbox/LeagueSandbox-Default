using System;
using System.Collections.Generic;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Brand
{
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "BrandPOF_cas.troy", owner);
            ApiFunctionManager.CreateTimer(0.625f, () =>
            {

            });
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 900;
            var trueCoords = current + range;

            if (castrange <= 900)
            {
                ApiFunctionManager.AddParticle(owner, "BrandPOF_tar.troy", spell.X, spell.Y);
                spell.AddProjectile("BrandFissure", spell.X, spell.Y);
                ApplyDamage(owner, spell, target);
            }
            else
            {
                ApiFunctionManager.AddParticle(owner, "BrandPOF_tar.troy", trueCoords.X, trueCoords.Y);
                spell.AddProjectile("BrandFissure", trueCoords.X, trueCoords.Y);
                ApplyDamage(owner, spell, target);
            }
        }
        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 900;
            var trueCoords = current + range;
            Target t = new Target(spell.X, spell.Y);
            Target c = new Target(trueCoords.X, trueCoords.Y);

            if (castrange <= 900)
            {
                List<Unit> units = ApiFunctionManager.GetUnitsInRange(t, 250, true);
                foreach (Unit unit in units)
                {
                    if (unit.Team != owner.Team)
                    {
                        var passive = unit.GetStats().HealthPoints.Total * 0.02f;
                        var ap = owner.GetStats().AbilityPower.Total * 0.6f;
                        var damage = 30 + spell.Level * 45 + ap;
                        owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                        for (float i = 0.0f; i < 4.0; i += 1.0f)
                        {
                            ApiFunctionManager.CreateTimer(i, () =>
                            {
                                owner.DealDamageTo(unit, passive, DamageType.DAMAGE_TYPE_MAGICAL,
                                    DamageSource.DAMAGE_SOURCE_SPELL, false);
                                ApiFunctionManager.AddBuffHUDVisual("BrandAblaze", 4.0f, 1, unit, removeAfter: 4.0f);
                            });
                        }
                    }
                }
            }
            else
            {
                List<Unit> units = ApiFunctionManager.GetUnitsInRange(c, 250, true);
                foreach (Unit unit in units)
                {
                    if (unit.Team != owner.Team)
                    {
                        var ap = owner.GetStats().AbilityPower.Total * 0.6f;
                        var damage = 30 + spell.Level * 45 + ap;
                        owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                }
            }
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
        public void OnUpdate(double diff) { }
     }
}