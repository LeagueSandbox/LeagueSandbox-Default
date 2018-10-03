using GameServerCore.Domain.GameObjects;
using GameServerCore.Enums;
using LeagueSandbox.GameServer.API;
using GameServerCore.Domain.GameObjects;
using LeagueSandbox.GameServer.GameObjects.AttackableUnits.AI;
using LeagueSandbox.GameServer.GameObjects.Missiles;
using GameServerCore.Domain;
using LeagueSandbox.GameServer.Scripting.CSharp;

namespace Spells
{
    public class LuluW : IGameScript
    {
        public void OnActivate(IChampion owner)
        {
        }

        public void OnDeactivate(IChampion owner)
        {
        }

        public void OnStartCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            spell.SpellAnimation("SPELL2", owner);
        }

        public void OnFinishCasting(IChampion owner, ISpell spell, IAttackableUnit target)
        {
            var IChampion = (IChampion) target;
            if (IChampion.Team != owner.Team)
            {
                spell.AddProjectileTarget("LuluWTwo", target);
            }
            else
            {
                var p1 = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_buf_02.troy", target, 1);
                var p2 = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_buf_01.troy", target, 1);
                var time = 2.5f + 0.5f * spell.Level;
                ((ObjAiBase) target).AddBuffGameScript("LuluWBuff", "LuluWBuff", spell, time, true);
                ApiFunctionManager.CreateTimer(time, () =>
                {
                    ApiFunctionManager.RemoveParticle(p1);
                    ApiFunctionManager.RemoveParticle(p2);
                });
            }
        }

        public void ApplyEffects(IChampion owner, IAttackableUnit target, ISpell spell, IProjectile projectile)
        {
            // TODO: problematic code, if the target is only IAttackableUnit crash will occure
            var IChampion = (IChampion) target;
            var time = 1 + 0.25f * spell.Level;
            IChampion.AddBuffGameScript("LuluWDebuff", "LuluWDebuff", spell, time, true);
            var model = IChampion.Model;
            ChangeModel(owner.Skin, target);

            var p = ApiFunctionManager.AddParticleTarget(owner, "Lulu_W_polymorph_01.troy", target, 1);
            ApiFunctionManager.CreateTimer(time, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                IChampion.Model = model;
            });
            projectile.SetToRemove();
        }

        public void OnUpdate(double diff)
        {
        }

        private void ChangeModel(int skinId, IAttackableUnit target)
        {
            switch (skinId)
            {
                case 0:
                    ((IChampion) target).Model = "LuluSquill";
                    break;
                case 1:
                    ((IChampion) target).Model = "LuluCupcake";
                    break;
                case 2:
                    ((IChampion) target).Model = "LuluKitty";
                    break;
                case 3:
                    ((IChampion) target).Model = "LuluDragon";
                    break;
                case 4:
                    ((IChampion) target).Model = "LuluSnowman";
                    break;
            }
        }
    }
}
