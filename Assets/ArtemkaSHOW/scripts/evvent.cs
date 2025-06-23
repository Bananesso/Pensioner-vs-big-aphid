using UnityEngine;
using System.Collections.Generic;
using System;

public class EventGameSystem : MonoBehaviour
{
    [System.Serializable]
    public class GameEvent
    {
        [Tooltip("Уникальное название события")]
        public string eventName;

        [Tooltip("Сообщение при начале события")]
        [TextArea(3, 5)]
        public string startMessage;

        [Tooltip("Длительность события в секундах")]
        public float duration = 60f;

        [Tooltip("Шанс срабатывания (от 0 до 1)")]
        [Range(0f, 1f)]
        public float chance = 0.5f;

        [Tooltip("Задержка перед началом события")]
        public float delayBeforeStart = 0f;

        [Tooltip("Звук при начале события")]
        public AudioClip startSound;

        [Tooltip("Звук при завершении события")]
        public AudioClip endSound;

        [HideInInspector] public bool isActive;
        [HideInInspector] public float remainingTime;
    }

    [Header("Основные настройки")]
    [Tooltip("Список всех возможных событий")]
    [SerializeField]
    private List<GameEvent> events = new List<GameEvent>();

    [Tooltip("Интервал проверки новых событий в секундах")]
    public float checkInterval = 10f;

    [Header("Ссылки")]
    [Tooltip("Компонент для воспроизведения звуков")]
    [SerializeField]
    private AudioSource audioSource;

    private float checkTimer;

    public static event Action<string> OnEventStart;
    public static event Action<string> OnEventEnd;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                Debug.Log("AudioSource добавлен автоматически к объекту " + gameObject.name);
            }
        }
    }

    private void OnEnable()
    {
        ResetAllEvents();
    }

    private void OnDisable()
    {
        StopAllEvents();
    }

    private void Update()
    {
        UpdateActiveEvents();

        checkTimer += Time.deltaTime;
        if (checkTimer >= checkInterval)
        {
            checkTimer = 0f;
            CheckForNewEvents();
        }
    }

    private void ResetAllEvents()
    {
        foreach (var e in events)
        {
            e.isActive = false;
            e.remainingTime = 0f;
        }
    }

    private void StopAllEvents()
    {
        foreach (var e in events)
        {
            if (e.isActive)
            {
                EndEvent(e);
            }
        }
    }

    private void UpdateActiveEvents()
    {
        foreach (var e in events)
        {
            if (e.isActive)
            {
                e.remainingTime -= Time.deltaTime;
                if (e.remainingTime <= 0)
                {
                    EndEvent(e);
                }
            }
            else if (e.delayBeforeStart > 0)
            {
                e.delayBeforeStart -= Time.deltaTime;
                if (e.delayBeforeStart <= 0)
                {
                    StartEvent(e);
                }
            }
        }
    }

    private void CheckForNewEvents()
    {
        foreach (var e in events)
        {
            if (!e.isActive && e.delayBeforeStart <= 0)
            {
                if (UnityEngine.Random.value <= e.chance)
                {
                    if (e.delayBeforeStart > 0)
                    {
                        continue;
                    }
                    StartEvent(e);
                }
            }
        }
    }

    private void StartEvent(GameEvent e)
    {
        e.isActive = true;
        e.remainingTime = e.duration;

        if (e.startSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(e.startSound);
        }

        if (!string.IsNullOrEmpty(e.startMessage))
        {
            Debug.Log("Ivent Start: " + e.startMessage);
            OnEventStart?.Invoke(e.startMessage);
        }
    }

    private void EndEvent(GameEvent e)
    {
        e.isActive = false;

        if (e.endSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(e.endSound);
        }

        OnEventEnd?.Invoke(e.eventName);
        Debug.Log("Ivent End: " + e.eventName);
    }

    #region Публичные методы для внешнего доступа


    public void ActivateEvent(string eventName)
    {
        var e = events.Find(x => x.eventName == eventName);
        if (e != null && !e.isActive)
        {
            StartEvent(e);
        }
    }

  
    public void DeactivateEvent(string eventName)
    {
        var e = events.Find(x => x.eventName == eventName);
        if (e != null && e.isActive)
        {
            EndEvent(e);
        }
    }


    public void AddEvent(GameEvent newEvent)
    {
        if (!events.Exists(x => x.eventName == newEvent.eventName))
        {
            events.Add(newEvent);
        }
    }


    public void RemoveEvent(string eventName)
    {
        events.RemoveAll(x => x.eventName == eventName);
    }


    public bool IsEventActive(string eventName)
    {
        var e = events.Find(x => x.eventName == eventName);
        return e != null && e.isActive;
    }


    public List<GameEvent> GetAllEvents()
    {
        return new List<GameEvent>(events);
    }


    public void SetEvents(List<GameEvent> newEvents)
    {
        events = new List<GameEvent>(newEvents);
    }
    #endregion
} //если кто то читает это, знайте как я ЗАЕБАЛСЯ читать этот код!!!! спасибо гайдам и другим открытым источникам. ВЫ лучшие!