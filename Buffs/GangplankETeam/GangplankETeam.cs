using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace GangplankETeam
{
    class GangplankETeam : BuffGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            statMod.AttackDamage.FlatBonus = (5 + ownerSpell.Level * 7) * 0.5f;
            statMod.MoveSpeed.PercentBonus = (5 + ownerSpell.Level * 3) / 200f;
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
