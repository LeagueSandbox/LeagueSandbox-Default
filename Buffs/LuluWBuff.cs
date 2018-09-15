using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LuluWBuff
{
    internal class LuluWBuff : IBuffGameScript
    {
        private StatsModifier _statMod;
        private Buff _visualBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            var ap = ownerSpell.Owner.Stats.AbilityPower.Total * 0.001;
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + 0.3f + (float)ap;
            unit.AddStatModifier(_statMod);
            var time = 2.5f + 0.5f * ownerSpell.Level;
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluWBuff", time, 1, BuffType.COMBAT_ENCHANCER,
                unit);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
