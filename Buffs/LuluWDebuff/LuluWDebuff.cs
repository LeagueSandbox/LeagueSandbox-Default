using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluWDebuff
{
    internal class LuluWDebuff : BuffGameScript
    {
        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            unit.Stats.FlatMovementSpeedBonus -= 60;
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.Stats.FlatMovementSpeedBonus += 60;
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
