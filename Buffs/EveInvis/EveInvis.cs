using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Packets.PacketDefinitions.S2C;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace EveInvis
{
    internal class EveInvis : BuffGameScript
    {
        private double _currentTime;
        private double _lastUpdate;
        private ObjAIBase _ownerUnit;
        private Buff _visualBuff;
        
        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _currentTime = 0;
            _lastUpdate = 0;
            _ownerUnit = unit;
            _visualBuff = ApiFunctionManager.AddBuffHUDVisual("EveInvis", 5.0f, 5, _ownerUnit, -1);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            ApiFunctionManager.RemoveBuffHUDVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
            _currentTime += diff;
            if (_currentTime >= (_lastUpdate + 1000))
            {
                var manaRegenerated = _ownerUnit.GetStats().ManaPoints.Total / 100;
                var maxMana = _ownerUnit.GetStats().ManaPoints.Total;
                var newMana = _ownerUnit.GetStats().CurrentMana + manaRegenerated;
                if (maxMana >= newMana)
                {
                    _ownerUnit.GetStats().CurrentMana = newMana;
                }
                else
                {
                    _ownerUnit.GetStats().CurrentMana = maxMana;
                }

                _lastUpdate = _currentTime;
            }
        }
    }
}
