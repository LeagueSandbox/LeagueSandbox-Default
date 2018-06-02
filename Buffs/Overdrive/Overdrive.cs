using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace Overdrive
{
    internal class Overdrive : BuffGameScript
    {
        private float _movementSpeedBonus;
        private float _attackSpeedBonus;
        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _movementSpeedBonus = (12 + ownerSpell.Level * 4) / 100f;
            _attackSpeedBonus = (22 + 8 * ownerSpell.Level) / 100f;
            unit.Stats.AdditiveMovementSpeedBonus += _movementSpeedBonus;
            unit.Stats.PercentAttackSpeedMod += _attackSpeedBonus;
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.Stats.AdditiveMovementSpeedBonus -= _movementSpeedBonus;
            unit.Stats.PercentAttackSpeedMod -= _attackSpeedBonus;
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}

