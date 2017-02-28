using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Global
{
    public class Recall : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, Unit target)
        {
        
        }
        public void OnFinishCasting(Champion owner, Spell spell, Unit target)
        {
            //addBuff("Recall", 8, owner, owner)
            ApiFunctionManager.AddParticleTarget(owner, "TeleportHome.troy", owner);
        }
        public void ApplyEffects(Champion owner, Unit target, Spell spell, Projectile projectile)
        {
        
        }
        public void OnUpdate(double diff)
        {

        }

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}
