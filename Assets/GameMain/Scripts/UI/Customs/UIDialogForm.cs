using UnityEngine;
using UnityEngine.UI;
using UnityGameFramework.Runtime;
using System.Collections;
using Fishing.Data;
using System;
namespace Fishing
{
    public class UIDialogForm : UGuiFormEx
    {
        [SerializeField]
        private Text dialog,LName,RName;
        [SerializeField]
        private Image leftHead, rightHead;
        [SerializeField]
        private ItemImage headImage;
        [SerializeField]
        private RectTransform LRoot, RRoot;
        private string dialogText;
        private float typeScan=0.2f;
        private Action dialogFinishCallback;
        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            DialogParams dialogParams = userData as DialogParams;
            DataCharacter dataCharacter = GameEntry.Data.GetData<DataCharacter>();
            if(dialogParams==null)
            {
                Log.Error("UIDialogForm open with invalid params");
                Close();
                return;
            }
            if (dialogParams.VoiceID != 0)
            {
                GameEntry.Sound.PlayMusic(dialogParams.VoiceID);
            }
            dialogText = dialogParams.Dialog;
            if(dialogParams.IsLeft)
            {
                LRoot.gameObject.SetActive(true);
                RRoot.gameObject.SetActive(false);
                leftHead.sprite = headImage.GetImage(((int)dialogParams.Character));
                LName.text = dataCharacter.GetCharacterData(dialogParams.Character).NameTag;
            }
            else{
                LRoot.gameObject.SetActive(false);
                RRoot.gameObject.SetActive(true);
                rightHead.sprite = headImage.GetImage(((int)dialogParams.Character));
                RName.text = dialogParams.Character.ToString();
                RName.text = dataCharacter.GetCharacterData(dialogParams.Character).NameTag;
            }
            dialogFinishCallback = dialogParams.UnitFinishCallback;
            dialogParams.Clear();
            GameEntry.Input.RegisterAnyKey(FinishType);
            dialog.text = null;
            StartCoroutine("TyperCo");
        }

        private void FinishType(){
            StopCoroutine("TyperCo");
            GameEntry.Input.UnregisterAnyKey(FinishType);
            dialog.text = dialogText;
            GameEntry.Input.RegisterAnyKey(EndDialog);
        }
        private void EndDialog(){
            GameEntry.Input.UnregisterAnyKey(EndDialog);
            GameEntry.Event.Fire(this, DialogUnitFinishEventArgs.Create(this));
            if(dialogFinishCallback!=null)dialogFinishCallback.Invoke();
            Close();
        }
        public IEnumerator TyperCo()
        {
            for (int i = 0; i < dialogText.Length;i++)
            {
                dialog.text += dialogText[i];
                yield return new WaitForSeconds(typeScan);
            }
            GameEntry.Input.UnregisterAnyKey(FinishType);
            GameEntry.Input.RegisterAnyKey(EndDialog);
            yield break;
        }
    }
}