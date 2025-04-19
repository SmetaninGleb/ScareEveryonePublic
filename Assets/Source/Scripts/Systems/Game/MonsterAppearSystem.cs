
using UnityEngine;
using Kuhpik;

class MonsterAppearSystem : GameSystem, IUpdating
{
    public void OnUpdate()
    {
        foreach (HideableComponent hideable in game.HideableList)
        {
            if (hideable.IsAppearing)
            {
                Transform spawner = hideable.HidingPlace.ScareSpawner.transform;
                hideable.transform.position = new Vector3(spawner.position.x, hideable.transform.position.y, spawner.position.z);
                hideable.transform.rotation = spawner.rotation;
                hideable.IsHiding = false;
                hideable.IsAppearing = false;
                hideable.Appear();
                hideable.TouchingHidePlace.IsHighlighted = false;
                hideable.RadiusSprite.SetActive(false);
                hideable.RadiusDraw.gameObject.SetActive(false);
                game.Player.HideableComponent.PoofParticles.transform.position = game.Player.transform.position;
                hideable.PoofParticles.Play();
                if (hideable.transform == game.Player.transform)
                {
                    game.Joystick.gameObject.SetActive(true);
                }
            }
        }
    }
}