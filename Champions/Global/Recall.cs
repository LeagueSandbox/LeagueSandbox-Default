using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class Recall : IGameScript
    {
        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            // @TODO Find out if possible to check for champion movement
            owner.AddBuffGameScript("Recall", "Recall", spell, 8.0f, true);
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }

        public void OnActivate(IChampion owner)
        {

        }

        public void OnDeactivate(IChampion owner)
        {

        }
    }
}

