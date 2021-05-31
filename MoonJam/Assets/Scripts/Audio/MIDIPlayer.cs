using System.IO;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Tools;
using UnityEngine;
using System;

public class MIDIPlayer : MonoBehaviour
{
    MidiFile midiFile;
    Playback playback;

    // Start is called before the first frame update
    void Start()
    {
        string midiFolderPath = Path.Combine("Assets", "Music", "MIDI");
        string midiFilePath = Path.Combine(midiFolderPath, "Arp_Polymer.mid");
        midiFile = MidiFile.Read(midiFilePath);

        // Regular precision tick generator needed to prevent Unity from crashing
        playback = midiFile.GetPlayback(new MidiClockSettings
        {
            CreateTickGeneratorCallback = () => new RegularPrecisionTickGenerator()
        });
        playback.NoteCallback += OnNoteCallback;
        playback.Start();
    }

    private void OnDestroy()
    {
        playback.Stop();
        playback.Dispose();
    }

    public NotePlaybackData OnNoteCallback(NotePlaybackData rawNoteData, long rawTime, long rawLength, TimeSpan playbackTime)
    {
        Debug.Log("RawNote: " + rawNoteData.NoteNumber);
        return rawNoteData;
    }

}
