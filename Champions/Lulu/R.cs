using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class LuluR : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_R_cas.troy", target, 1);
            var buff = ((ObjAiBase) target).AddBuffGameScript("LuluR", "LuluR", spell);
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluR", 7.0f, 1, owner);
            ApiFunctionManager.CreateTimer(7.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
                ApiFunctionManager.AddParticleTarget(owner, "Lulu_R_expire.troy", target, 1);
            });
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
