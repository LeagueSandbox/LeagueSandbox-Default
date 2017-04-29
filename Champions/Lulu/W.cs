using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
<<<<<<< HEAD
    public class W : IGameScript
=======
    public class LuluW : GameScript
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
            ApiEventManager.OnSpellApplyEffects.AddListener(this, spell, ApplyEffects);
        }
        public void OnStartCasting(Unit target){
            spell.spellAnimation("SPELL2", owner);
         }
        public void OnDeactivate()
        {

        }
        public void OnFinishCasting(Unit target) {
            if (ApiFunctionManager.GetTeam(target) != ApiFunctionManager.GetTeam(owner))
            {
                spell.AddProjectileTarget("LuluWTwo", target);
            }
            else
            {
                Particle p = null;
                if (owner is Champion)
                {
                    p = ApiFunctionManager.AddParticleTarget(owner as Champion, "Lulu_W_buf_02.troy", target, 1);
                    ApiFunctionManager.AddParticleTarget(owner as Champion, "Lulu_W_buf_01.troy", target, 1);
                }
                float time = 2.5f + 0.5f * spell.Level;
                var buff = target.AddBuffGameScript("LuluWBuff", "LuluWBuff", spell);
                var visualBuff = ApiFunctionManager.AddBuffHUDVisual("LuluWBuff", time, 1, target);
                ApiFunctionManager.CreateTimer(time, () =>
                {
                    if (p != null) ApiFunctionManager.RemoveParticle(p);
                    ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                    owner.RemoveBuffGameScript(buff);
                });
            }
        }
        public void ApplyEffects(Unit target, Projectile projectile) {
            if (target is Champion)
            {
                Champion champion = (Champion)target;
                Champion ownerChampion = owner as Champion;
                float time = 1 + 0.25f * spell.Level;
                var buff = target.AddBuffGameScript("LuluWDebuff", "LuluWDebuff", spell);
                var visualBuff = ApiFunctionManager.AddBuffHUDVisual("LuluWDebuff", time, 1, target);
                string model = champion.Model;
                changeModel(ownerChampion.Skin, target);

                Particle p = ApiFunctionManager.AddParticleTarget(ownerChampion, "Lulu_W_polymorph_01.troy", target, 1);
                ApiFunctionManager.CreateTimer(time, () =>
                {
                    ApiFunctionManager.RemoveParticle(p);
                    ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                    owner.RemoveBuffGameScript(buff);
                    ApiFunctionManager.SetChampionModel((Champion)target, model);
                });
                projectile.setToRemove();
            }
         }
        void changeModel(int skinId, Unit target){
            switch(skinId)
            {
                case 0:
                    ApiFunctionManager.SetChampionModel((Champion)target, "LuluSquill");
                    break;
                case 1:
                    ApiFunctionManager.SetChampionModel((Champion)target, "LuluCupcake");
                    break;
                case 2:
                    ApiFunctionManager.SetChampionModel((Champion)target, "LuluKitty");
                    break;
                case 3:
                    ApiFunctionManager.SetChampionModel((Champion)target, "LuluDragon");
                    break;
                case 4:
                    ApiFunctionManager.SetChampionModel((Champion)target, "LuluSnowman");
                    break;
          }
      }
    }
}