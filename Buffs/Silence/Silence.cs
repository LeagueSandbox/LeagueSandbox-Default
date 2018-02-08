using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace Silence
{
    internal class Silence : BuffGameScript
    {
        private UnitCrowdControl _crowd = new UnitCrowdControl(CrowdControlType.Silence);

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
