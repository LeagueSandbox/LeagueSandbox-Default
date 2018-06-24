using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace MoltenShield
{
    internal class MoltenShield : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.Armor.FlatBonus = (new float[] { 20, 30, 40, 50, 60 })[ownerSpell.Level - 1];
            _statMod.MagicResist.FlatBonus = (new float[] { 20, 30, 40, 50, 60 })[ownerSpell.Level - 1];
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