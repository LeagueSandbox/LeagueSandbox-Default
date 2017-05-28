using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace OverdriveSlow
{
    class OverdriveSlow : IGameScript
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
            statMod.MoveSpeed.PercentBonus = statMod.MoveSpeed.PercentBonus - 0.3f;
            target.AddStatModifier(statMod);
        }

        public void OnDeactivate()
        {
            target.RemoveStatModifier(statMod);
        }
    }
}
