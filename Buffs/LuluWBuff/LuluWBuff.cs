using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace LuluWBuff
{
    class LuluWBuff : IGameScript
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
            
            double ap = owner.GetStats().AbilityPower.Total * 0.001;
            statMod = new ChampionStatModifier();
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus + 0.3f + (float)ap;
            target.AddStatModifier(statMod);
        }

        public void OnDeactivate()
        {
            target.RemoveStatModifier(statMod);
        }
    }
}