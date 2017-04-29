using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
<<<<<<< HEAD
    public class W : IGameScript
=======
    public class EzrealEssenceFlux : GameScript
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
            if (owner is Champion) ApiFunctionManager.AddParticleTarget(owner as Champion, "ezreal_bow_yellow.troy", owner, 1, "L_HAND");
        }
        public void OnFinishCasting(Unit target) {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 1000;
            var trueCoords = current + range;

            spell.AddProjectile("EzrealEssenceFluxMissile", trueCoords.X, trueCoords.Y);
        }
     }
}