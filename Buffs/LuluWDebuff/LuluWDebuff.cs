using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace LuluWDebuff
{
    internal class LuluWDebuff : BuffGameScript
    {
        private UnitCrowdControl _crowdDisarm = new UnitCrowdControl(CrowdControlType.Disarm);
        private UnitCrowdControl _crowdSilence = new UnitCrowdControl(CrowdControlType.Silence);
        private ChampionStatModifier _statMod;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _statMod = new ChampionStatModifier();
            _statMod.MoveSpeed.BaseBonus = _statMod.MoveSpeed.BaseBonus - 60;
            unit.ApplyCrowdControl(_crowdDisarm);
            unit.ApplyCrowdControl(_crowdSilence);
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            unit.RemoveCrowdControl(_crowdDisarm);
            unit.RemoveCrowdControl(_crowdSilence);
            unit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
