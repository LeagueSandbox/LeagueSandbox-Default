using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Garen
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.PlaySpellAnimation(owner, "Spell4");
            ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_R_Cas.troy", owner);

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_R_Sword_Tar.troy", target);
            spell.AddProjectileTarget("GarenR", target);
            ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_R_Tar_Impact.troy", target);
            ApplyDamage(owner, spell, target);
        }

        public void ApplyDamage(Champion owner, Spell spell, Unit target)
        {
            float misHealth = new[] { .286f, .333f, .40f}[spell.Level - 1] * (target.GetStats().HealthPoints.Total - target.GetStats().CurrentHealth);
            float damage = new[] { 175, 350, 525 }[spell.Level - 1] + misHealth;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            if (ApiFunctionManager.IsDead(target))
            {
                ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_R_Champ_Kill.troy", target);
                ApiFunctionManager.AddParticleTarget(owner, "Garen_Base_R_Champ_Death.troy", target);
            }
                
            
        }

        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            
        }
        public void OnUpdate(double diff) {

        }
     }
}