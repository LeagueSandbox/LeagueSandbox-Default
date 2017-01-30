using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

 namespace Caitlyn
 {
     public class R
     {
         public static void OnStartCasting(Champion owner, Spell spell, Unit target)
         {

        
        
         }
         public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
         {
            Unit castTarget = target;
            Vector2 current = new Vector2(owner.X, owner.Y);
            if(Vector2.Distance(current, new Vector2(castTarget.X, castTarget.Y)) <= spell.getEffectValue(1))
            {
                spell.AddProjectileTarget("CaitlynAceintheHoleMissile", castTarget);
            }
        
         }
         public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {
            Unit castTarget = target;
            if (castTarget != null && ApiFunctionManager.IsDead(castTarget))
            {
                float damage = spell.getEffectValue(0) + owner.GetStats().AttackDamage.Total * 2;
                owner.DealDamageTo(castTarget, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
            projectile.setToRemove();
        
         }
         public static void OnUpdate(double diff) {
       
        
          
         }
     }
 }