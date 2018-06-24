using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using System.Collections.Generic;

namespace Spells
{
    public class ItemTiamatCleave : GameScript
    {

        private Champion _owningChampion;

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            ApiFunctionManager.AddParticleTarget(_owningChampion, "TiamatMelee_itm_active.troy", target);
            var closeRangeTargets = ApiFunctionManager.GetUnitsInRange(owner, 200, true);
            var midRangeTargets = ApiFunctionManager.GetUnitsInRange(owner, 300, true);
            var farRangeTargets = ApiFunctionManager.GetUnitsInRange(owner, 400, true);
            CorrectLists(closeRangeTargets, midRangeTargets, farRangeTargets);

            var ad = _owningChampion.GetStats().AttackDamage.Total;
            foreach (var unit in closeRangeTargets)
            {
                unit.TakeDamage(_owningChampion, ad * 0.6f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
            foreach (var unit in midRangeTargets)
            {
                unit.TakeDamage(_owningChampion, ad * 0.4f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
            foreach (var unit in farRangeTargets)
            {
                unit.TakeDamage(_owningChampion, ad * 0.2f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(Champion owner)
        {
            _owningChampion = owner;
            ApiEventManager.OnHitUnit.AddListener(this, owner, ApplyCleave);
        }

        private void ApplyCleave(AttackableUnit target, bool isCrit)
        {
            ApiFunctionManager.AddParticleTarget(_owningChampion, "TiamatMelee_itm.troy", target);
            var closeRangeTargets = ApiFunctionManager.GetUnitsInRange(target, 185, true);
            var midRangeTargets = ApiFunctionManager.GetUnitsInRange(target, 285, true);
            var farRangeTargets = ApiFunctionManager.GetUnitsInRange(target, 385, true);
            CorrectLists(closeRangeTargets, midRangeTargets, farRangeTargets);

            var ad = _owningChampion.GetStats().AttackDamage.Total;
            foreach(var unit in closeRangeTargets)
            {
                unit.TakeDamage(_owningChampion, ad * 0.6f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
            foreach(var unit in midRangeTargets)
            {
                unit.TakeDamage(_owningChampion, ad * 0.4f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
            foreach(var unit in farRangeTargets)
            {
                unit.TakeDamage(_owningChampion, ad * 0.2f, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
            }
        }

        // Remove units that overlap between the three tiamat ranges, and clear out allied units.
        private void CorrectLists(List<AttackableUnit> closeRange, List<AttackableUnit> midRange, List<AttackableUnit> farRange)
        {
            farRange.RemoveAll((unit) => (midRange.Contains(unit)));
            midRange.RemoveAll((unit) => (closeRange.Contains(unit)));
            closeRange.RemoveAll((unit) => (unit.Team == _owningChampion.Team));
            midRange.RemoveAll((unit) => (unit.Team == _owningChampion.Team));
            farRange.RemoveAll((unit) => (unit.Team == _owningChampion.Team));
        }

        public void OnDeactivate(Champion owner)
        {
            ApiEventManager.OnHitUnit.RemoveListener(this);
        }
    }
}

