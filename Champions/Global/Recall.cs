using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
namespace Global
{
    public static class Recall
    {
        public static void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
        
        }
        public static void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            //addBuff("Recall", 8, owner, owner)
            ApiFunctionManager.AddParticleTarget(owner, "TeleportHome.troy", owner);
        }
        public static void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
        
        }
        public static void OnUpdate(double diff)
        {
        
        }
    }
}
