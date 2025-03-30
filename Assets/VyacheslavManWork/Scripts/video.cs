using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

[RequireComponent(typeof(VideoPlayer))]
[RequireComponent(typeof(RawImage))]
public class VideoPlayerController : MonoBehaviour
{
    [Header("Video Settings")]
    public VideoClip videoClip;

    public string videoUrl = "";

    public bool playOnAwake = true;

    public bool loop = true;

    public bool prepareOnStart = true;

    [Header("Audio Settings")]
    public bool enableAudio = true;

    private VideoPlayer videoPlayer;
    private RawImage rawImage;
    private AudioSource audioSource;

    void Awake()
    {
       
        videoPlayer = GetComponent<VideoPlayer>();
        rawImage = GetComponent<RawImage>();

      
        SetupVideoPlayer();

        
        SetupAudio();
    }

    void Start()
    {
        if (prepareOnStart)
        {
            PrepareVideo();
        }

        if (playOnAwake)
        {
            PlayVideo();
        }
    }

    void SetupVideoPlayer()
    {
       
        if (!string.IsNullOrEmpty(videoUrl))
        {
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = videoUrl;
        }
        else if (videoClip != null)
        {
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = videoClip;
        }

        videoPlayer.playOnAwake = false;
        videoPlayer.isLooping = loop;

       
        videoPlayer.prepareCompleted += OnVideoPrepared;

       
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;

       
        if (videoPlayer.targetTexture == null)
        {
            videoPlayer.targetTexture = new RenderTexture(1920, 1080, 24);
        }
    }

    void SetupAudio()
    {
        if (enableAudio)
        {
           
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;

            
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);
            videoPlayer.EnableAudioTrack(0, true);
        }
        else
        {
            videoPlayer.EnableAudioTrack(0, false);
        }
    }

    void OnVideoPrepared(VideoPlayer source)
    {
        
        rawImage.texture = source.texture;
        rawImage.color = Color.white;
    }

    public void PrepareVideo()
    {
        
        videoPlayer.Prepare();
    }

    public void PlayVideo()
    {
        if (videoPlayer.isPrepared)
        {
            videoPlayer.Play();
        }
        else
        {
            PrepareVideo();
            videoPlayer.prepareCompleted += (source) => source.Play();
        }
    }

    public void PauseVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
    }

    public void StopVideo()
    {
        videoPlayer.Stop();
    }

    public void TogglePlayPause()
    {
        if (videoPlayer.isPlaying)
        {
            PauseVideo();
        }
        else
        {
            PlayVideo();
        }
    }

    void OnDestroy()
    {
       
        if (videoPlayer != null)
        {
            videoPlayer.prepareCompleted -= OnVideoPrepared;
        }
    }
}