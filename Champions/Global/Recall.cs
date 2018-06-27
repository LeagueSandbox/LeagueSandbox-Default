using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class Recall : IGameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("Recall", 8.0f, 1, owner);
            var addParticle = ApiFunctionManager.AddParticleTarget(owner, "TeleportHome.troy", owner);
            ApiFunctionManager.CreateTimer(8.0f, () =>
            {
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                owner.Recall(owner);
            });
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
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

