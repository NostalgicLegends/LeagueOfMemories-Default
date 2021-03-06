﻿using System.Numerics;
using System;
using LeagueSandbox.GameServer.Logic;
using LeagueSandbox.GameServer.Logic.GameObjects;
using LeagueSandbox.GameServer.Logic.GameObjects.AttackableUnits;
using LeagueSandbox.GameServer.Logic.Scripting.CSharp;
using LeagueSandbox.GameServer.Logic.API;

namespace Spells
{
    public class GravesClusterShot : GameScript
    {

        public static Vector2 Rotate(Vector2 v, float angle, bool isAngleInRadians=false)
        {
            if (!isAngleInRadians)
            {
               angle = angle * ((float)Math.PI / 180);
            }
            var ca = (float)Math.Cos(angle);
            var sa = (float)Math.Sin(angle);
            return new Vector2((float)(ca * v.X - sa * v.Y), ((float)sa * v.X + ca * v.Y));
        }

        public void OnActivate(Champion owner)
        {

        }

        public void OnDeactivate(Champion owner)
        {                           
        }

        public void OnStartCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            spell.spellAnimation("SPELL1", owner);
        }

        public void OnFinishCasting(Champion owner, Spell spell, AttackableUnit target)
        {
            var current = new Vector2(owner.X, owner.Y);
            var to = Vector2.Normalize(new Vector2(spell.X, spell.Y) - current);
            Vector2 to2 = Rotate(to,15.32f);
            Vector2 to3 = Rotate(to,360 - 15.32f);

            var range = to * 950;
            var range2 = to2 * 950;
            var range3 = to3 * 950;
            var trueCoords1 = current + range;
            var trueCoords2 = current + range2;
            var trueCoords3 = current + range3;

            spell.AddProjectile("GravesClusterShotAttack", trueCoords1.X, trueCoords1.Y);
            spell.AddProjectile("GravesClusterShotAttack", trueCoords2.X, trueCoords2.Y);
            spell.AddProjectile("GravesClusterShotAttack", trueCoords3.X, trueCoords3.Y);
        }

        public void ApplyEffects(Champion owner, AttackableUnit target, Spell spell, Projectile projectile)
        {
            var bonusAD = owner.GetStats().AttackDamage.Total - owner.GetStats().AttackDamage.BaseValue;
            var ad = bonusAD * 0.150f;
            var damage = new[] {60, 95, 130, 165, 200}[spell.Level - 1] + ad;
            target.TakeDamage(owner, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, false);
        }

        public void OnUpdate(double diff)
        {
        }
    }
}
