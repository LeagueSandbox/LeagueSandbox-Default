using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System;
using LeagueSandbox.GameServer.Logic;

namespace Spells
{
    public class MissFortuneViciousStrikes : GameScript
    {

        private double _currentTime;
        private bool _impureShotsLearned;
        private Champion _owningChampion;
        private Dictionary<ObjAIBase, double> _targetsByLastHitTime;
        private Dictionary<ObjAIBase, int> _targetsByStackCount;

        public void OnActivate(Champion owner)
        {
            _owningChampion = owner;
            _currentTime = 0;
            _impureShotsLearned = false;
            _targetsByLastHitTime = new Dictionary<ObjAIBase, double>();
            _targetsByStackCount = new Dictionary<ObjAIBase, int>();
            ApiEventManager.OnHitUnit.AddListener(this, owner, OnAutoAttack);
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            owner.AddBuffGameScript("ImpureShotsActive", "ImpureShotsActive", spell,6.0f, true);
            ApiFunctionManager.AddBuffHUDVisual("MissFortuneViciousStrikes", 6.0f, 1, owner, 6.0f);
        }

        public void OnAutoAttack(AttackableUnit target, bool isCrit = false)
        {
            if (!_impureShotsLearned)
            {
                Spell impureShots = _owningChampion.GetSpell(1);
                if (impureShots.Level >= 1)
                {
                    _impureShotsLearned = true;
                }
            }
            if (_impureShotsLearned)
            {
                ObjAIBase impureTarget = target as ObjAIBase;
                if (impureTarget != null)
                {
                    Spell bulletTime = _owningChampion.GetSpell(3);
                    if (!_targetsByLastHitTime.ContainsKey(impureTarget))
                    {
                        _targetsByLastHitTime[impureTarget] = _currentTime;
                        _targetsByStackCount[impureTarget] = 1;
                    }
                    else
                    {
                        _targetsByLastHitTime[impureTarget] = _currentTime;
                        var maxStacks = 5 + bulletTime.Level;
                        if (_targetsByStackCount[impureTarget] < maxStacks)
                        {
                            _targetsByStackCount[impureTarget] += 1;
                        }
                    }
                    var ad = _owningChampion.GetStats().AttackDamage.Total * (0.06f * _targetsByStackCount[impureTarget]);
                    target.TakeDamage(_owningChampion, ad, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
                }
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
            _currentTime += diff;
            List<ObjAIBase> impureTargets = new List<ObjAIBase>();
            foreach(var target in _targetsByLastHitTime.Keys)
            {
                impureTargets.Add(target);
            }
            foreach(var target in impureTargets)
            {
                if(_currentTime >= (_targetsByLastHitTime[target] + 5000))
                {
                    _targetsByLastHitTime.Remove(target);
                    _targetsByStackCount.Remove(target);
                }
            }
        }
    }
}

