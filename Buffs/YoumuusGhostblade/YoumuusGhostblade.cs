using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace YoumuusGhostblade
{
    internal class YoumuusGhostblade : IBuffGameScript
    {
        private StatsModifier _statMod;
        private Buff _visualBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = 0.2f;
            _statMod.AttackSpeed.PercentBonus = 0.4f;
            _visualBuff = AddBuffHudVisual("SpectralFury", 6.0f, 1, BuffType.COMBAT_ENCHANCER, unit);
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            RemoveBuffHudVisual(_visualBuff);
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
