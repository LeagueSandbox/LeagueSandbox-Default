using GameServerCore.Domain.GameObjects;
using GameServerCore.Domain;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.Scripting.CSharp;


namespace Spells
{
    public class Gemcraft : IGameScript
    {

        public void OnActivate(IChampion owner)
        {
            
        }
        
        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
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

