using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer;

namespace Spells
{
    public class FallenOne : GameScript
    {
        public void OnActivate(Champion owner) { }
        public void OnDeactivate(Champion owner) { }
        public void OnStartCasting(Champion owner, Spell spell, ObjAIBase target){
            foreach (var enemyTarget in ApiFunctionManager.GetChampionsInRange(owner, 20000, true).Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                ApiFunctionManager.AddParticleTarget(owner, "KarthusFallenOne", enemyTarget);
            }
        }
        public void OnFinishCasting(Champion owner, Spell spell, ObjAIBase target) {
            var ap = owner.GetStats().AbilityPower.Total;
            var damage = 100 + spell.Level * 150 + ap * 0.6f;
            foreach (var enemyTarget in ApiFunctionManager.GetChampionsInRange(owner, 20000, true).Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                owner.DealDamageTo(enemyTarget, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
        }
        public void ApplyEffects(Champion owner, ObjAIBase target, Spell spell, Projectile projectile) {

        }
        public void OnUpdate(double diff) {

        }
     }
}