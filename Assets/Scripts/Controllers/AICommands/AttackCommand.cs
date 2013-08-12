using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class AttackCommand : AICommand
    {
        public Transform Target { get; set; }
        //public Weapon Weapon { get; set; }
        public String[] WeaponsToFire { get; set; }

        public AttackCommand(Transform target, String[] weaponsToFire)
        {
            Target = target;
            WeaponsToFire = weaponsToFire;
        }

        public AttackCommand(Transform target)
        {
            Target = target;
            WeaponsToFire = null;
        }

        public override bool Run()
        {

            Finished = false;
            if (!Target)
            {
                return false;
            }
            Character.WeaponsController.Fire(Target, WeaponsToFire);
            
            return true;

            //if (Character != null)
            //{
            //    Cannon = Character.GetComponentInChildren<Weapon>();
            //}
        }

        //public void LockOn(Transform transform)
        //{
        //    if (Weapon)
        //    {
        //        Target = transform;
        //        Character.WeaponsController.LockOn(transform);
        //    }
        //}

        //public void Fire(ActorController target)
        //{
        //    Weapon.Fire(target);
        //}
    }
}
