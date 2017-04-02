using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Warwick
{
    public class Q : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, Unit target){

        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target) {
            ApplyDamage(owner, target, spell);

        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {

        }
        public void OnUpdate(double diff) {

        }

        public void ApplyDamage(Champion owner, Unit target, Spell spell)
        {
            //Damage scales with 100% AP
            var ap = owner.GetStats().AbilityPower.Total;
            // Damage = 5 / 7.5 / 10 / 12.5 / 15% of target's maximum health
            var damagePer = target.GetStats().HealthPoints.Total * (0.06f + spell.Level * 0.02f);
            // Damage = 75 / 125 / 175 / 225 / 275
            var damageFlat = 25 + spell.Level * 50;

            //Decides if damageFlat or damagePer has the higher damage 

            if (target is Monster)
            {
                var damage = damageFlat + ap;
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

                var newHealth = owner.GetStats().CurrentHealth + damage * 0.8f;
                var maxHealth = owner.GetStats().HealthPoints.Total;
                if (newHealth >= maxHealth)
                {
                    owner.GetStats().CurrentHealth = maxHealth;
                }
                else
                {
                    owner.GetStats().CurrentHealth = newHealth;
                }
            }
            if (damageFlat > damagePer)
            {
                var damage = damageFlat + ap;
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

                var newHealth = owner.GetStats().CurrentHealth + damage * 0.8f;
                var maxHealth = owner.GetStats().HealthPoints.Total;
                if (newHealth >= maxHealth)
                {
                    owner.GetStats().CurrentHealth = maxHealth;
                }
                else
                {
                    owner.GetStats().CurrentHealth = newHealth;
                }
            }
            else
            {
                var damage = damagePer + ap;
                owner.DealDamageTo(target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

                var newHealth = owner.GetStats().CurrentHealth + damage * 0.8f;
                var maxHealth = owner.GetStats().HealthPoints.Total;
                if (newHealth >= maxHealth)
                {
                    owner.GetStats().CurrentHealth = maxHealth;
                }
                else
                {
                    owner.GetStats().CurrentHealth = newHealth;
                }
            }
        }
     }
}