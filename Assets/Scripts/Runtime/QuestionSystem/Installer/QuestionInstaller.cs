using System.Collections.Generic;
using Runtime.QuestionSystem.Model;
using Runtime.QuestionSystem.Model.ScriptableObject;
using Runtime.QuestionSystem.Presenter;
using UnityEngine;
using Zenject;

namespace Runtime.QuestionSystem.Installer
{
    public class QuestionInstaller : MonoInstaller
    {
        [SerializeField]
        private List<QuestionSo> fourCharacterQuestionsSo;
        [SerializeField]
        private List<QuestionSo> fiveCharacterQuestionsSo;
        [SerializeField]
        private List<QuestionSo> sixCharacterQuestionsSo;
        [SerializeField]
        private List<QuestionSo> sevenCharacterQuestionsSo;
        [SerializeField]
        private List<QuestionSo> eightCharacterQuestionsSo;
        [SerializeField]
        private List<QuestionSo> nineCharacterQuestionsSo;
        [SerializeField]
        private List<QuestionSo> tenCharacterQuestionsSo;
        
        public override void InstallBindings()
        {
            Container.Bind<QuestionModel>().AsSingle()
                .WithArguments(
                    fourCharacterQuestionsSo,
                    fiveCharacterQuestionsSo,
                    sixCharacterQuestionsSo,
                    sevenCharacterQuestionsSo,
                    eightCharacterQuestionsSo,
                    nineCharacterQuestionsSo,
                    tenCharacterQuestionsSo)
                .NonLazy();
            Container.BindInterfacesTo<QuestionPresenter>().AsSingle().NonLazy();
        }
    }
}