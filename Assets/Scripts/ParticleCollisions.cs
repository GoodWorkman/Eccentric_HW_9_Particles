using UnityEngine;

public class ParticleCollisions : MonoBehaviour
{
    [SerializeField] private ParticleSystem mainParticleSystem;
    [SerializeField] private ParticleSystem wallSubEmitter; // Саб-эмиттер для стены
    [SerializeField] private ParticleSystem floorSubEmitter; // Саб-эмиттер для пола

    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private LayerMask floorLayerMask;

    private void OnParticleCollision(GameObject other)
    {
        if ((wallLayerMask.value & (1 << other.layer)) != 0)
        {
            wallSubEmitter.Emit(1);
            Debug.Log("wall");
        }
        
        else if ((floorLayerMask.value & (1 << other.layer)) != 0)
        {
            Debug.Log("floura");

            floorSubEmitter.Emit(1);

            var particles = new ParticleSystem.Particle[mainParticleSystem.main.maxParticles];
            int numParticlesAlive = mainParticleSystem.GetParticles(particles);

            for (int i = 0; i < numParticlesAlive; i++)
            {
                particles[i].remainingLifetime = 0;
            }

            mainParticleSystem.SetParticles(particles, numParticlesAlive);
        }
    }
}