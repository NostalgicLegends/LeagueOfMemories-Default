﻿using System.Collections.Generic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.API;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;

namespace Spells
{
    public class MissFortuneViciousStrikes : GameScript
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
            var buff = ((ObjAIBase)target).AddBuffGameScript("ImpureShotsActive", "ImpureShotsActive", spell, -1, true);
            var visualBuff = ApiFunctionManager.AddBuffHUDVisual("MissFortuneViciousStrikes", 5.0f, 1, owner);
            ApiFunctionManager.CreateTimer(6.0f, () =>
            {
                owner.RemoveBuffGameScript(buff);
                ApiFunctionManager.RemoveBuffHUDVisual(visualBuff);
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

