using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Karthus
{
    public class Q : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            //ApiFunctionManager.AddParticleTarget(owner, "BrandPOF_cas.troy", owner);
            ApiFunctionManager.AddParticleTarget(owner, "Karthus_Base_Q_Hand_Glow.troy", owner);

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);
            var range = to * 875;
            var trueCoords = current + range;

            if (castrange <= 875)
            {
                ApiFunctionManager.CreateTimer(0.5f, () =>
                {
                    ApiFunctionManager.AddParticle(owner, "Karthus_Base_Q_Point.troy", spell.X, spell.Y);
                    //ApiFunctionManager.AddParticle(owner, "Karthus_Base_Q_Explosion.troy", spell.X, spell.Y);
                    spell.AddProjectile("KarthusLayWasteA1", spell.X, spell.Y);
                    ApplyDamage(owner, spell, target);
                });
            }
            else
            {
                ApiFunctionManager.CreateTimer(0.5f, () =>
                {
                    ApiFunctionManager.AddParticle(owner, "Karthus_Base_Q_Point.troy", trueCoords.X, trueCoords.Y);
                    //ApiFunctionManager.AddParticle(owner, "Karthus_Base_Q_Explosion.troy", trueCoords.X, trueCoords.Y);
                    spell.AddProjectile("KarthusLayWasteA1", trueCoords.X, trueCoords.Y);
                    ApplyDamage(owner, spell, target);
                });
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

            if (castrange <= 875)
            {
                List<Unit> units = ApiFunctionManager.GetUnitsInRange(t, 100, true);
                foreach (Unit unit in units)
                {
                    if (unit.Team != owner.Team)
                    {
                        var ap = owner.GetStats().AbilityPower.Total * 0.3f;
                        var damage = 20 + spell.Level * 20 + ap;
                        owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                }
            }
            else
            {
                List<Unit> units = ApiFunctionManager.GetUnitsInRange(c, 100, true);
                foreach (Unit unit in units)
                {
                    if (unit.Team != owner.Team)
                    {
                        var ap = owner.GetStats().AbilityPower.Total * 0.3f;
                        var damage = 20 + spell.Level * 20 + ap;
                        owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    }
                }
            }
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
        public void OnUpdate(double diff) { }
    }
}