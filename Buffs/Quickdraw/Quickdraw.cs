using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Quickdraw
{
    class Quickdraw : IGameScript
    {
        ChampionStatModifier statMod = new ChampionStatModifier();
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        Unit target;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            target = info.TargetUnit;

            statMod.AttackSpeed.PercentBonus = owner.GetStats().Level * 10.0f / 100.0f;
            target.AddStatModifier(statMod);
        }

        public void OnDeactivate()
        {
            target.RemoveStatModifier(statMod);
        }
    }
}