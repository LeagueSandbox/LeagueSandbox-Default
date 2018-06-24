using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;

namespace MFStrut
{
    internal class MFStrut : BuffGameScript
    {
        private ChampionStatModifier _statMod;
        private float _currentStatMod;
        private double _currentTime;
        private double _lastUpdate;
        private ObjAIBase _ownerUnit;
        private Buff _visualBuff;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _currentTime = 0;
            _lastUpdate = 0;
            _currentStatMod = 25;
            _ownerUnit = unit;
            _statMod = new ChampionStatModifier();
            _statMod.MoveSpeed.FlatBonus = _currentStatMod;
            _ownerUnit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHUDVisual("MissFortuneStrutStacks", 5.0f, (int)_currentStatMod, _ownerUnit, -1);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHUDVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
            _currentTime += diff;
            if(_currentTime >= (_lastUpdate + 1000)) {
                if (_currentStatMod < 70)
                {
                    _currentStatMod += 8;
                    if (_currentStatMod > 70)
                    {
                        _currentStatMod = 70;
                    }
                    _ownerUnit.RemoveStatModifier(_statMod);
                    ApiFunctionManager.RemoveBuffHUDVisual(_visualBuff);
                    _statMod.MoveSpeed.FlatBonus = _currentStatMod;
                    _lastUpdate = _currentTime;
                    _ownerUnit.AddStatModifier(_statMod);
                    _visualBuff = ApiFunctionManager.AddBuffHUDVisual("MissFortuneStrutStacks", 5.0f, (int)_currentStatMod, _ownerUnit, -1);
                }
            }
        }
    }
}
