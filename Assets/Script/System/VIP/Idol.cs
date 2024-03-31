using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : VIP_Base
{
    [SerializeField] ParticleSystem particle;
    public override bool NecessaryCondition(Portal portal)
    {
        if (portal.people.Count > 0)
            return false;
        return true;
    }
    public override void Process()
    {
        GameManager.Instance.countPenalty += GameManager.Instance.maxCount;
    }
    public void PlayParticle()
    {
        particle.Play();
    }

    public override void PlayEventSound()
    {
        SoundManager.Instance.PlaySfx("Idol");
    }
}
