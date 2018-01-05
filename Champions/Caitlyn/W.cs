using System.Collections.Generic;
using System.Numerics;
using LeagueSandbox.GameServer.Logic;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class CaitlynYordleTrap : GameScript
    {
        public void OnActivate(Champion owner) { }

        public void OnDeactivate(Champion owner) { }

        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            spell.spellAnimation("SPELL2", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 750;
            var trueCoords = current + range;

            /*ObjAIBase trap = new ObjAIBase("CaitlynTrap", new Stats(), 0, trueCoords.X, trueCoords.Y);
            trap.OnAdded();*/

            var trap = new ObjAIBase("Caitlyn", new Stats(), 40, trueCoords.X, trueCoords.Y);
            trap.OnAdded();
            new Placeable(owner, trueCoords.X, trueCoords.Y, "VisionWard", "VisionWard").OnAdded();
        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
    }
}