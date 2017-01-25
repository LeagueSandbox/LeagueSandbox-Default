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
     public class W
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {
            ApiFunctionManager.AddParticleTarget(owner, "ezreal_bow_yellow.troy", owner, 1, "L_HAND");
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {
            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 1000;
            Vector2 trueCoords = current + range;

            spell.AddProjectile("EzrealEssenceFluxMissile", trueCoords.X, trueCoords.Y);
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {
            
         }
         public static void onUpdate(double diff) {
            
         }
     }
 }