using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Kassadin
{
    public class R : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticle(owner, "Kassadin_Base_R_vanish.troy", owner.X, owner.Y);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("KassadinR", 15.0f, 1, owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = new Vector2(spell.X, spell.Y) - current;
            Vector2 trueCoords;

            if (to.Length() > 475)
            {
                to = Vector2.Normalize(to);
                var range = to * 700;
                trueCoords = current + range;
            }
            else
            {
                trueCoords = new Vector2(spell.X, spell.Y);
            }
            ApiFunctionManager.TeleportTo(owner, trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticle(owner, "Kassadin_Base_R_appear.troy", owner.X, owner.Y);
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
            ApplyDamage(owner, spell, target);

        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            
        }

        public void OnUpdate(double diff)
        {

        }
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }

        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            List<Unit> units = ApiFunctionManager.GetUnitsInRange(owner, 150, true);
            foreach (Unit unit in units)
            {
                if (unit.Team != owner.Team)
                {
                    owner.DealDamageTo(unit, 60f + spell.Level * 20f + owner.GetStats().ManaPoints.Total * 0.02f, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                }
            }
        }

    }
}