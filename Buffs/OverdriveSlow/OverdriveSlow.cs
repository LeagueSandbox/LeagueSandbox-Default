using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace OverdriveSlow
{
    internal class OverdriveSlow : BuffGameScript
    {
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.MoveSpeed.PercentBonus = _statMod.MoveSpeed.PercentBonus - 0.3f;
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

