using LeagueSandbox.GameServer.API;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace LeagueSandbox_Default.Champions.Blitzcrank
{
    public class Overdrive : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var p = ApiFunctionManager.AddParticleTarget(owner, "Overdrive_buf.troy", target, 1);
            var buff = ((ObjAiBase) target).AddBuffGameScript("Overdrive", "Overdrive", spell);
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("Overdrive", 8.0f, 1, owner);
            ApiFunctionManager.CreateTimer(8.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                ((ObjAiBase) target).RemoveBuffGameScript(buff);
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
