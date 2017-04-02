using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace EzrealW
{
    class EzrealW : BuffGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            statMod.AttackSpeed.PercentBonus = 0.15f + ownerSpell.Level * 0.05f;
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
