using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting;


namespace ImpureShotsActive
{
    internal class ImpureShotsActive : BuffGameScript
    {
        private ChampionStatModifier _AttackSpeedMod;
        private ObjAIBase _owningUnit;
        private Spell _owningSpell;

        public void OnActivate(ObjAIBase buffOwner, Spell ownerSpell)
        {
            _owningSpell = ownerSpell;
            _owningUnit = buffOwner;
            _AttackSpeedMod = new ChampionStatModifier();
            _AttackSpeedMod.AttackSpeed.PercentBonus = (new float[] { 0.2f, 0.3f, 0.4f, 0.5f, 0.6f })[ownerSpell.Level - 1];
            buffOwner.AddStatModifier(_AttackSpeedMod);
            ApiEventManager.OnHitUnit.AddListener(this, buffOwner, OnAutoAttack);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this);
            unit.RemoveStatModifier(_AttackSpeedMod);
        }

        private void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
            if(target is ObjAIBase)
            {
                ((ObjAIBase)target).AddBuffGameScript("GrievousWounds", "GrievousWounds", _owningSpell, 2.0f, true);
            }
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
