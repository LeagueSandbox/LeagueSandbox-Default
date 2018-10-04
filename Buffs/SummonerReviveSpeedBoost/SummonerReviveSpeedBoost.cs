using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace SummonerReviveSpeedBoost
{
    internal class SummonerReviveSpeedBoost : IBuffGameScript
    {
        private StatsModifier _statMod;
        private Buff _visualBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = 1.25f;
            unit.AddStatModifier(_statMod);
            _visualBuff = AddBuffHudVisual("SummonerReviveSpeedBoost", 12.0f, 1, BuffType.COMBAT_ENCHANCER, unit);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            RemoveBuffHudVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
