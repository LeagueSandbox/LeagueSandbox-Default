using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Lulu
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_R_cas.troy", target, 1);
            var buff = target.AddBuffGameScript("LuluR", "LuluR", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("LuluR", 7.0f, 1, owner);
            ApiFunctionManager.CreateTimer(7.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
                ApiFunctionManager.AddParticleTarget(owner, "Lulu_R_expire.troy", target, 1);
            });

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {

        }
        public void OnUpdate(double diff) {

        }
     }
}