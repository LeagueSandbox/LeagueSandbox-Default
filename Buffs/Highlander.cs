using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Highlander
{
    internal class Highlander : IBuffGameScript
    {
        private StatsModifier _statMod;
        private Buff _visualBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + (15f + ownerSpell.Level * 10) / 100f;
            _statMod.AttackSpeed.PercentBonus = _statMod.AttackSpeed.PercentBonus + (5f + ownerSpell.Level * 25) / 100f;
            unit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("Highlander", 10.0f, 1, BuffType.COMBAT_ENCHANCER, unit);
            //Immunity to slowness not added
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
            unit.RemoveStatModifier(_statMod);
        }

        private void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
