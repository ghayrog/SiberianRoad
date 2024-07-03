using UnityEngine;

namespace Map
{
    public sealed class MapPointController : MonoBehaviour
    {
        public IMapPoint MapPoint { get; private set; }

        [SerializeField]
        private  MapPointType _mapPoint;

        [SerializeField]
        private string _title;

        [SerializeField,TextArea(3,10)]
        private string _description;

        [SerializeField]
        private AudioClip _sound;

        [SerializeField]
        private AudioClip _music;

        private void Awake()
        {
            switch (_mapPoint)
            { 
                case MapPointType.InterestPoint:
                    MapPoint = new InterestPoint(_title, _description, _sound, _music);
                    break;
                case MapPointType.CheckPoint:
                    MapPoint = new CheckPoint(_title, _description, _sound, _music);
                    break;
                default:
                    break;
            }
        }
    }
}
