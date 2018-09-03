using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.GameObjects.Stats;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace HealSpeed
{
    internal class HealSpeed : IBuffGameScript
    {
        private StatsModifier _statMod;
        private Buff _healBuff;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.PercentBonus = 0.3f;
            unit.AddStatModifier(_statMod);
            _healBuff = ApiFunctionManager.AddBuffHudVisual("SummonerHeal", 1.0f, 1, unit);
        }

        public void OnDeactivate(ObjAiBase unit)
        {
            unit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHudVisual(_healBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
