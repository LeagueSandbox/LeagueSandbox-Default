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

        }

        public void OnActivate(Champion owner, Spell ownerSpell)
        {
            statMod.Size.PercentBonus = statMod.Size.PercentBonus + 1;
            owner.AddStatModifier(statMod);
        }

        public void OnDeactivate(Unit unit)
        {

        }

        public void OnDeactivate(Champion owner)
        {
            owner.RemoveStatModifier(statMod);
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
