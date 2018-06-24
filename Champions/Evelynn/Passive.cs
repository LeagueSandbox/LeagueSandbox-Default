using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class EvelynnPassive : GameScript
    {

        private double _lastTakenDamage;
        private double _currentTime;
        private bool _invisActive;
        private Champion _owningChampion;
        private Spell _owningSpell;
        private BuffGameScriptController InvisBuff;

        public void OnActivate(Champion owner)
        {
            _lastTakenDamage = 0;
            _currentTime = 0;
            _owningChampion = owner;
            _invisActive = false;
            ApiEventManager.OnDamageTaken.AddListener(this, owner, SelfWasDamaged);
        }

        private void SelfWasDamaged()
        {
            _lastTakenDamage = _currentTime;
            if (_invisActive && (InvisBuff != null))
            {
                _owningChampion.RemoveBuffGameScript(InvisBuff);
            }
            _invisActive = false;
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {

            if (_owningChampion == null)
            {
                return;
            }

            if (_owningSpell == null)
            {
                _owningSpell = _owningChampion.GetSpell(14);
                if (_owningSpell == null)
                {
                    return;
                }
            }

            _currentTime += diff;
            if (_currentTime >= (_lastTakenDamage + 6000))
            {
                if (!_invisActive)
                {
                    InvisBuff = _owningChampion.AddBuffGameScript("EveInvis", "EveInvis", _owningSpell, -1, true);
                    _invisActive = true;
                }
                else
                {
                    InvisBuff.UpdateBuff(diff);
                }
            }
        }
    }
}