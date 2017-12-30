using LeagueSandbox.GameServer.Logic;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Enet;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.Packets;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using System.Collections.Generic;

namespace Spells
{
    public class Recall : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, ObjAIBase target)
        {

        }
        public void OnFinishCasting(Champion owner, Spell spell, ObjAIBase target)
        {
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("Recall", 8.0f, 1, owner);
            var AddParticle = ApiFunctionManager.AddParticleTarget(owner, "TeleportHome.troy", owner);
            ApiFunctionManager.CreateTimer(8.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.Recall(owner);
            });
        }
        public void ApplyEffects(Champion owner, ObjAIBase target, Spell spell, Projectile projectile)
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
