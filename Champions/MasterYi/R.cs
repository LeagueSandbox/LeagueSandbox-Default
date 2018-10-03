using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class MasterYiHighlander : IGameScript
    {
        public void OnActivate(IChampion owner)
        {
        }

        private void ReduceCooldown(IAttackableUnit unit, bool isCrit)
        {
        //No Cooldown reduction on the other skills yet
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            var p = ApiFunctionManager.AddParticleTarget(owner, "Highlander_buf.troy", target, 1);
            ((ObjAiBase) target).AddBuffGameScript("Highlander", "Highlander", spell, 10.0f, true);
            ApiFunctionManager.CreateTimer(10.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
            });
            //No increased durations on kills and assists yet
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
