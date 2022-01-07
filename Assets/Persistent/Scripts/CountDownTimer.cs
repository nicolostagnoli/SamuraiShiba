using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    public float currenTime=0;

    public float startingTime=50f;

    public GameObject bossScript;
    public GameObject feather;

    private BossMovement _bossMovement;

    [SerializeField] Text countDownText;

    private DarkParticleEffect _darkParticleEffect;
    private DarkParticleEffect _projectileDarkParticleEffect;

    // Start is called before the first frame update
    void Start()
    {
        currenTime = startingTime;
        _bossMovement=bossScript.GetComponent<BossMovement>();
        _darkParticleEffect = bossScript.GetComponentInChildren<DarkParticleEffect>();
        _projectileDarkParticleEffect = feather.GetComponentInChildren<DarkParticleEffect>();
    }

    // Update is called once per frame
    void Update()
    {
        currenTime -= 1 * Time.deltaTime;
        countDownText.text = currenTime.ToString("0");

        if (currenTime < 10)
        {
            countDownText.color = Color.red;
        }
        
        if (currenTime <= 0)
        {
            currenTime = 0;
            TriggerDarkMode();
        }
    }

    void TriggerDarkMode()
    {
        _bossMovement.TriggerDarkMode(30,4,20);
        _projectileDarkParticleEffect.activateDarkMode();
        _darkParticleEffect.activateDarkMode();
        countDownText.fontSize = 30;
        countDownText.text = "THE CRANE IS ON DARK MODE";
    }
}
