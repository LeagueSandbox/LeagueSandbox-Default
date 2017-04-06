using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Blitzcrank
{
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Overdrive_buf.troy", target, 1);
            var buff = target.AddBuffGameScript("Overdrive", "Overdrive", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Overdrive", 8.0f, 1, owner);
            ApiFunctionManager.CreateTimer(8.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                target.RemoveBuffGameScript(buff);
            });
        
        
        
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {

        }
        public void OnUpdate(double diff) {

        }
     }
}