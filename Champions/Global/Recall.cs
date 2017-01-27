using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
namespace Global
{
    public static class Recall
    {
        public static void onStartCasting(Champion owner, Spell spell, Unit target)
        {
        
        }
        public static void onFinishCasting(Champion owner, Spell spell, Unit target)
        {
            //addBuff("Recall", 8, owner, owner)
            ApiFunctionManager.AddParticleTarget(owner, "TeleportHome.troy", owner);
        }
        public static void applyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
        
        }
        public static void onUpdate(double diff)
        {
        
        }
    }
}
