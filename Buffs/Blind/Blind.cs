using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace Blind
{
    class Blind : BuffGameScript
    {
        UnitCrowdControl crowd = new UnitCrowdControl(CrowdControlType.Blind);

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            unit.ApplyCrowdControl(crowd);
        }

        public void OnDeactivate(Unit unit)
        {
            unit.RemoveCrowdControl(crowd);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
