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
     public class Q
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {

        
        
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {

            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 1150;
            Vector2 trueCoords = current + range;

            spell.AddProjectile("CaitlynPiltoverPeacemaker", trueCoords.X, trueCoords.Y, true);
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {

            int reduc = Math.Min(projectile.ObjectsHit.Count, 5);
            owner.dealDamageTo(spell.Target, (spell.getEffectValue(0) + owner.GetStats().AttackDamage.Total + (1.3f * owner.GetStats().AttackDamage.Total)) + spell.getEffectValue(0) * (1 - reduc / 10), DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            ApiFunctionManager.AddParticleTarget(owner, "caitlyn_Base_peaceMaker_tar_02.troy", spell.Target);
        
         }
         public static void onUpdate(double diff) {
       
        
          
         }
     }
 }