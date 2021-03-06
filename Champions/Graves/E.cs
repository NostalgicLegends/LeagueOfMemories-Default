using System.Numerics;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class GravesMove : GameScript
    {
        public void OnActivate(Champion owner)
        {
        }

        public void OnDeactivate(Champion owner)
        {
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            var range = to * 425;
            var trueCoords = current + range;

            ApiFunctionManager.DashToLocation(owner, trueCoords.X, trueCoords.Y, 1200, false, "Spell3");
            Particle p = ApiFunctionManager.AddParticleTarget(owner, "Graves_Move_OnBuffActivate.troy", owner);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("GravesSteroid", 5.0f, 90, owner);
            var buff = owner.AddBuffGameScript("Quickdraw", "Quickdraw", spell, -1, true);
            ApiFunctionManager.CreateTimer(4.0f, () =>
            {
                ApiFunctionManager.RemoveParticle(p);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
                owner.RemoveBuffGameScript(buff);
            });
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
