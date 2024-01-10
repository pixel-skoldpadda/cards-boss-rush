using Data;

namespace Infrastructure.Services.Audio
{
    public interface IAudioService
    {
        AudioSettings Settings { get; set; }
    }
}