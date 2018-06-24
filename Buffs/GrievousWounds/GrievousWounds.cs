using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting;

namespace GrievousWounds
{
    internal class GrievousWounds : BuffGameScript
    {
        private ObjAIBase _owningUnit;
        private Spell _owningSpell;
        private Particle _grievousParticle;
        private Buff _grievousBuff;

        public void OnActivate(ObjAIBase unit, Spell ownerSpell)
        {
            _owningUnit = unit;
            _owningSpell = ownerSpell;
            if (unit is Champion) // Add a visual buff if the effected unit is a Champion
            {
                _grievousParticle = ApiFunctionManager.AddParticleTarget((Champion)unit, "global_grievousWound_tar.troy", unit);
            }
            _grievousBuff = ApiFunctionManager.AddBuffHUDVisual("GrievousWound", -1, 1, unit, -1);
        }

        public void OnDeactivate(ObjAIBase unit)
        {
            if (_grievousParticle != null)
            {
                ApiFunctionManager.RemoveParticle(_grievousParticle);
            }
            ApiFunctionManager.RemoveBuffHUDVisual(_grievousBuff);
        }

        public void OnUpdate(double diff)
        {

        }
    }
}
