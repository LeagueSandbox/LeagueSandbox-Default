using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Overdrive
{
    class Overdrive : IGameScript
    {
        ChampionStatModifier statMod;

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
            
            statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus + (12f + owner.GetStats().Level * 4) / 100f;
            statMod.AttackSpeed.PercentBonus = statMod.AttackSpeed.PercentBonus + (22f + 8f * owner.GetStats().Level) / 100f;
            target.AddStatModifier(statMod);
        }

        public void OnDeactivate()
        {
            target.RemoveStatModifier(statMod);
        }
    }
}
