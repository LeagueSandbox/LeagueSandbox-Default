using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;

namespace Spells
{
    public class YoumusBlade : GameScript
    {
        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var buff = ((ObjAIBase)target).AddBuffGameScript("YoumusBlade", "YoumusBlade", spell,6.0f);
            var p = ApiFunctionManager.AddParticleTarget(owner, "spectral_fury_activate_speed.troy", owner, 2);
            ApiFunctionManager.CreateTimer(6.0f, () =>
             {
                 ApiFunctionManager.RemoveParticle(p);
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

        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }
    }
}
