using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace Invulnerable
{
    internal class Invulnerable : IBuffGameScript
    {
        private UnitCrowdControl _crowd = new UnitCrowdControl(CrowdControlType.INVULNERABLE);

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

