using System.Linq;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer;

namespace Spells
{
<<<<<<< HEAD
    public class R : IGameScript
=======
    public class FallenOne : GameScript
>>>>>>> refs/remotes/origin/indev
    {
        GameScriptInformation info;
        Spell spell;
        Unit owner;
        public void OnActivate(GameScriptInformation scriptInfo)
        {
            info = scriptInfo;
            spell = info.OwnerSpell;
            owner = info.OwnerUnit;
            //Setup event listeners
            ApiEventManager.OnSpellCast.AddListener(this, spell, OnStartCasting);
            ApiEventManager.OnSpellFinishCast.AddListener(this, spell, OnFinishCasting);
        }
        public void OnDeactivate() { }
        public void OnStartCasting(Unit target){
            foreach (var enemyTarget in ApiFunctionManager.GetChampionsInRange(owner, 20000, true).Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "KarthusFallenOne", enemyTarget);
            }
        }
        public void OnFinishCasting(Unit target) {
            var ap = owner.GetStats().AbilityPower.Total;
            var damage = 100 + spell.Level * 150 + ap * 0.6f;
            foreach (var enemyTarget in ApiFunctionManager.GetChampionsInRange(owner, 20000, true).Where(x => x.Team == CustomConvert.GetEnemyTeam(owner.Team)))
            {
                owner.DealDamageTo(enemyTarget, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
            }
        }
     }
}