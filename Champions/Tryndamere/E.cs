using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Packets;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Tryndamere
{
    public class E : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "slash.troy", owner);
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 660;
            var trueCoords = current + range;
            var curser = new Vector2(spell.X, spell.Y);
            var castrange = Vector2.Distance(current, curser);

            if (castrange <= 660)
            {
                ApiFunctionManager.DashToLocation(owner, spell.X, spell.Y, 700, false, "SPELL3");
                spell.AddProjectile("slash", spell.X, spell.Y);
                ApplyDamage(owner, spell, target);
            }
            if (castrange > 660)
            {
                ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 700, false, "SPELL3");
                spell.AddProjectile("slash", trueCoords.X, trueCoords.Y);
                ApplyDamage(owner, spell, target);
            }
            ApiFunctionManager.RemoveParticle(p);
        }

        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            List<Unit> units = ApiFunctionManager.GetUnitsInRange(owner, 225, true);
            foreach (Unit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    //PHYSICAL DAMAGE: 70 / 100 / 130 / 160 / 190 (+120% BonusAD) (+100% AP)
                    var ap = owner.GetStats().AbilityPower.Total;
                    var bonusAd = (owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue) * 1.2f;
                    var damage = 40 + spell.Level * 30 + ap + bonusAd;
                    owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    ApiFunctionManager.AddParticleTarget(owner, "bloodslash.troy", unit);

                }
            }
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            
        }
        public void OnUpdate(double diff) {

        }
     }
}