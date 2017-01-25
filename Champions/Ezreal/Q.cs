using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
 using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

 namespace Ezreal
 {
     public class Q
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {

            ApiFunctionManager.AddParticleTarget(owner, "ezreal_bow.troy", owner, 1, "L_HAND");

        
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {

            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 1150;
            Vector2 trueCoords = current + range;

            spell.AddProjectile("EzrealMysticShotMissile", trueCoords.X, trueCoords.Y);
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {

            float AD = owner.GetStats().AttackDamage.Total * 1.1f;
            float AP = owner.GetStats().AbilityPower.Total * 0.4f;
            float damage = 15 + (spell.Level * 20) + AD + AP;
            owner.dealDamageTo(target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, false);
            spell.LowerCooldown(0, 1);
            spell.LowerCooldown(1, 1);
            spell.LowerCooldown(2, 1);
            spell.LowerCooldown(3, 1);
            projectile.setToRemove();
        
         }
         public static void onUpdate(double diff) {
          
         }
     }
 }