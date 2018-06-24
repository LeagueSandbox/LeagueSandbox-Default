using System;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;
using System.Linq;
using LeagueSandbox.GameServer;
using System.Collections.Generic;

namespace Spells
{
    public class KatarinaQ : GameScript
    {
        private Particle _mark;
        private Champion _owningChampion;
        private Spell _owningSpell;
        private List<AttackableUnit> _markTarget;
        private bool _listenerAdded;

        public void OnActivate(Champion owner)
        {
            _owningChampion = owner;
            _markTarget = new List<AttackableUnit>();
            _owningSpell = null;
            _listenerAdded = false;
            _mark = null;
        }

        private void OnProc(AttackableUnit target, bool isCrit)
        {
            if(_mark == null || _markTarget == null || !_markTarget.Contains(target))
            {
                return;
            }


            ApiFunctionManager.RemoveParticle(_mark);

            var damage = new[] { 15, 30, 45, 60, 75 }[_owningSpell.Level - 1] + _owningChampion.GetStats().AbilityPower.Total * 0.15f;
            target.TakeDamage(_owningChampion, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);

            _mark = null;
            _markTarget.Remove(target);
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            _owningSpell = spell;

            spell.AddProjectileTarget("KatarinaQ", target);

            if (!_listenerAdded)
            {
                ApiEventManager.OnHitUnit.AddListener(this, _owningChampion, OnProc);
                _listenerAdded = true;
            }

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 625, true))
            {
                if (enemyTarget != null && enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team) && enemyTarget != target && enemyTarget != owner && target.GetDistanceTo(enemyTarget) < 100 && !ApiFunctionManager.UnitIsTurret(enemyTarget))
                {
                    ApiFunctionManager.CreateTimer(3.0f, () => {
                        ApiFunctionManager.AddParticle(owner, "katarina_bouncingBlades_mis.troy", enemyTarget.X, enemyTarget.Y);
                        spell.AddProjectileTarget("KatarinaQMark", enemyTarget);
                    });
                }
            }

            //WE NEED A TIMER ON THE Q TO BE FIXED
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var damage = new[] { 60, 85, 110, 135, 160 }[spell.Level - 1] + owner.GetStats().AbilityPower.Total * 0.45f;

            foreach (var enemyTarget in ApiFunctionManager.GetUnitsInRange(target, 625, true))
            {
                if (enemyTarget != null && enemyTarget.Team == CustomConvert.GetEnemyTeam(owner.Team) && enemyTarget != owner && !ApiFunctionManager.UnitIsTurret(enemyTarget))
                {
                    enemyTarget.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

                    if (!enemyTarget.IsDead || !ApiFunctionManager.UnitIsChampion(enemyTarget))
                    {
                        _markTarget.Add(enemyTarget);
                        _mark = ApiFunctionManager.AddParticleTarget(owner, "katarina_daggered.troy", enemyTarget);
                    }
                }
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            projectile.setToRemove();
            ApiFunctionManager.CreateTimer(6.0f, () =>
            {
                if (_mark == null)
                    return;
                ApiFunctionManager.RemoveParticle(_mark);
                _markTarget.Clear();
                _mark = null;
            });

        }

        public void OnUpdate(double diff)
        {
        }
    }
}
