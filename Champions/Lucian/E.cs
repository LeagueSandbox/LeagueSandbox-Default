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
     public class E
     {
         public static void onStartCasting(Champion owner, Spell spell, Unit target)
         {
 
         }
         public static void onFinishCasting(Champion owner, Spell spell, Unit target)
         {

            Vector2 current = new Vector2(owner.X, owner.Y);
            Vector2 to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 range = to * 445;
            Vector2 trueCoords = current + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1500, false, "SPELL3");
        
         }
         public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
         {
 
         }
         public static void onUpdate(double diff) {
          
         }
     }
 }