using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
 using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

 namespace Blitzcrank
 {
     public class Q
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {

            spell.spellAnimation("SPELL1", owner);
        
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {

            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 925;
            Vector2 trueCoords = current + range;

            spell.AddProjectile("RocketGrabMissile", trueCoords.X, trueCoords.Y);
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {

            float AP = owner.GetStats().AbilityPower.Total;
            float damage = 25 + (spell.Level * 55) + AP;
            owner.dealDamageTo(spell.Target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

            if(!ApiFunctionManager.IsDead(spell.Target))
            {
                ApiFunctionManager.AddParticleTarget(owner, "Blitzcrank_Grapplin_tar.troy", spell.Target, 1, "L_HAND");
                Vector2 current = new Vector2(owner.X, owner.Y);
                Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
                Vector2 range = to * 50;
                Vector2 trueCoords = current + range;

                ApiFunctionManager.DashToLocation(spell.Target, trueCoords.X, trueCoords.Y, spell.ProjectileSpeed, true);
            }

            projectile.setToRemove();
        
         }
         public static void onUpdate(double diff) {
          
         }
     }
 }