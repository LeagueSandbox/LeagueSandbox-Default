using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Blind
{
    class Blind : IGameScript
    {
        UnitCrowdControl crowd = new UnitCrowdControl(CrowdControlType.Blind);

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

            target.ApplyCrowdControl(crowd);
        }
        
        public void OnDeactivate()
        {
            target.RemoveCrowdControl(crowd);
        }
    }
}
