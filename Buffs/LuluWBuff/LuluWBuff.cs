using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluWBuff
{
    class LuluWBuff : BuffGameScript
    {
        ChampionStatModifier statMod;

        public void OnActivate(Unit unit, Spell ownerSpell)
        {
            statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus + 0.3f;
            //statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus + this.GetStats().AbilityPower.Total * 0.3;
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