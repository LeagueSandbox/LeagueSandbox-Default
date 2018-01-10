using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace OverdriveSlow
{
    class OverdriveSlow : BuffGameScript
    {
        ChampionStatModifier statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus - 0.3f;
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
