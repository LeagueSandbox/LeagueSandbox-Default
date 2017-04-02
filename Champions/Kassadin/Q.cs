using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Kassadin
{
    public class Q : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){
            ApiFunctionManager.AddParticleTarget(owner, "Kassadin_Base_cas.troy", owner);
            ApiFunctionManager.AddParticleTarget(owner, "Kassadin_Base_Q_ShieldOn.troy", owner);
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            spell.AddProjectileTarget("NullLance", target, true);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            Particle P = ApiFunctionManager.AddParticleTarget(owner, "Kassadin_Base_Q_tar.troy", target);
            var ap = owner.GetStats().AbilityPower.Total * 0.7f;
            var damage = 30 + spell.Level * 50 + ap;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            projectile.setToRemove();

        }
        public void OnUpdate(double diff) {

        }
     }
}