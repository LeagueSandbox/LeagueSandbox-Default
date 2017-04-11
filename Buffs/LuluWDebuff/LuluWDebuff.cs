using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace LuluWDebuff
{
    class LuluWDebuff : IGameScript
    {
        UnitCrowdControl crowdDisarm = new UnitCrowdControl(CrowdControlType.Disarm);
        UnitCrowdControl crowdSilence = new UnitCrowdControl(CrowdControlType.Silence);
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
            statMod.MoveSpeed.BaseBonus = statMod.MoveSpeed.BaseBonus - 60;
            target.ApplyCrowdControl(crowdDisarm);
            target.ApplyCrowdControl(crowdSilence);
            target.AddStatModifier(statMod);
        }
        
        public void OnDeactivate()
        {
            target.RemoveCrowdControl(crowdDisarm);
            target.RemoveCrowdControl(crowdSilence);
            target.RemoveStatModifier(statMod);
        }
    }
}