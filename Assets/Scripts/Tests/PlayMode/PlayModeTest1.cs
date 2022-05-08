using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PlayModeTest1
{
    [UnityTest]
    public IEnumerator GameControl()
    {
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1);
        var weaponManager = GameObject.Find("WeaponHolder").GetComponent<WeaponManager>();
        var ak74 = GameObject.Find("AK74").GetComponent<Weapon>();
        var gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
        var UIStuff = GameObject.Find("UI stuff").GetComponent<PauseMenu>();
        int assertFlag = 1;
        ak74.FireWeapon();
        yield return new WaitForSeconds(1);
        weaponManager.selectedWeapon++;
        weaponManager.SelectWeapon();
        yield return new WaitForSeconds(1);
        var m1911 = GameObject.Find("M1911").GetComponent<Weapon>();
        yield return new WaitForSeconds(1);
        if (gameControl.targetsHit != 0)
        {
            assertFlag = 0;
        }
        UIStuff.Pause();
        if (gameControl.pauseStatus != global::GameControl.pauseStatusEnum.PAUSED)
        {
            assertFlag = 0;
        }
        yield return new WaitForSeconds(1);
        UIStuff.Resume();
        if (gameControl.pauseStatus != global::GameControl.pauseStatusEnum.RESUMED)
        {
            assertFlag = 0;
        }
        yield return new WaitForSeconds(1);
        UIStuff.LoadMenuSave();
        yield return new WaitForSeconds(1);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != 0)
        {
            assertFlag = 0;
        }

        Assert.AreEqual(1, assertFlag);
    }

    [UnityTest]
    public IEnumerator GameControl_moving()
    {
        SceneManager.LoadScene(3);
        yield return new WaitForSeconds(1);
        var weaponManager = GameObject.Find("WeaponHolder").GetComponent<WeaponManager>();
        var ak74 = GameObject.Find("AK74").GetComponent<Weapon>();
        var gameControl = GameObject.Find("GameControl").GetComponent<GameControl_Moving>();
        var UIStuff = GameObject.Find("UI stuff").GetComponent<PauseMenu_Moving>();
        int assertFlag = 1;
        ak74.FireWeapon();
        yield return new WaitForSeconds(1);
        weaponManager.selectedWeapon++;
        weaponManager.SelectWeapon();
        yield return new WaitForSeconds(1);
        if (gameControl.targetsHit != 0)
        {
            assertFlag = 0;
        }
        UIStuff.Pause();
        if (gameControl.pauseStatus != global::GameControl_Moving.pauseStatusEnum.PAUSED)
        {
            assertFlag = 0;
        }
        yield return new WaitForSeconds(1);
        UIStuff.Resume();
        if (gameControl.pauseStatus != global::GameControl_Moving.pauseStatusEnum.RESUMED)
        {
            assertFlag = 0;
        }
        yield return new WaitForSeconds(1);
        UIStuff.LoadMenuSave();
        yield return new WaitForSeconds(1);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != 0)
        {
            assertFlag = 0;
        }

        Assert.AreEqual(1, assertFlag);
    }

    [UnityTest]
    public IEnumerator GameControl_anticipation()
    {
        SceneManager.LoadScene(2);
        yield return new WaitForSeconds(1);
        var weaponManager = GameObject.Find("WeaponHolder").GetComponent<WeaponManager>();
        var ak74 = GameObject.Find("AK74").GetComponent<Weapon>();
        var gameControl = GameObject.Find("GameControl_Anticipation").GetComponent<GameControl_Anticipation>();
        var UIStuff = GameObject.Find("UI stuff").GetComponent<PauseMenu_Anticipation>();
        int assertFlag = 1;
        ak74.FireWeapon();
        yield return new WaitForSeconds(1);
        weaponManager.selectedWeapon++;
        weaponManager.SelectWeapon();
        yield return new WaitForSeconds(1);
        var m1911 = GameObject.Find("M1911").GetComponent<Weapon>();
        yield return new WaitForSeconds(1);
        if (gameControl.targetsHit != 0)
        {
            assertFlag = 0;
        }
        UIStuff.Pause();
        if (gameControl.pauseStatus != global::GameControl_Anticipation.pauseStatusEnum.PAUSED)
        {
            assertFlag = 0;
        }
        yield return new WaitForSeconds(1);
        UIStuff.Resume();
        if (gameControl.pauseStatus != global::GameControl_Anticipation.pauseStatusEnum.RESUMED)
        {
            assertFlag = 0;
        }
        yield return new WaitForSeconds(1);
        UIStuff.LoadMenuSave();
        yield return new WaitForSeconds(1);
        Scene scene = SceneManager.GetActiveScene();
        if (scene.buildIndex != 0)
        {
            assertFlag = 0;
        }

        Assert.AreEqual(1, assertFlag);
    }

    [UnityTest]
    public IEnumerator Menu()
    {
        SceneManager.LoadScene(0);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        yield return new WaitForSeconds(30);
        Assert.AreEqual(1, 1);
    }
}
