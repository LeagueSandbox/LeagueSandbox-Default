using LeagueSandbox.GameServer.Logic.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Evelynn
{
    public class Passive : GameScript
    {
        public void OnActivate(Champion owner) {
            ApiEventManager.OnChampionDamageTaken.AddListener(this, owner, selfWasDamaged);
        }

        private void selfWasDamaged()
        {

        }

        public void OnDeactivate(Champion owner) {
            //Listeners are automatically removed when GameScripts deactivate
        }

        public void OnStartCasting(Champion owner, Spell spell, Unit target){}
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {}
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {}
        public void OnUpdate(double diff) {}
    }
}
