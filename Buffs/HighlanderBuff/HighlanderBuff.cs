using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace HighlanderBuff
{
    internal class HighlanderBuff : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.AttackSpeed.PercentBonus = (new float[] { 0.3f , 0.55f , 0.8f })[ownerSpell.Level - 1];
            _statMod.MoveSpeed.PercentBonus = (new float[] { 0.5f , 0.6f , 0.7f })[ownerSpell.Level - 1];
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