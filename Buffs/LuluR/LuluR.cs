using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace LuluR
{
    internal class LuluR : BuffGameScript
    {
        private float _givenHealthBonus;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            unit.Stats.PercentSizeMod += 1;
            _givenHealthBonus = 150 + 150 * ownerSpell.Level;
            unit.Stats.FlatHealthBonus += _givenHealthBonus;
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.Stats.PercentSizeMod -= 1;
            unit.Stats.FlatHealthBonus -= _givenHealthBonus;
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}

