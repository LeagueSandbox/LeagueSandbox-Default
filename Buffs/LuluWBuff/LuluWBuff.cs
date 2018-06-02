using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace LuluWBuff
{
    internal class LuluWBuff : BuffGameScript
    {
        private float _givenBonus;
        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            var ap = ownerSpell.Owner.Stats.TotalAbilityPower * 0.001f;
            _givenBonus = 0.3f + ap;
            unit.Stats.AdditiveMovementSpeedBonus += _givenBonus;
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.Stats.AdditiveMovementSpeedBonus -= _givenBonus;
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
