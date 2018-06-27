using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace Disarm
{
    internal class Disarm : IBuffGameScript
    {
        private UnitCrowdControl _crowd = new UnitCrowdControl(CrowdControlType.DISARM);

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            unit.ApplyCrowdControl(_crowd);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveCrowdControl(_crowd);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}

