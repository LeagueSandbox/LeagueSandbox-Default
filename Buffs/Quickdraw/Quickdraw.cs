using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace Quickdraw
{
    internal class Quickdraw : BuffGameScript
    {
        private float _bonusGiven;
        public void OnUpdate(double diff)
        {
            
        }

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _bonusGiven = 0.2f + 0.1f * ownerSpell.Level;
            unit.Stats.PercentAttackSpeedMod += _bonusGiven;
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.Stats.PercentAttackSpeedMod -= _bonusGiven;
        }
    }
}

