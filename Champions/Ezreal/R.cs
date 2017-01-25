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
     public class R
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {

            ApiFunctionManager.AddParticleTarget(owner, "Ezreal_bow_huge.troy", owner, 1, "L_HAND");
        
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {

            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 20000;
            Vector2 trueCoords = current + range;

            spell.AddProjectile("EzrealTrueshotBarrage", trueCoords.X, trueCoords.Y, true);
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {

            int reduc = Math.Min(projectile.ObjectsHit.Count, 7);
            float bonusAD = owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue;
            float AP = owner.GetStats().AbilityPower.Total * 0.9f;
            float damage = 200 + spell.Level * 150 + bonusAD + AP;
            owner.dealDamageTo(target, damage * (1 - reduc / 10), DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);

        
         }
         public static void onUpdate(double diff) {
          
         }
     }
 }