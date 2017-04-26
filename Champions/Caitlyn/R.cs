using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class CaitlynAceintheHole : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            spell.AddProjectileTarget("CaitlynAceintheHoleMissile", target);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {
            if (target != null && !target.IsDead)
            {
                // 250/475/700
                var damage = 250 + owner.GetStats().AttackDamage.Total * 2;
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
            projectile.setToRemove();
        }
        public void OnUpdate(double diff) {

        }
     }
}