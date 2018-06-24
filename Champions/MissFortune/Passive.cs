using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class MissFortuneStrut : GameScript
    {
        
        private double _currentTime;
        private double _lastTakenDamage;
        private bool _strutIsActive;
        private Champion _owningChampion;
        private Spell _owningSpell;
        private BuffGameScriptController StrutBuff;
        private Buff _visualBuff;

        public void OnActivate(Champion owner)
        {
            _lastTakenDamage = 0;
            _currentTime = 0;
            _owningChampion = owner;
            _strutIsActive = false;

            ApiEventManager.OnDamageTaken.AddListener(this, owner, SelfWasDamaged);
        }

        private void SelfWasDamaged()
        {
            _lastTakenDamage = _currentTime;
            if (_strutIsActive && (StrutBuff != null))
            {
                _owningChampion.RemoveBuffGameScript(StrutBuff);
            }
            _strutIsActive = false;
            if (_visualBuff == null)
            {
                _visualBuff = ApiFunctionManager.AddBuffHUDVisual("Miss Fortune Strut Cooldown", -1, 0, _owningChampion, -1);
            }
            ApiFunctionManager.LogInfo("Miss Fortune was damaged. Deactivating Strut.");
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
            if(_owningChampion == null)
            {
                return;
            }
            if(_owningSpell == null)
            {
                _owningSpell = _owningChampion.GetSpell(14);
                if(_owningSpell == null)
                {
                    return;
                }
            }
            _currentTime += diff;
            if (_currentTime >= (_lastTakenDamage + 5000)) // if damage has not been taken in 5 seconds, and Strut is not active, activate Strut.
            {
                if (!_strutIsActive)
                {
                    if(_visualBuff != null)
                    {
                        ApiFunctionManager.RemoveBuffHUDVisual(_visualBuff);
                        _visualBuff = null;
                    }
                    ApiFunctionManager.LogInfo("Miss Fortune has not been damaged in 5 seconds. Activating Strut.");
                    StrutBuff = _owningChampion.AddBuffGameScript("MFStrut", "MFStrut", _owningSpell, -1, true);
                    _strutIsActive = true;
                }
                else
                {
                    StrutBuff.UpdateBuff(diff);
                }
            }
        }
    }
}
