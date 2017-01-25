using System;
 using System.Collections.Generic;
 using System.Linq;
 using System.Text;
 using System.Threading.Tasks;
 using System.Numerics;
 using LeagueSandbox.GameServer.Logic.GameObjects;
 using LeagueSandbox.GameServer.Logic.API;

 namespace Lucian
 {
     public class Q
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {

            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 1100;
            Vector2 trueCoords = current + range;

            spell.AddLaser(trueCoords.X, trueCoords.Y, true);
            spell.spellAnimation("SPELL1", owner);
            ApiFunctionManager.AddParticle(owner, "Lucian_Q_laser.troy", trueCoords.X, trueCoords.Y);
            ApiFunctionManager.AddParticleTarget(owner, "Lucian_Q_tar.troy", owner);
        
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {
 
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {

            float damage = (owner.GetStats().AttackDamage.Total * (0.45f + (spell.Level * 0.15f))) + (50 + (spell.Level * 30));
            owner.dealDamageTo(spell.Target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        
         }
         public static void onUpdate(double diff) {
          
         }
     }
 }