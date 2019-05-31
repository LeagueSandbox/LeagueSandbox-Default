using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace GangplankE
{
    internal class GangplankE : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.AttackSpeed.PercentBonus = _statMod.AttackSpeed.PercentBonus + (10f + 20f * ownerSpell.Level) / 100f;
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + (10f + 5f * ownerSpell.Level) / 100f;
            _statMod.AttackDamage.PercentBonus = _statMod.AttackDamage.PercentBonus + (10f + 10f * ownerSpell.Level) / 100f;
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
