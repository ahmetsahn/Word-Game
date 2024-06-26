using Runtime.Signals;
using Zenject;

namespace Runtime.Core
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<SendAnswerButtonClickedSignal>();
            Container.DeclareSignal<CharacterButtonClickedSignal>();
            Container.DeclareSignal<StopTimerSignal>();
            Container.DeclareSignal<ResumeTimerSignal>();
        }
    }
}