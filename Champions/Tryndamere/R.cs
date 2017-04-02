using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Tryndamere
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            float time = 5f;
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("UndyingRage", time, 1, target);
            if (owner.GetStats().CurrentHealth <= 1)
            {
                owner.GetStats().CurrentHealth = 1;
                owner.AddBuffGameScript("Invulnerable", "Invulnerable", spell);
            }
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
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