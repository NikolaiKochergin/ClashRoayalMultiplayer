using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore
{
    public class Team
    {
        private readonly List<IDamageable> _towers = new List<IDamageable>();
        private readonly List<IDamageable> _walkingUnits = new List<IDamageable>();
        private readonly List<IDamageable> _flyUnits = new List<IDamageable>();

        public void Add(Tower tower) => 
            AddObjectToList(_towers, tower);

        public void Add(UnitBase unit)
        {
            switch (unit.Stats.MoveType)
            {
                case MoveType.Walk:
                    AddObjectToList(_walkingUnits, unit);
                    break;
                case MoveType.Fly:
                    AddObjectToList(_flyUnits, unit);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool TryGetNearestUnit(Vector3 currentPosition, IEnumerable<MoveType> attackTargetTypes, out IDamageable unit, out float distanceToCurrentTarget)
        {
            List<IDamageable> targets = new List<IDamageable>();
            foreach (MoveType attackTargetType in attackTargetTypes)
            {
                switch (attackTargetType)
                {
                    case MoveType.Walk:
                        IDamageable walker = GetNearest(currentPosition, _walkingUnits, out float walkingDistance);
                        if(walker != null)
                            targets.Add(walker);
                        break;
                    case MoveType.Fly:
                        IDamageable flyer = GetNearest(currentPosition, _flyUnits, out float flyingDistance);
                        if(flyer != null)
                            targets.Add(flyer);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            unit = GetNearest(currentPosition, targets, out distanceToCurrentTarget);
            return unit != null;
        }

        public bool TryGetNearestTower(in Vector3 currentPosition, out IDamageable tower, out float distance)
        {
            tower = GetNearest(currentPosition, _towers, out distance);
            return tower != null;
        }

        private static IDamageable GetNearest<T>(in Vector3 currentPosition, IEnumerable<T> objects, out float distance) where T : IDamageable
        {
            IDamageable nearestTarget = null;
            distance = float.MaxValue;
            
            foreach (T target in objects)
            {
                if(target.Health.CurrentValue == 0)
                    continue;
                
                float tempDistance = Vector3.Distance(currentPosition, target.Transform.position);
                if(tempDistance > distance)
                    continue;

                nearestTarget = target;
                distance = tempDistance;
            }

            return nearestTarget;
        }

        private static void AddObjectToList<T>(ICollection<T> list, T obj) where T : IDamageable
        {
            list.Add(obj);
            obj.Health.Died += RemoveAndUnsubscribe;
            return;

            void RemoveAndUnsubscribe()
            {
                RemoveObjectFromList(list, obj);
                obj.Health.Died -= RemoveAndUnsubscribe;
            }
        }

        private static void RemoveObjectFromList<T>(ICollection<T> list, T obj)
        {
            if (list.Contains(obj))
                list.Remove(obj);
        }
    }
}