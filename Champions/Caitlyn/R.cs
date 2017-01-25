using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
 using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

 namespace Caitlyn
 {
     public class R
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {

        
        
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {
            Unit castTarget = target;
            Vector2 current = new Vector2(owner.X, owner.Y);
            if(Vector2.Distance(current, new Vector2(castTarget.X, castTarget.Y)) <= spell.getEffectValue(1))
            {
                spell.AddProjectileTarget("CaitlynAceintheHoleMissile", castTarget);
            }
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {
            Unit castTarget = target;
            if (castTarget != null && ApiFunctionManager.IsDead(castTarget))
            {
                float damage = spell.getEffectValue(0) + owner.GetStats().AttackDamage.Total * 2;
                owner.dealDamageTo(castTarget, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
            projectile.setToRemove();
        
         }
         public static void onUpdate(double diff) {
       
        
          
         }
     }
 }