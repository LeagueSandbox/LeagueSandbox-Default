using System.Collections.Generic;
using System.Linq;
using GameServerCore.Enums;
using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.GameObjects;
using LeagueSandbox.GameServer.GameObjects.Spells;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class Recall : IGameScript
    {
        private Particle _recallParticle;

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            bool ownerHasRecallActive = owner.HasBuffGameScriptActive("Recall", "Recall");

            owner.AddBuffGameScript("Recall", "Recall", spell, 8.0f, true);

            if (!ownerHasRecallActive)
            {
                _recallParticle = AddParticleTarget(owner, "TeleportHome.troy", owner);
            }
            else
            {
                RemoveParticle(_recallParticle);

                _recallParticle = AddParticleTarget(owner, "TeleportHome.troy", owner);
            }
            
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

