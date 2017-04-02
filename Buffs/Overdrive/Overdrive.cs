using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace Overdrive
{
    class Overdrive : BuffGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus + (12f + ownerSpell.Level * 4) / 100f;
            statMod.AttackSpeed.PercentBonus = statMod.AttackSpeed.PercentBonus + (22f + 8f * ownerSpell.Level) / 100f;
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
