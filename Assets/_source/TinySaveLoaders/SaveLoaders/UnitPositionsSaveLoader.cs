using System.Collections.Generic;
using TinyGame;
using TinySave;
using UnitPositions;
using UnityEngine;

namespace TinySaveLoaders
{
    public sealed class UnitPositionsSaveLoader : SaveLoader<UnitPositionsState, StaticUnitPositionsCollector>
    {
        protected override UnitPositionsState ConvertToData(StaticUnitPositionsCollector service)
        {
            UnitPositionsState state = new UnitPositionsState
            {
                transforms = new Dictionary<string, TransformStruct>()
            };

            foreach (KeyValuePair<string, Transform> position in service.transforms)
            {
                TransformStruct transform = new TransformStruct()
                {
                    x = position.Value.position.x,
                    y = position.Value.position.y,
                    z = position.Value.position.z,
                    eulerX = position.Value.rotation.eulerAngles.x,
                    eulerY = position.Value.rotation.eulerAngles.y,
                    eulerZ = position.Value.rotation.eulerAngles.z,
                };
                state.transforms[position.Key] = transform;
            }

            return state;
        }

        protected override void SetupData(StaticUnitPositionsCollector service, UnitPositionsState data)
        {
            foreach (KeyValuePair<string, TransformStruct> transform in data.transforms)
            {
                Vector3 vector3 = new Vector3(transform.Value.x, transform.Value.y, transform.Value.z);
                service.transforms[transform.Key].position = vector3;
                Quaternion rotaion = Quaternion.Euler(new Vector3(transform.Value.eulerX, transform.Value.eulerY, transform.Value.eulerZ));
                service.transforms[transform.Key].rotation = rotaion;
            }
        }
    }
}
