using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class GravesMove : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, ObjAIBase target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, ObjAIBase target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 425;
            var trueCoords = current + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1200, false, "Spell3");
            ApiFunctionManager.AddParticleTarget(owner, "Graves_Move_OnBuffActivate.troy", owner);
        }
        public void ApplyEffects(Champion owner, ObjAIBase target, Spell spell, Projectile projectile) {
            
        }
        public void OnUpdate(double diff) {

        }
     }
}