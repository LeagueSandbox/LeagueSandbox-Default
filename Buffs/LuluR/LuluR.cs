using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluR
{
    class LuluR : BuffGameScript
    {
        ChampionStatModifier statMod;
        float healthBefore;
        float meantimeDamage;
        float healthNow;
        float healthBonus;

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            statMod = new ChampionStatModifier();
            statMod.Size.PercentBonus = statMod.Size.PercentBonus + 1;
            healthBefore = unit.GetStats().CurrentHealth;
            healthBonus = 150 + 150 * ownerSpell.Level;
            statMod.HealthPoints.BaseBonus = statMod.HealthPoints.BaseBonus + 150 + 150 * ownerSpell.Level;
            unit.GetStats().CurrentHealth = unit.GetStats().CurrentHealth + 150 + 150 * ownerSpell.Level;
            unit.AddStatModifier(statMod);
        }

        public void OnDeactivate(Unit unit)
        {
            healthNow = unit.GetStats().CurrentHealth - healthBonus;
            meantimeDamage = healthBefore - healthNow;
            float bonusDamage = healthBonus - meantimeDamage;
            unit.RemoveStatModifier(statMod);
            if (unit.GetStats().CurrentHealth > unit.GetStats().HealthPoints.Total)
            {
                unit.GetStats().CurrentHealth = unit.GetStats().CurrentHealth - bonusDamage;
            }
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
