using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace LuluWDebuff
{
    internal class LuluWDebuff : IBuffGameScript
    {
        private UnitCrowdControl _crowdDisarm = new UnitCrowdControl(CrowdControlType.DISARM);
        private UnitCrowdControl _crowdSilence = new UnitCrowdControl(CrowdControlType.SILENCE);
        private StatsModifier _statMod;

        public void OnActivate(ObjAiBase unit, Spell ownerSpell)
        {
            _statMod = new StatsModifier();
            _statMod.MoveSpeed.BaseBonus = _statMod.MoveSpeed.BaseBonus - 60;
            unit.ApplyCrowdControl(_crowdDisarm);
            unit.ApplyCrowdControl(_crowdSilence);
            unit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAiBase unit)
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
