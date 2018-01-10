using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace Disarm
{
    class Disarm : BuffGameScript
    {
        UnitCrowdControl crowd = new UnitCrowdControl(CrowdControlType.Disarm);

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            unit.ApplyCrowdControl(crowd);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveCrowdControl(crowd);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
