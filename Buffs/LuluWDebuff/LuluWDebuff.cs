using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluWDebuff
{
    class LuluWDebuff : BuffGameScript
    {
        UnitCrowdControl crowdDisarm = new UnitCrowdControl(CrowdControlType.Disarm);
        UnitCrowdControl crowdSilence = new UnitCrowdControl(CrowdControlType.Silence);
        ChampionStatModifier statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            statMod = new ChampionStatModifier();
            statMod.MoveSpeed.BaseBonus = statMod.MoveSpeed.BaseBonus - 60;
            unit.ApplyCrowdControl(crowdDisarm);
            unit.ApplyCrowdControl(crowdSilence);
            unit.AddStatModifier(statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveCrowdControl(crowdDisarm);
            unit.RemoveCrowdControl(crowdSilence);
            unit.RemoveStatModifier(statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}