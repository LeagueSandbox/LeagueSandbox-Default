using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluR
{
    class LuluR : BuffGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            statMod.Size.PercentBonus = statMod.Size.PercentBonus + 1;
            statMod.HealthPoints.BaseBonus = statMod.HealthPoints.BaseBonus + 150 + 150 * ownerSpell.Level;
            unit.GetStats().CurrentHealth = unit.GetStats().CurrentHealth + 150 + 150 * ownerSpell.Level;
            unit.AddStatModifier(statMod);
        }

        public void OnDeactivate(Unit unit)
        {
            unit.RemoveStatModifier(statMod);
            if (unit.GetStats().CurrentHealth > unit.GetStats().HealthPoints.Total)
            {
                unit.GetStats().CurrentHealth = unit.GetStats().HealthPoints.Total;
            }
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
