using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.Logic.GameObjects.Missiles;
using LeagueSandbox.GameServer.Logic.GameObjects.Spells;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class LuluW : IGameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.SpellAnimation("SPELL2", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var champion = (Champion) target;
            if (champion.Team != owner.Team)
            {
                spell.AddProjectileTarget("LuluWTwo", target);
            }
            else
            {
                var p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_buf_02.troy", target, 1);
                ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_buf_01.troy", target, 1);
                var time = 2.5f + 0.5f * spell.Level;
                var buff = ((ObjAiBase) target).AddBuffGameScript("LuluWBuff", "LuluWBuff", spell);
                var visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluWBuff", time, 1, (ObjAiBase) target);
                ApiFunctionManager.CreateTimer(time, () =>
                {
                    ApiFunctionManager.RemoveParticle(p);
                    ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                    owner.RemoveBuffGameScript(buff);
                });
            }
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var champion = (Champion) target;
            var time = 1 + 0.25f * spell.Level;
            var buff = ((ObjAiBase) target).AddBuffGameScript("LuluWDebuff", "LuluWDebuff", spell);
            var visualBuff = ApiFunctionManager.AddBuffHudVisual("LuluWDebuff", time, 1, (ObjAiBase) target);
            var model = champion.Model;
            ChangeModel(owner.Skin, target);

            var p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_polymorph_01.troy", target, 1);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHudVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
                ApiFunctionManager.SetChampionModel((Champion) target, model);
            });
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }

        private void ChangeModel(int skinId, AttackableUnit target)
        {
            switch (skinId)
            {
                case 0:
                    ApiFunctionManager.SetChampionModel((Champion) target, "LuluSquill");
                    break;
                case 1:
                    ApiFunctionManager.SetChampionModel((Champion) target, "LuluCupcake");
                    break;
                case 2:
                    ApiFunctionManager.SetChampionModel((Champion) target, "LuluKitty");
                    break;
                case 3:
                    ApiFunctionManager.SetChampionModel((Champion) target, "LuluDragon");
                    break;
                case 4:
                    ApiFunctionManager.SetChampionModel((Champion) target, "LuluSnowman");
                    break;
            }
        }
    }
}
