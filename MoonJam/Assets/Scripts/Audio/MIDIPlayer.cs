using System;
using System.IO;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using UnityEngine;
using NoteType;
using System.Collections.Generic;

public class MIDIPlayer : MonoBehaviour
{
    [SerializeField] public string midiFileName;

    public delegate void OnLockSet(LockState lockState, bool state);
    public static event OnLockSet OnLockSetDelegate;

    private LockState lockState;
    private MidiFile midiFile;
    private Playback playback;

    public void InitPlayer(LockState lockState)
    {
        this.lockState = lockState;

        string midiFolderPath = Path.Combine("Assets", "Music", "MIDI");
        string midiFilePath = Path.Combine(midiFolderPath, midiFileName);
        midiFile = MidiFile.Read(midiFilePath);

        // Regular precision tick generator needed to prevent Unity from crashing
        playback = midiFile.GetPlayback(new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        playback.NoteCallback += OnNoteCallback;
        Play();
    }

    void OnDestroy()
    {
        playback.NoteCallback -= OnNoteCallback;
        Stop();
        playback.Dispose();
    }

    public void Play()
    {
        playback.Start();
    }

    public void Stop()
    {
        playback.Stop();
    }

    public NotePlaybackData OnNoteCallback(NotePlaybackData rawNoteData, long rawTime, long rawLength, TimeSpan playbackTime)
    {
        Debug.Log("note: " + rawNoteData.NoteNumber);

        OnLockSetDelegate(lockState, true);

        return rawNoteData;
    }

}
