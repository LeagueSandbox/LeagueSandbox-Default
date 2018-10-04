using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class MasterYiHighlander : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        private void ReduceCooldown(AttackableUnit unit, bool isCrit)
        {
        //No Cooldown reduction on the other skills yet
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var p = AddParticleTarget(owner, "Highlander_buf.troy", target, 1);
            ((ObjAiBase) target).AddBuffGameScript("Highlander", "Highlander", spell, 10.0f, true);
            CreateTimer(10.0f, () =>
            {
                RemoveParticle(p);
            });
            //No increased durations on kills and assists yet
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
