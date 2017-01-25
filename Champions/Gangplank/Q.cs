using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
 using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

 namespace Gangplank
 {
     public class Q
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {
 
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {
            
            spell.AddProjectileTarget("pirate_parley_mis", target, false);
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {

            Unit castTarget = target;
            float damage = owner.GetStats().AttackDamage.Total + -5 + (25 * spell.Level);
            float newGold = owner.GetStats().Gold + 3 + (1 * spell.Level);
            if(target != null && !ApiFunctionManager.IsDead(target))
            {
                if(castTarget.GetStats().CurrentHealth > damage)
                {
                    owner.dealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
                }
                else
                {
                    owner.GetStats().Gold = newGold;
                    owner.dealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
                }
                projectile.setToRemove();
            }
        
         }
         public static void onUpdate(double diff) {
          
         }
     }
 }