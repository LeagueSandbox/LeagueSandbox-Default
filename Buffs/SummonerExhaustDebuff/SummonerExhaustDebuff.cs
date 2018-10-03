using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace SummonerExhaustDebuff
{
    internal class SummonerExhaustDebuff : IBuffGameScript
    {
        private StatsModifier _statMod;
        private IBuff _visualBuff;

        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = -0.3f;
            _statMod.AttackSpeed.PercentBonus = -0.3f;
            _statMod.Armor.BaseBonus = -10;
            _statMod.MagicResist.BaseBonus = -10;
            unit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHudVisual("SummonerExhaustDebuff", 2.5f, 1, BuffType.COMBAT_DEHANCER,
                unit);
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHudVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
