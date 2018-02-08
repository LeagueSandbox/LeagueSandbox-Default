using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace Blind
{
    internal class Blind : BuffGameScript
    {
        private UnitCrowdControl _crowd = new UnitCrowdControl(CrowdControlType.Blind);

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            unit.ApplyCrowdControl(_crowd);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveCrowdControl(_crowd);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}

