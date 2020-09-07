using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace GangplankW
{
    internal class GangplankW : IBuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(IObjAIBase unit, ISpell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + (10f + 5f * ownerSpell.Level) / 100f;
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(IObjAIBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
