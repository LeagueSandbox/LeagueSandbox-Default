using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace GangplankE
{
    class GangplankE : BuffGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();

        public void OnActivate(Unit unit, Spell ownerSpell)
        {

            statMod.AttackDamage.BaseBonus = 5 + ownerSpell.Level * 7;
            statMod.MoveSpeed.PercentBonus = (5 + ownerSpell.Level * 3) / 100f;
            unit.AddStatModifier(statMod);

            
        }

        public void OnDeactivate(Unit unit)
        {
            unit.RemoveStatModifier(statMod);
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
