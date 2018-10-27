using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.Stats;

namespace EzrealEssenceFluxBuff
{
    internal class EzrealEssenceFluxBuff : IBuffGameScript
    {
        private Buff _healBuff;
        private StatsModifier _statMod;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.AttackSpeed.PercentBonus = _statMod.AttackSpeed.PercentBonus 
                + (0.05f * ownerSpell.Level) + 0.15f;
            unit.AddStatModifier(_statMod);
            _healBuff = AddBuffHudVisual("EzrealEssenceFluxBuff", 35.0f, 1, BuffType.COMBAT_ENCHANCER, unit);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            RemoveBuffHudVisual(_healBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
