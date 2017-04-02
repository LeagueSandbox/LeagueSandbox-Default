using System.Collections.Generic;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Gangplank
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            foreach (var champion in ApiFunctionManager.GetChampionsInRange(owner, 20000, true))
            {
                if (champion.Team == owner.Team)
                {
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_aoe_indicator_green.troy", spell.X, spell.Y);
                }

                else
                {
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_aoe_indicator_red.troy", spell.X, spell.Y);
                }
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 20000;
            var trueCoords = current + range;
            spell.AddProjectile("CannonBarrage", spell.X, spell.Y, true);

            for (float i = 0.0f; i < 7.0; i += 1.0f)
            {
                ApiFunctionManager.CreateTimer(i, () =>
                {
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_point.troy", spell.X, spell.Y);
                    ApiFunctionManager.AddParticle(owner, "Pirate_XMarksTheSpot_nova.troy", spell.X, spell.Y);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X + 100, spell.Y);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X + 50, spell.Y + 50);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X - 70, spell.Y - 30);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X + 168, spell.Y - 221);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X + 64, spell.Y - 280);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X - 136, spell.Y + 42);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_ball.troy", spell.X - 76, spell.Y - 196);
                    ApiFunctionManager.AddParticle(owner, "pirate_cannonBarrage_tar.troy", spell.X, spell.Y);
                    ApplyDamage(owner, spell, target);
                });
            }
        }
        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            Target t = new Target(spell.X, spell.Y);
            List<Unit> units = ApiFunctionManager.GetUnitsInRange(t, 600, true);
            foreach (Unit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    //MAGICAL DAMAGE PER SECOND: 75 / 120 / 165 (+ 20% AP)
                    var ap = owner.GetStats().AbilityPower.Total * 0.2f;
                    var damage = 30 + spell.Level * 45 + ap;
                    owner.DealDamageTo(unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                }
            }
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
        public void OnUpdate(double diff) { }

        
    }

}