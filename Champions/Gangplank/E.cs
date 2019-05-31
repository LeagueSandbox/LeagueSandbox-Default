using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class RaiseMorale : GameScript
    {
        public void OnActivate(Champion owner)
        {

        }

        private void SelfWasDamaged()
        {
        }

        public void OnDeactivate(Champion owner)
        {

        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "pirate_raiseMorale_cas.troy", target, 1);
            Particle p2 = ApiFunctionManager.AddParticleTarget(owner, "pirate_raiseMorale_mis.troy", target, 1);
            Particle p3 = ApiFunctionManager.AddParticleTarget(owner, "pirate_raiseMorale_tar.troy", target, 1);
            var buff = ((ObjAIBase) target).AddBuffGameScript("GangplankE", "GangplankE", spell);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("RaiseMorale", 7.0f, 1, owner); // Add buff for teammates in skill range
            ApiFunctionManager.CreateTimer(7.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveParticle(p2);
                ApiFunctionManager.RemoveParticle(p3);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });				
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {		
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
