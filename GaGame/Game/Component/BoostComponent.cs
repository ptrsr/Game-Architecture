using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaGame
{
    class BoostComponent : Component
    {
        // settings
        private float _respawnTime;

        protected override void Start()
        {
            // apply settings
            _respawnTime = ServiceLocator.Locate<Settings>().Boost_Respawn;

            // add image and collider
            ImageComponent imageComponent = Parent.AddComponent<ImageComponent>();
            imageComponent.SetImage("booster.png");

            Parent.AddComponent<BoxColliderComponent>();

            // setup event handling
            Events.reset += Reset;
        }

        public override void OnCollision(GameObject other)
        {
            if (other.Tag != Tags.Tag.Ball)
                return;

            // respawn of boost
            Parent.Active = false;
            Time.Timeout(Parent.Name, _respawnTime, ReSpawn);
        }

        private void Reset(bool hard)
        {
            ReSpawn();
        }

        private void ReSpawn()
        {
            Parent.Active = true;
        }
    }
}
