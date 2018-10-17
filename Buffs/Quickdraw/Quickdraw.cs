using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Quickdraw
{
    internal class Quickdraw : IBuffGameScript
    {
        private StatsModifier _statMod = new StatsModifier();
        private Buff _visualBuff;

        public void OnUpdate(double diff)
        {

        }

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod.AttackSpeed.PercentBonus = 0.2f + (0.1f * ownerSpell.Level);
            unit.AddStatModifier(_statMod);
            _visualBuff = AddBuffHudVisual("GravesMoveSteroid", 4.0f, 1, BuffType.COMBAT_ENCHANCER, 
                unit);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            RemoveBuffHudVisual(_visualBuff);
            unit.RemoveStatModifier(_statMod);
        }
    }
}
