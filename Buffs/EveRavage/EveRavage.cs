using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EveRavage
{
    class EveRavage : BuffGameScript
    {
        private ChampionStatModifier _statMod;
        private ObjAIBase _ownerUnit;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _ownerUnit = unit;
            _statMod = new ChampionStatModifier();
            _statMod.AttackSpeed.PercentBonus = (new float[] { .6f, .75f, .90f, 1.05f, 1.2f })[ownerSpell.Level - 1];
            _ownerUnit.AddStatModifier(_statMod);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            _ownerUnit.RemoveStatModifier(_statMod);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
