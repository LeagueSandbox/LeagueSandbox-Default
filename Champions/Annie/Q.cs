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
    public class Disintegrate : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            spell.AddProjectileTarget("Disintegrate", target, false);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile) {
            var ap = owner.GetStats().AbilityPower.Total * 0.8f;
            var damage = 45 + spell.Level * 35 + ap;

            if (target != null && !ApiFunctionManager.IsDead(target))
            {
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                if (target.IsDead)
                {
                    spell.LowerCooldown(0, spell.getCooldown());
                    float manaToRecover = 55 + spell.Level * 5;
                    var newMana = owner.GetStats().CurrentMana + manaToRecover;
                    var maxMana = owner.GetStats().ManaPoints.Total;
                    if (newMana >= maxMana)
                    {
                        owner.GetStats().CurrentMana = maxMana;
                    }
                    else
                    {
                        owner.GetStats().CurrentMana = newMana;
                    }
                }
            }
            projectile.setToRemove();

        }
        public void OnUpdate(double diff) {

        }
     }
}