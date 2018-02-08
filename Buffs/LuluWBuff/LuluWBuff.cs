using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace LuluWBuff
{
    internal class LuluWBuff : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            var ap = ownerSpell.Owner.GetStats().AbilityPower.Total * 0.001;
            _statMod = new ChampionStatModifier();
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus + 0.3f + (float)ap;
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
