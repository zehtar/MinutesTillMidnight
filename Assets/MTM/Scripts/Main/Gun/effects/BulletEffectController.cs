using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletEffectController : MonoBehaviour
{
    
        private IBulletEffect[] allEffects;
        private List<IBulletEffect> activeEffects = new();

        private void Awake()
        {
            allEffects = GetComponents<MonoBehaviour>().OfType<IBulletEffect>().ToArray();
        }

        public void SetActiveEffectsByIDs(List<string> effectIDs)
        {
            activeEffects.Clear();

            foreach (IBulletEffect effect in allEffects)
            {
                var mono = effect as MonoBehaviour;
                if (effectIDs.Contains(effect.EffectID))
                {
                    mono.enabled = true;
                    activeEffects.Add(effect);
                }
                else
                {
                    mono.enabled = false;
                }
            }
        }

        public void ApplyEffects(Enemy enemy, Vector2 hitPoint,Bullet orgBullet)
        {
            foreach (var effect in activeEffects)
            {
                effect.OnHit(enemy, hitPoint,orgBullet);
            }
        }
    }


