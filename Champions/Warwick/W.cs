using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Warwick
{
    public class W : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){
           
            //Adds the buff to Warwick and allies
            foreach (var ally in ApiFunctionManager.GetChampionsInRange(owner, 1250, true))
            {
                ally.AddBuffGameScript("HuntersCall", "HuntersCall", spell, 6.0f);
            }

        }

        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

          
        }
        public void OnUpdate(double diff) {

        }
     }
}