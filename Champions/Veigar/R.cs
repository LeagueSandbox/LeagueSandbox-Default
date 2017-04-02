using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Veigar
{
    public class R : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){
            
            if (owner.Skin == 8)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_R_cas.troy", owner);

            }
            else
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_R_cas.troy", owner);
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            spell.AddProjectileTarget("VeigarPrimordialBurst", target);
            
            if (owner.Skin == 8)
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Skin08_R_tar.troy", target);

            }
            else
            {
                ApiFunctionManager.AddParticleTarget(owner, "Veigar_Base_R_tar.troy", target);
            }

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
            //120% of your own AP
            var ap = owner.GetStats().AbilityPower.Total * 1.2f;
            //80% of the targets AP
            var enemyAp = target.GetStats().AbilityPower.Total * 0.8F;
            // 250/375/500 Damage + Your AP + Enemy AP
            var damage = 125 + spell.Level * 125 + ap + enemyAp;
            owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            projectile.setToRemove();
        }
        public void OnUpdate(double diff) {

        }
     }
}