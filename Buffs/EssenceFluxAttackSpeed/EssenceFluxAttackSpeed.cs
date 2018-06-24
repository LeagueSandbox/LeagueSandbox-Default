using LeagueSandbox.GameServer.Logic.Content;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace EssenceFluxAttackSpeed
{
    internal class EssenceFluxAttackSpeed : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnUpdate(double diff)
        {

        }

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.AttackSpeed.PercentBonus = new float[] { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f }[ownerSpell.Level - 1];
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveStatModifier(_statMod);
        }
    }
}
