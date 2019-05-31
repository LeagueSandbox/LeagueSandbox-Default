using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Scripting.CSharp;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Enums;
using GameServerCore;

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
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {		
            Particle p = AddParticleTarget(owner, "pirate_raiseMorale_cas.troy", target, 1);
            Particle p2 = AddParticleTarget(owner, "pirate_raiseMorale_mis.troy", target, 1);
            Particle p3 = AddParticleTarget(owner, "pirate_raiseMorale_tar.troy", target, 1);
            var buff = ((ObjAIBase) target).AddBuffGameScript("GangplankE", "GangplankE", spell);
            var visualBuff = AddBuffHUDVisual("RaiseMorale", 7.0f, 1, owner); // add hud visual
            
            var hasbuff = owner.HasBuffGameScriptActive("GangplankE", "GangplankE");
            
            foreach (var allyTarget in GetUnitsInRange(owner, 1000, true)
                .Where(x => x.Team != CustomConvert.GetEnemyTeam(owner.Team)))
            {
                if (allyTarget is IAttackableUnit && owner != allyTarget && hasbuff == false)
                {
                    ((ObjAIBase) allyTarget).AddBuffGameScript("GangplankE", "GangplankE", spell, 7.0f, true);
                    AddBuffHUDVisual("RaiseMorale", 7.0f, 1, target);
                    Particle p_ally1 = AddParticleTarget(owner, "pirate_raiseMorale_cas.troy", target, 1);
                    Particle p_ally2 = AddParticleTarget(owner, "pirate_raiseMorale_mis.troy", target, 1);
                    Particle p_ally3 = AddParticleTarget(owner, "pirate_raiseMorale_tar.troy", target, 1);
                }
            }
            
            
            CreateTimer(7.0f, () => // remove particle and buff (include hud visual) after 7 seconds
            {
                RemoveParticle(p);
                RemoveParticle(p2);
                RemoveParticle(p3);
                RemoveParticle(p_ally1);
                RemoveParticle(p_ally2);
                RemoveParticle(p_ally3);
                RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });				
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
