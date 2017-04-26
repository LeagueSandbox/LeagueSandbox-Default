using LeagueSandbox.GameServer.Logic.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EzrealRisingSpellForce : GameScript
    {
        public void OnActivate(Champion owner)
        {
            ApiFunctionManager.LogInfo("Ezreal Passive OnActivate");
            ApiEventManager.OnChampionDamageTaken.AddListener(this, owner, selfWasDamaged);
        }

        private void selfWasDamaged()
        {
            ApiFunctionManager.LogInfo("Ezreal was damaged");
        }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){}
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {}
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {}
        public void OnUpdate(double diff) {}
    }
}
