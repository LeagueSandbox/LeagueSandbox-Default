using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class VayneSilveredBolts : GameScript
    {
        
        private bool _silverBoltsLearned;
        private double _lastAttackedValidTarget;
        private double _currentTime;
        private Champion _owningChampion;
        private ObjAIBase _lastTarget;
        private Spell _owningSpell;
        private byte _silverBoltsStacks;
        private Particle _silverBoltsParticle;
        private Buff _silverBoltsBuff;

        public void OnActivate(Champion owner)
        {
            _owningChampion = owner;
            ApiFunctionManager.CreateTimer(0.5f, () =>
             {
                 _owningSpell = owner.GetSpell(1);
             });
            ApiEventManager.OnHitUnit.AddListener(this, owner, OnAutoAttack);
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            _owningChampion = owner;
            _owningSpell = spell;
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        void OnAutoAttack(AttackableUnit target, bool isCrit)
        {
            if(_silverBoltsLearned == false)
            {
                if(_owningSpell.Level >= 1)
                {
                    _silverBoltsLearned = true;
                }
                else
                {
                    return;
                }
            }

            ObjAIBase silverTarget = target as ObjAIBase;
            if (silverTarget != null)
            {
                _lastAttackedValidTarget = _currentTime;

                if (_silverBoltsBuff != null)
                {
                    ApiFunctionManager.RemoveBuffHUDVisual(_silverBoltsBuff);
                    _silverBoltsBuff = null;
                }

                if (silverTarget != _lastTarget)
                {
                    if (_silverBoltsParticle != null)
                    {
                        ApiFunctionManager.RemoveParticle(_silverBoltsParticle);
                        _silverBoltsParticle = null;
                    }
                    if (_lastTarget != null)
                    {
                        _silverBoltsStacks = 0;
                    }
                    _lastTarget = silverTarget;
                }
                _silverBoltsStacks += 1;
                if (_silverBoltsStacks < 3) {
                    _silverBoltsBuff = ApiFunctionManager.AddBuffHUDVisual("VayneSilveredDebuff", 3.5f, _silverBoltsStacks, silverTarget, _owningChampion, -1);
                }
                if (_silverBoltsParticle != null)
                {
                    ApiFunctionManager.RemoveParticle(_silverBoltsParticle);
                }
                if (_silverBoltsStacks == 1)
                {
                    _silverBoltsParticle = ApiFunctionManager.AddParticleTarget(_owningChampion, "vayne_W_ring1.troy", silverTarget);
                }
                else if (_silverBoltsStacks == 2)
                {
                    _silverBoltsParticle = ApiFunctionManager.AddParticleTarget(_owningChampion, "vayne_W_ring2.troy", silverTarget);
                }
                else
                {
                    _silverBoltsStacks = 0; // We're at 3 stacks. Apply damage and reset to zero.
                    float healthRatio = (new float[] { 0.04f, 0.05f, 0.06f, 0.07f, 0.08f }[_owningSpell.Level - 1]) * silverTarget.GetStats().HealthPoints.Total;
                    float damage = new float[] { 20, 30, 40, 50, 60 }[_owningSpell.Level - 1] + healthRatio;
                    silverTarget.TakeDamage(_owningChampion, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PASSIVE, false);
                    ApiFunctionManager.AddParticleTarget(_owningChampion, "vayne_W_tar.troy", silverTarget);
                    _lastTarget = null;
                }

            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
            _currentTime += diff;
            if((_lastTarget != null) && _currentTime >= (_lastAttackedValidTarget + 3500)) // if Vayne has not damaged a valid target in 3.5 seconds
            {
                if (_silverBoltsParticle != null)
                {
                    ApiFunctionManager.RemoveParticle(_silverBoltsParticle);
                }
                _lastTarget = null;
                _silverBoltsParticle = null;
                _silverBoltsStacks = 0;
                if (_silverBoltsBuff != null)
                {
                    ApiFunctionManager.RemoveBuffHUDVisual(_silverBoltsBuff);
                }
                _silverBoltsBuff = null;
            }
        }
    }
}
