using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace Quickdraw
{
    internal class Quickdraw : BuffGameScript
    {
        private StatsModifier _statMod = new StatsModifier();

        public void OnUpdate(double diff)
        {
            
        }

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod.AttackSpeed.PercentBonus = ownerSpell.Level * 10.0f / 100.0f;
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }
    }
}
