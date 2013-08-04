﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    class AttackCommand : AICommand
    {
        public Transform Target { get; set; }
        public Weapon Weapon { get; set; }

        public override void Run()
        {
			Finished = false;
            if(!Target || !Weapon)
            {
                Finished = true;
            }

            LockOn(Target);


            //if (Character != null)
            //{
            //    Cannon = Character.GetComponentInChildren<Weapon>();
            //}
        }
        
        public override bool Process()
        {
            Weapon.Fire();

            return Finished;
        }

        public void LockOn(Transform transform)
        {
            if (Weapon)
            {
                Target = transform;
                Weapon.LockOn(transform);
            }
        }

        public void Fire(ActorController target)
        {
            Weapon.Fire(target);
        }
    }
}