using GameServerCore.Enums;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Radiance_ally
{
    internal class Radiance_ally : IBuffGameScript
    {
        private StatsModifier _statMod;
        private IBuff _healBuff;

        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.AttackDamage.FlatBonus += (10f + ownerSpell.Level * 20) / 2;
            _statMod.AbilityPower.FlatBonus += (10f + ownerSpell.Level * 20) / 2;
            unit.AddStatModifier(_statMod);
            _healBuff = AddBuffHudVisual("Radiance", 10.0f, 1, BuffType.COMBAT_ENCHANCER, unit,10.0f);
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            //RemoveBuffHudVisual(_healBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}