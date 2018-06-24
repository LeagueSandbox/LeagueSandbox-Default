using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace YoumusBlade
{
    internal class YoumusBlade : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.AttackSpeed.PercentBonus = 0.4f;
            _statMod.MoveSpeed.PercentBonus = 0.2f;
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