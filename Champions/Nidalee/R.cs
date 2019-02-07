using GameServerCore.Domain.GameObjects;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class AspectOfTheCougar : IGameScript
    {
        static ISpell spell;
        public void OnActivate(IChampion owner)
        {
            //TODO:when game started, set level as 1;
            
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            var p1 = AddParticleTarget(owner, "Nidalee_Base_R_Cas.troy", owner, 1);
            if (owner.Model == "Nidalee_Cougar")
            {               
                var a = owner.Spells[0].Level;
                var b = owner.Spells[1].Level;
                var c = owner.Spells[2].Level;
                owner.ChangeModel("Nidalee");
                owner.Stats.Range.FlatBonus += 400;
                owner.SetSpell("JavelinToss", 0, true);
                owner.SetSpell("Bushwhack", 1, true);
                owner.SetSpell("PrimalSurge", 2,true);
                               
                for (byte p = 1; p <= a; p++)
                {
                    owner.Spells[0].LevelUp();
                }
                for (byte p = 1; p <= b; p++)
                {
                    owner.Spells[0].LevelUp();
                }
                for (byte p = 1; p <= c; p++)
                {
                    owner.Spells[2].LevelUp();                    
                }
                return;
            }
            if (owner.Model == "Nidalee")
            {                
                var a = owner.Spells[0].Level;
                var b = owner.Spells[1].Level;
                var c = owner.Spells[2].Level;
                owner.ChangeModel("Nidalee_Cougar");
                owner.Stats.Range.FlatBonus -= 400;
                owner.SetSpell("Takedown", 0, true);
                owner.SetSpell("Pounce", 1, true);
                owner.SetSpell("Swipe", 2,true);
                for (byte p = 1; p <= a; p++)
                {
                    owner.Spells[0].LevelUp();
                }
                for (byte p = 1; p <= b; p++)
                {
                    owner.Spells[1].LevelUp();
                }
                for (byte p = 1; p <= c; p++)
                {
                    owner.Spells[2].LevelUp();
                }
                return;
            }
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
