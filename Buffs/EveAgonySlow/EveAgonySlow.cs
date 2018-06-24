using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveAgonySlow
{
    class EveAgonySlow : BuffGameScript
    {
        private ChampionStatModifier _statMod;
        private float _currentStatMod;
        private ObjAIBase _ownerUnit;
        private Buff _visualBuff;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _ownerUnit = unit;
            _currentStatMod = (new float[] { -0.30f, -0.50f, -0.70f })[ownerSpell.Level - 1];
            _statMod = new ChampionStatModifier();
            _statMod.MoveSpeed.PercentBonus = _currentStatMod;
            _ownerUnit.AddStatModifier(_statMod);
            _visualBuff = ApiFunctionManager.AddBuffHUDVisual("EveAgonySlow", 2.0f, 1, _ownerUnit);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
            ApiFunctionManager.RemoveBuffHUDVisual(_visualBuff);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
