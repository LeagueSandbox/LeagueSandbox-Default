using System.Linq;
using GameServerCore;
using GameServerCore.Domain;
using GameServerCore.Domain.GameObjects;
using GameServerCore.Enums;
using static LeagueSandbox.GameServer.API.ApiFunctionManager;
using LeagueSandbox.GameServer.Scripting.CSharp;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Stats;


namespace Spells
{
    public class Shatter : IGameScript
    {
        public StatsModifier _statMod;

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
            _statMod = new StatsModifier();
            var armor = owner.Stats.Armor.Total;
            var damage = spell.Level * 40 + armor * 0.2f;
            var reduce = spell.Level * 5 + armor * 0.05f;
            var p1 = AddParticleTarget(owner, "Shatter_nova.troy", owner, 1);
            _statMod.Armor.FlatBonus -= reduce;
            
            foreach (var enemys in GetUnitsInRange(owner, 375, true)
                .Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                var hasbuff = ((ObjAiBase)enemys).HasBuffGameScriptActive("TaricWDis", "TaricWDis");
                if (enemys is IAttackableUnit && enemys != owner)
                {
                    enemys.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
                    var p2 = AddParticleTarget(owner, "Shatter_tar.troy", enemys, 1);
                    ((ObjAiBase)enemys).AddBuffGameScript("TaricWDis", "TaricWDis", spell, 4f, true);

                    if (hasbuff == true)
                    {
                        return;
                    }
                    if (hasbuff == false)
                    {
                        ((ObjAiBase)enemys).AddStatModifier(_statMod);                        
                    }

                    CreateTimer(4f, () =>
                    {
                        ((ObjAiBase)enemys).RemoveStatModifier(_statMod);
                        RemoveParticle(p2);
                    });
                }
            }
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
         
        }

        public void OnUpdate(double diff)
        {
            
        }
    }
}
