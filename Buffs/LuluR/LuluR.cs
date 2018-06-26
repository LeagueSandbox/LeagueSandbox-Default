﻿using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace LuluR
{
    internal class LuluR : BuffGameScript
    {
        private StatsModifier _statMod;
        private float _healthBefore;
        private float _meantimeDamage;
        private float _healthNow;
        private float _healthBonus;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.Size.PercentBonus = _statMod.Size.PercentBonus + 1;
            _healthBefore = unit.Stats.CurrentHealth;
            _healthBonus = 150 + 150 * ownerSpell.Level;
            _statMod.HealthPoints.BaseBonus = _statMod.HealthPoints.BaseBonus + 150 + 150 * ownerSpell.Level;
            unit.Stats.CurrentHealth = unit.Stats.CurrentHealth + 150 + 150 * ownerSpell.Level;
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            _healthNow = unit.Stats.CurrentHealth - _healthBonus;
            _meantimeDamage = _healthBefore - _healthNow;
            float bonusDamage = _healthBonus - _meantimeDamage;
            unit.RemoveStatModifier(_statMod);
            if (unit.Stats.CurrentHealth > unit.Stats.HealthPoints.Total)
            {
                unit.Stats.CurrentHealth = unit.Stats.CurrentHealth - bonusDamage;
            }
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}

