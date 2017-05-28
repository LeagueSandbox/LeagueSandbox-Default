using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Invulnerable
{
    class Invulnerable : IGameScript
    {
        UnitCrowdControl crowd = new UnitCrowdControl(CrowdControlType.Invulnerable);

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
