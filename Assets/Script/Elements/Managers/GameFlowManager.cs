using com.ironicentertainment.Elements.Player.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;



namespace com.ironicentertainment.Common.Elements.Manager
{
    public class GameFlowManager : MonoBehaviour
    {
        public enum Scenes
        {
            Menu,
            Level_1,
            Level_2,
        }

        [SerializeField] private Scenes _Active;

        private void Start()
        {
            SetSceneStart();
        }

        private void Update()
        {
            UpdateScene();
        }

        public void SetSceneStart()
        {
            switch (_Active)
            {
                case Scenes.Menu:
                    StartMenu();
                    break;
                case Scenes.Level_1:
                    StartLevel_1();
                    break;
                case Scenes.Level_2:
                    StartLevel_2();
                    break;
                default:
                    break;
            }
        }

        public void UpdateScene()
        {
            switch (_Active)
            {
                case Scenes.Menu:
                    UpdateMenu();
                    break;
                case Scenes.Level_1:
                    UpdateLevel_1();
                    break;
                case Scenes.Level_2:
                    UpdateLevel_2();
                    break;
                default:
                    break;
            }
        }

        public void EndScene()
        {
            switch (_Active)
            {
                case Scenes.Menu:
                    EndMenu();
                    break;
                case Scenes.Level_1:
                    EndLevel_1();
                    break;
                case Scenes.Level_2:
                    EndLevel_2();
                    break;
                default:
                    break;
            }
        }

        private void StartMenu()
        {

        }

        private void StartLevel_1()
        {
            Animator lAnim = PlayerMSManager.Instance.Anim;

            lAnim.SetBool("OnGround", true);

            PlayerMSManager.Instance.Active = false;


        }

        public IEnumerator Level_1Cinematic()
        {
            float   lTime = 10f,
                    lWait= 0;

            while (lTime > lWait)
            {
                lWait += Time.deltaTime;



                yield return new WaitForSeconds(Time.deltaTime);
            }

            Animator lAnim = PlayerMSManager.Instance.Anim;

            lAnim.SetBool("OnGround", false);

            PlayerMSManager.Instance.Active = true;
        }

        private void StartLevel_2()
        {

        }

        private void UpdateMenu()
        {

        }

        private void UpdateLevel_1()
        {

        }

        private void UpdateLevel_2()
        {

        }

        private void EndMenu()
        {

        }

        private void EndLevel_1()
        {

        }

        private void EndLevel_2()
        {

        }
    }
}