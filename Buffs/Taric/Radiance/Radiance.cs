using GameServerCore.Enums;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Radiance
{
    internal class Radiance : IBuffGameScript
    {
        private StatsModifier _statMod;
        private IBuff _healBuff;

        public void OnActivate(IObjAiBase unit, ISpell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.AttackDamage.FlatBonus += 10f + ownerSpell.Level * 20;
            _statMod.AbilityPower.FlatBonus += 10f + ownerSpell.Level * 20;
            unit.AddStatModifier(_statMod);
            var a =_healBuff = AddBuffHudVisual("Radiance", 10.0f, 1, BuffType.COMBAT_ENCHANCER, unit,10.0f);
            var b =_healBuff = AddBuffHudVisual("RadianceAura", 10.0f, 1, BuffType.COMBAT_ENCHANCER, unit,10.0f);
            //CreateTimer(10.0f, () =>
            // {
            //     RemoveBuffHudVisual(a);
            //     RemoveBuffHudVisual(b);
            // });
        }

        public void OnDeactivate(IObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}