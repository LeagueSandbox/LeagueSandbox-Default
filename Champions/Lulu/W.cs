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
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){ }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            float time = 1 + 0.25f * spell.Level;
            var buff = target.AddBuffGameScript("LuluWDebuff", "LuluWDebuff", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("LuluWDebuff", time, 1, owner);
            Champion champion = (Champion)target;
            string model = champion.Model;
            ApiFunctionManager.SetChampionModel((Champion)target, "LuluCupcake");
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
                ApiFunctionManager.SetChampionModel((Champion)target, model);
            });
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) { }
        public void OnUpdate(double diff) {

        }
     }
}