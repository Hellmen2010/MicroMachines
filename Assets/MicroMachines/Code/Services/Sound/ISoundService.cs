using MicroMachines.Code.Data.Enums;
using MicroMachines.Code.Data.Progress;
using MicroMachines.Code.Data.StaticData.Sounds;
using MicroMachines.Code.Infrastructure.ServiceContainer;
using MicroMachines.Code.Services.PersistentProgress;
using MicroMachines.Code.Services.SaveLoad;

namespace MicroMachines.Code.Services.Sound
{
    public interface ISoundService : IService
    {
        void Construct(SoundData soundData, Settings userSettings, ISaveLoad saveLoad, IPersistentProgress progress);
        void PlayBackgroundMusic();
        void PlayEffectSound(SoundId soundId);
        void SetVolume(float volume);
        void PlayDrive(SoundId soundId);
        void PlayDrift(SoundId soundId);
        void StopDrive();
        void StopDrift();
    }
}