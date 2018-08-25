using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LeagueSandbox_Default.Buffs.Quickdraw
{
    internal class Quickdraw : IBuffGameScript
    {
        private StatsModifier _statMod = new StatsModifier();

        public void OnUpdate(double diff)
        {

        }

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod.AttackSpeed.PercentBonus = ownerSpell.Level * 10.0f / 100.0f;
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }
    }
}
