using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace Quickdraw
{
    class Quickdraw : BuffGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();
        public void OnActivate(Champion owner, Spell ownerSpell)
        {
            statMod.AttackSpeed.PercentBonus = (ownerSpell.Level + 2) * 10;
            owner.AddStatModifier(statMod);
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
