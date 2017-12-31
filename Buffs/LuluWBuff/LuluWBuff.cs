using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluWBuff
{
    class LuluWBuff : BuffGameScript
    {
        ChampionStatModifier statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            double ap = ownerSpell.Owner.GetStats().AbilityPower.Total * 0.001;
            statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus + 0.3f + (float)ap;
            unit.AddStatModifier(statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveStatModifier(statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}